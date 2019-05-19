using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Collections.Generic;

namespace GameRequest
{
    class HTTPRequest
    {
        private static readonly string API_URL = "https://armuseum.ml/api/";
        private static readonly string DRAGNDROP_URL = API_URL + "update/drag/1";
        private static readonly string TRIVIA_URL = API_URL + "update/trivia/";
        private static readonly string FINAL_URL = DRAGNDROP_URL; //PlayerPrefs.GetInt("Games_Museum");
        
        public delegate void OnComplete<T>(T x);
        public delegate IEnumerator OnCompleteYield<T>(T x);

        public List<Photo> Photos { get; set; }
        public List<JsonFile> jsonFiles { get; set; }
        private int Completed { get; set; }

        private static void SetHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "680bff9eb1ba0a8d48badd598be95c5642ad2939");
            request.SetRequestHeader("UserDevice", "2535C5EB-D6ED-4ABC-956B-4ACF29938F26");
        }

        private IEnumerator DoGetRequest(string url, OnComplete<string> callback)
        {
            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.certificateHandler = new CustomCertificateHandler();
            SetHeaders(request);

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                callback(request.downloadHandler.text);
            }
        }
        private IEnumerator DoGetRequest(string url, OnCompleteYield<string> callback)

        {
            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.certificateHandler = new CustomCertificateHandler();
            SetHeaders(request);

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                yield return callback(request.downloadHandler.text);
            }
        }

        private void OnPhotoComplete(string json, OnComplete<List<Photo>> onComplete)
        {
            var node = JSON.Parse(json);

            Photos = GameDeserializer.DeserializePhotoList(node["photos"]);
            onComplete(Photos);
        }

        private void OnJsonComplete(string json, OnComplete<List<JsonFile>> onComplete)
        {
            var node = JSON.Parse(json);

            jsonFiles = GameDeserializer.DeserializeJsonList(node["jsons"]);
            onComplete(jsonFiles);


        }

        public IEnumerator ReturnJson(OnComplete<List<Photo>> onComplete)
        {
            yield return DoGetRequest(FINAL_URL, j => OnPhotoComplete(j, onComplete));
        }
    }
}