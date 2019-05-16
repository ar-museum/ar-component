using Assets.Scripts.AR_TEAM.Http;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;
using System.Linq;

namespace Assets.Scripts.AR_TEAM.HttpRequests {
    public class HttpRequests {
        private static readonly string JSON_TOKEN_INPUT =
            "{ \"deviceId\": \"2535C5EB-D6ED-4ABC-956B-4ACF29938F26\", \"token\": \"680bff9eb1ba0a8d48badd598be95c5642ad2939\" }";
        private static readonly string API_URL = "https://armuseum.ml/api/";
        private static readonly string GET_MUSEUM_URL = "https://armuseum.ml/api";
        private static readonly string EXHIBITS_URL = API_URL + "exhibit";
        private static readonly string EXHIBITS_RELS_URL = API_URL + "exh/rels";
        private static readonly string AUTHORS_URL = API_URL + "author";
        private static readonly string EXPOSITIONS_URL = API_URL + "exposition";
        private static readonly string MUSEUMS_URL = API_URL + "museum";
        private static readonly string MUSEUMS_RELS_URL = API_URL + "mus/rels";
        private static readonly string EXPOSITIONS_RELS_URL = API_URL + "expo/rels";
        private static readonly string UPDATE_URL = API_URL + "update";
        public static readonly string SITE_URL = "https://armuseum.ml/";

        public delegate void OnComplete<T>(T x);
        public delegate IEnumerator OnCompleteYield<T>(T x);

        private OnCompleteYield<MuseumDto> OnCompleteMuseumFunction { get; set; }
        private OnCompleteYield<MuseumInfo> OnCompleteMuseumInfoFunction { get; set; }
        private MuseumDto Museum { get; set; }

        private static void SetHeaders(UnityWebRequest request) {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "680bff9eb1ba0a8d48badd598be95c5642ad2939");
            request.SetRequestHeader("UserDevice", "2535C5EB-D6ED-4ABC-956B-4ACF29938F26");
        }

        private IEnumerator DoGetRequest(string url, OnComplete<string> callback) {
            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.certificateHandler = new CustomCertificateHandler();
            SetHeaders(request);

            yield return request.SendWebRequest();

            LoadFindData.messageToShow = "Get " + url;
            if (request.isNetworkError) {
                LoadFindData.messageToShow = "Error While Sending: " + request.error;
                Debug.Log("Error While Sending: " + request.error);
            } else {
                Debug.Log("Received: " + request.downloadHandler.text);
                callback(request.downloadHandler.text);
            }
        }

        private IEnumerator DoGetRequest(string url, OnCompleteYield<string> callback) {
            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.certificateHandler = new CustomCertificateHandler();
            SetHeaders(request);

            yield return request.SendWebRequest();

            LoadFindData.messageToShow = "Get " + url;
            if (request.isNetworkError) {
                LoadFindData.messageToShow = "Error While Sending: " + request.error;
                Debug.Log("Error While Sending: " + request.error);
            } else {
                Debug.Log("Received: " + request.downloadHandler.text);
                yield return callback(request.downloadHandler.text);
            }
        }

        private IEnumerator DoPostRequest(string url, string json, OnComplete<string> callback) {
            var request = UnityWebRequest.Post(url, json);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.certificateHandler = new CustomCertificateHandler();
            SetHeaders(request);

            yield return request.SendWebRequest();

            LoadFindData.messageToShow = "Post " + url;
            if (request.isNetworkError) {
                LoadFindData.messageToShow = "Error While Sending: " + request.error;
                Debug.Log("Error While Sending: " + request.error);
            } else {
                Debug.Log("Received: " + request.downloadHandler.text);
                callback(request.downloadHandler.text);
            }
        }

