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
        private static readonly string AUTHORS_URL = API_URL + "author";
        private static readonly string EXPOSITIONS_URL = API_URL + "exposition";
        private static readonly string MUSEUMS_URL = API_URL + "museum";
        private static readonly string MUSEUMS_RELS_URL = API_URL + "mus/rels";
        private static readonly string EXPOSITIONS_RELS_URL = API_URL + "expo/rels";

        public delegate void OnComplete<T>(T x);
        public delegate IEnumerator OnCompleteYield<T>(T x);

        private List<Exhibit> Exhibits { get; set; }
        private List<Author> Authors { get; set; }
        private List<Exposition> Expositions { get; set; }
        private int Completed { get; set; }

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

        public IEnumerator GetMuseumData(OnComplete<Http.Museum> onComplete, int id) {
            yield return DoGetRequest($"{MUSEUMS_RELS_URL}/{id}", OnMuseumCompleted);
        }

        //public IEnumerator GetEverything(OnComplete<(List<Exhibit>, List<Author>)> onComplete) {
        //    yield return DoRequest(EXHIBITS_URL, JSON_TOKEN_INPUT, OnExhibitCompleted);
        //    yield return DoRequest(AUTHORS_URL, JSON_TOKEN_INPUT, OnAuthorsCompleted);
        //    yield return DoRequest(EXPOSITIONS_URL, JSON_TOKEN_INPUT, OnExpositionsCompleted);

        //    onComplete((Exhibits, Authors));
        //}

        private IEnumerator OnMuseumCompleted(string json) {
            var node = JSON.Parse(json);
            var museum = Deserializers.DeserializeMuseum(node);

            foreach (var expo in museum.Expositions) {
                yield return DoGetRequest(EXPOSITIONS_RELS_URL, j => OnExhibitComplete(j, expo)));
            }
        }

        private void OnExhibitComplete(string json, Exposition exposition) {

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