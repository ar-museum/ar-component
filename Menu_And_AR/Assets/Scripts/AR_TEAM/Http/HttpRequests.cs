using Assets.Scripts.AR_TEAM.Http;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Collections.Generic;

namespace Assets.Scripts.AR_TEAM.HttpRequests {
    class HttpRequests {
        private static readonly string JSON_TOKEN_INPUT =
            "{ \"deviceId\": \"2535C5EB-D6ED-4ABC-956B-4ACF29938F26\", \"token\": \"680bff9eb1ba0a8d48badd598be95c5642ad2939\" }";
        private static readonly string API_URL = "museum.lc/web-admin/public/api/";
        private static readonly string EXHIBITS_URL = API_URL + "exhibit";
        private static readonly string AUTHORS_URL = API_URL + "author";
        private static readonly string EXPOSITIONS_URL = API_URL + "exposition";

        private delegate void RequestCallback(string json);

        public delegate void OnComplete<T>(T x);

        private List<Exhibit> Exhibits { get; set; }
        private List<Author> Authors { get; set; }
        private List<Exposition> Expositions { get; set; }
        private int Completed { get; set; }

        private IEnumerator DoRequest(string url, string json, RequestCallback callback) {
            var request = new UnityWebRequest(url, "POST");
            var bytes = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError) {
                Debug.Log("Error While Sending: " + request.error);
            } else {
                Debug.Log("Received: " + request.downloadHandler.text);
                callback(request.downloadHandler.text);
            }
        }

        public IEnumerator GetEverything(OnComplete<(List<Exhibit>, List<Author>)> onComplete) {
            yield return DoRequest(EXHIBITS_URL, JSON_TOKEN_INPUT, OnExhibitCompleted);
            yield return DoRequest(AUTHORS_URL, JSON_TOKEN_INPUT, OnAuthorsCompleted);
            yield return DoRequest(EXPOSITIONS_URL, JSON_TOKEN_INPUT, OnExpositionsCompleted);

            onComplete((Exhibits, Authors));
        }

        private void OnExhibitCompleted(string json) {
            var node = JSON.Parse(json);
            Exhibits = Deserializers.DeserializeExhibitList(node);
            ++Completed;
        }

        private void OnAuthorsCompleted(string json) {
            var node = JSON.Parse(json);
            Authors = Deserializers.DeserializeAuthorList(node);
            ++Completed;
        }

        private void OnExpositionsCompleted(string json) {
            var node = JSON.Parse(json);
            Expositions = Deserializers.DeserializeExpositionsList(node);
            ++Completed;
        }
    }
}