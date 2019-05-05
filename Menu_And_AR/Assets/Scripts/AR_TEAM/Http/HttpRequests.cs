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
        private static readonly string EXHIBIT_URL = API_URL + "exhibit";

        private delegate void RequestCallback(string json);

        public delegate void OnComplete<T>(T x);

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

        public IEnumerator GetExhibits(OnComplete<List<Exhibit>> onComplete) {
            return DoRequest(EXHIBIT_URL, JSON_TOKEN_INPUT, json => OnExhibitCompleted(json, onComplete));
        }

        private void OnExhibitCompleted(string json, OnComplete<List<Exhibit>> onComplete) {
            var node = JSON.Parse(json);
            var exhibits = Deserializers.DeserializeExhibitList(node);
            onComplete(exhibits);
        }
    }
}