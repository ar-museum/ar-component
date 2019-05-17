using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Assets.Scripts.AR_TEAM.Http {
    public class HttpRequests {
        public static void SetHeaders(UnityWebRequest request) {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "680bff9eb1ba0a8d48badd598be95c5642ad2939");
            request.SetRequestHeader("UserDevice", "2535C5EB-D6ED-4ABC-956B-4ACF29938F26");
        }

        public static async Task<string> DoGetRequestNew(string url) {
            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.certificateHandler = new CustomCertificateHandler();
            SetHeaders(request);

            LoadFindData.messageToShow = "Get " + url;
            await request.SendWebRequest();

            if (request.isNetworkError) {
                var message = "Error While Sending: " + request.error;
                LoadFindData.messageToShow = message;
                Debug.Log(message);
                throw new ArgumentException(message);
            } else {
                Debug.Log("Received: " + request.downloadHandler.text);
                return request.downloadHandler.text;
            }
        }

        public static async Task<T> DoGetRequestNew<T>(string url, Deserializers.DeserializeThing<T> deserialize) {
            var json = await DoGetRequestNew(url);
            var node = JSONNode.Parse(json);
            return deserialize(node);
        }

        public static async Task<string> DoPostRequestNew(string url, string json) {
            var body = Encoding.UTF8.GetBytes(json);
            var request = new UnityWebRequest(url, "POST") {
                uploadHandler = new UploadHandlerRaw(body),
                downloadHandler = new DownloadHandlerBuffer(),
                certificateHandler = new CustomCertificateHandler()
            };
            SetHeaders(request);

            await request.SendWebRequest();

            LoadFindData.messageToShow = "Post " + url;
            if (request.isNetworkError) {
                var message = "Error While Sending: " + request.error;
                LoadFindData.messageToShow = message;
                Debug.Log(message);
                throw new ArgumentException(message);
            } else {
                Debug.Log("Received: " + request.downloadHandler.text);
                return request.downloadHandler.text;
            }
        }

        public static async Task<T> DoPostRequestNew<T>(string url, string json, Deserializers.DeserializeThing<T> deserialize) {
            var newJson = await DoPostRequestNew(url, json);
            var node = JSONNode.Parse(newJson);
            return deserialize(node);
        }
    }
}