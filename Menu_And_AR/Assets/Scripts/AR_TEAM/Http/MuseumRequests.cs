using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.AR_TEAM.Http {
    public static class MuseumRequests {
        public static Task<Author> DownloadAuthor(int id) {
            return HttpRequests.DoGetRequestNew($"{Endpoints.AUTHORS_URL}/{id}", Deserializers.DeserializeAuthor);
        }

        public static async Task<Exhibit> DownloadExhibit(int id) {
            var exh = await HttpRequests.DoGetRequestNew($"{Endpoints.EXHIBITS_RELS_URL}/{id}", Deserializers.DeserializeExhibit);
            exh.Author = await DownloadAuthor(exh.Author.AuthorId);

            return exh;
        }

        public static async Task<Exposition> DownloadExposition(int id) {
            var expo = await HttpRequests.DoGetRequestNew($"{Endpoints.EXPOSITIONS_RELS_URL}/{id}", Deserializers.DeserializeExposition);
            var taskList = new List<Task<Exhibit>>();

            foreach (Exhibit i in expo.Exhibits) {
                var task = DownloadExhibit(i.ExhibitId);
                taskList.Add(task);
            }

            await Task.WhenAll(taskList);

            for (int i = 0; i < expo.Exhibits.Count; ++i) {
                expo.Exhibits[i] = taskList[i].Result;
            }

            return expo;
        }

        public static async Task<MuseumDto> DownloadMuseum(int id) {
            var museum = await HttpRequests.DoGetRequestNew($"{Endpoints.MUSEUMS_RELS_URL}/{id}", Deserializers.DeserializeMuseum);
            var taskList = new List<Task<Exposition>>();

            foreach (Exposition i in museum.Expositions) {
                taskList.Add(DownloadExposition(i.ExpositionId));
            }

            await Task.WhenAll(taskList);

            for (int i = 0; i < museum.Expositions.Count; ++i) {
                museum.Expositions = taskList
                    .Select(x => x.Result)
                    .ToList();
            }

            return museum;
        }

        public static async Task<MuseumInfo> DownloadMuseumInfo(GeoCoordinate coordinate) {
            var json = new JSONObject();
            json.Add("latitude", coordinate.Latitude);
            json.Add("longitude", coordinate.Longitude);

            var info = await HttpRequests.DoPostRequestNew(Endpoints.GET_MUSEUM_URL, json.ToString(), Deserializers.DeserializeMuseumInfo);

            if (info == null)
            {
                return info;
            }

            json = new JSONObject();
            json.Add("version", info.VuforiaDatabaseVersion);
            json.Add("museum_id", info.MuseumId);

            var node = JSON.Parse(await HttpRequests.DoPostRequestNew(Endpoints.UPDATE_URL, json.ToString()));

            info.VuforiaFiles = Deserializers
                .DeserializeStringArray(node["files"])
                .Select(x => Endpoints.SITE_URL + x)
                .Where(x => x != null)
                .ToList();

            return info;
        }

        public static async Task DownloadFile(string url, string pathOnDisk) {
            var request = new UnityWebRequest(url, "GET") {
                downloadHandler = new DownloadHandlerFile(pathOnDisk),
                certificateHandler = new CustomCertificateHandler()
            };

            LoadFindData.messageToShow = "Downloading + " + pathOnDisk;
            Debug.Log("Downloading + " + pathOnDisk);

            await request.SendWebRequest();
        }

        public static async Task DownloadFiles(List<(string, string)> files) {
            var tasks = new List<Task>();
            foreach (var (url, disk) in files) {
                tasks.Add(DownloadFile(url, disk));
            }

            await Task.WhenAll(tasks);
        }
    }
}
