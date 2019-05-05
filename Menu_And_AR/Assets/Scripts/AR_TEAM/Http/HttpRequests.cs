using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.AR_TEAM.HttpRequests {
    class HttpRequests {
        public IEnumerator PostRequest(string url, string json) {
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
            }
        }

        public IEnumerator GetMcStats() {
            return PostRequest("https://api.mojang.com/orders/statistics",
                "{\"metricKeys\": [\"item_sold_minecraft\",\"prepaid_card_redeemed_minecraft\"]}");
        }
    }
}