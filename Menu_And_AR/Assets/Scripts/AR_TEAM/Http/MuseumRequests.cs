using SimpleJSON;
using System.Linq;
using System.Threading.Tasks;
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

            for (int i = 0; i < expo.Exhibits.Count; ++i) {
                expo.Exhibits[i] = await DownloadExhibit(expo.Exhibits[i].ExhibitId);
            }

            return expo;
        }

        public static async Task<MuseumDto> DownloadMuseum(int id) {
            var museum = await HttpRequests.DoGetRequestNew($"{Endpoints.MUSEUMS_RELS_URL}/{id}", Deserializers.DeserializeMuseum);

            for (int i = 0; i < museum.Expositions.Count; ++i) {
                museum.Expositions[i] = await DownloadExposition(museum.Expositions[i].ExpositionId);
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
            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerFile(pathOnDisk);
            request.certificateHandler = new CustomCertificateHandler();

            await request.SendWebRequest();
        }
    }
}