        private IEnumerator DoPostRequest(string url, string json, OnCompleteYield<string> callback) {
            var body = Encoding.UTF8.GetBytes(json);
            var request = new UnityWebRequest(url, "POST") {
                uploadHandler = new UploadHandlerRaw(body),
                downloadHandler = new DownloadHandlerBuffer(),
                certificateHandler = new CustomCertificateHandler()
            };
            SetHeaders(request);

            yield return request.SendWebRequest();

            LoadFindData.messageToShow = "Post " + url;
            if (request.isNetworkError) {
                LoadFindData.messageToShow = "Error While Sending: " + request.error;
                Debug.Log("Error While Sending: " + request.error);
            } else {
                Debug.Log("Received: " + request.downloadHandler.text);
                yield return callback(request.downloadHandler.text);
            }
        }

        public IEnumerator DownloadData(string url, string pathOnDisk) {
            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerFile(pathOnDisk);
            request.certificateHandler = new CustomCertificateHandler();

            yield return request.SendWebRequest();
        }

        public IEnumerator GetMuseumData(OnCompleteYield<MuseumDto> onComplete, int id) {
            OnCompleteMuseumFunction = onComplete;
            yield return DoGetRequest($"{MUSEUMS_RELS_URL}/{id}", OnMuseumCompleted);
        }

        public IEnumerator GetMuseumInfo(OnCompleteYield<MuseumInfo> onComplete, GeoCoordinate coordinate) {
            OnCompleteMuseumInfoFunction = onComplete;
            var json = new JSONObject();
            json.Add("latitude", coordinate.Latitude);
            json.Add("longitude", coordinate.Longitude);
            yield return DoPostRequest(GET_MUSEUM_URL, json.ToString(), OnMuseumInfoCompleted);
        }

        private IEnumerator OnMuseumInfoCompleted(string json) {
            var node = JSON.Parse(json);
            var info = Deserializers.DeserializeMuseumInfo(node);

            var updateJson = new JSONObject();
            if (info != null)
            {
                updateJson.Add("version", info.VuforiaDatabaseVersion);
                updateJson.Add("museum_id", info.MuseumId);
                yield return DoPostRequest(UPDATE_URL, updateJson.ToString(), j => OnMuseumInfoWithVuforiaFilesCompleted(info, j));
            }
        }

        private IEnumerator OnMuseumInfoWithVuforiaFilesCompleted(MuseumInfo info, string json) {
            var node = JSON.Parse(json);
            info.VuforiaFiles = Deserializers
                .DeserializeStringArray(node["files"])
                .Select(x => SITE_URL + x)
                .Where(x => x != null)
                .ToList();
            yield return OnCompleteMuseumInfoFunction(info);
        }

        private IEnumerator OnMuseumCompleted(string json) {
            var node = JSON.Parse(json);
            var museum = Deserializers.DeserializeMuseum(node);
            Museum = museum;

            if (museum.Expositions.Count > 0) {
                foreach (var expo in museum.Expositions) {
                    yield return DoGetRequest($"{EXPOSITIONS_RELS_URL}/{expo.ExpositionId}", j => OnExpositionComplete(j, expo));
                }
            }
            yield return OnCompleteMuseumFunction(Museum);
        }

        private IEnumerator OnExpositionComplete(string json, Exposition exposition) {
            var node = JSON.Parse(json);
            exposition.Exhibits = Deserializers.DeserializeExhibitList(node["exhibits"]);

            foreach (var exh in exposition.Exhibits) {
                yield return DoGetRequest($"{EXHIBITS_RELS_URL}/{exh.ExhibitId}", j => OnExhibitComplete(j, exh));
            }
        }

        private void OnExhibitComplete(string json, Exhibit exh) {
            var node = JSON.Parse(json);
            exh.Author = Deserializers.DeserializeAuthor(node["authors"]);
            exh.AudioUrl = node["audio_path"];
            exh.PhotoUrl = node["photo_path"];
        }

        private void OnAuthorComplete(string json, Exhibit exhibit) {
            var node = JSON.Parse(json);
            exhibit.Author = Deserializers.DeserializeAuthor(node);
        }
    }
}