using Assets.Scripts.AR_TEAM.Http;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Collections.Generic;

namespace Assets.Scripts.AR_TEAM.HttpRequests {
    class HttpRequests {
        private static readonly string JSON_TOKEN_INPUT =
            "{ \"deviceId\": \"2535C5EB-D6ED-4ABC-956B-4ACF29938F26\", \"token\": \"680bff9eb1ba0a8d48badd598be95c5642ad2939\" }";
        private static readonly string API_URL = "https://armuseum.ml/api/";
        private static readonly string EXHIBITS_URL = API_URL + "exhibit";
        private static readonly string EXHIBITS_RELS_URL = API_URL + "exh/rels";
        private static readonly string AUTHORS_URL = API_URL + "author";
        private static readonly string EXPOSITIONS_URL = API_URL + "exposition";
        private static readonly string MUSEUMS_URL = API_URL + "museum";
        private static readonly string MUSEUMS_RELS_URL = API_URL + "mus/rels";
        private static readonly string EXPOSITIONS_RELS_URL = API_URL + "expo/rels";

        public delegate void OnComplete<T>(T x);
        public delegate IEnumerator OnCompleteYield<T>(T x);

        private OnComplete<MuseumDto> OnCompleteFunction { get; set; }
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

            if (request.isNetworkError) {
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

            if (request.isNetworkError) {
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

            if (request.isNetworkError) {
                Debug.Log("Error While Sending: " + request.error);
            } else {
                Debug.Log("Received: " + request.downloadHandler.text);
                callback(request.downloadHandler.text);
            }
        }

        public IEnumerator GetMuseumData(OnComplete<MuseumDto> onComplete, int id) {
            OnCompleteFunction = onComplete;
            yield return DoGetRequest($"{MUSEUMS_RELS_URL}/{id}", OnMuseumCompleted);
        }

        private IEnumerator OnMuseumCompleted(string json) {
            var node = JSON.Parse(json);
            var museum = Deserializers.DeserializeMuseum(node);
            Museum = museum;

            foreach (var expo in museum.Expositions) {
                yield return DoGetRequest($"{EXPOSITIONS_RELS_URL}/{expo.ExpositionId}", j => OnExpositionComplete(j, expo));
            }
        }

        private IEnumerator OnExpositionComplete(string json, Exposition exposition) {
            var node = JSON.Parse(json);
            exposition.Exhibits = Deserializers.DeserializeExhibitList(node["exhibits"]);

            foreach (var exh in exposition.Exhibits) {
                yield return DoGetRequest($"{EXHIBITS_RELS_URL}/{exh.ExhibitId}", j => OnExhibitComplete(j, exh));
            }

            OnCompleteFunction(Museum);
        }

        private void OnExhibitComplete(string json, Exhibit exposition) {
            var node = JSON.Parse(json);
            exposition.Author = Deserializers.DeserializeAuthor(node["authors"]);
        }

        private void OnAuthorComplete(string json, Exhibit exhibit) {
            var node = JSON.Parse(json);
            exhibit.Author = Deserializers.DeserializeAuthor(node);
        }
    }
}