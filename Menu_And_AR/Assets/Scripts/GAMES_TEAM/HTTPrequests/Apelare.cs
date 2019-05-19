using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameRequest
{
    public class Apelare : MonoBehaviour
    {
        public static List<Photo> Ph = new List<Photo>();
        static List<JsonFile> JsonFiles = new List<JsonFile>();

        public IEnumerator Start()
        {
            HTTPRequest obj = new HTTPRequest();
            yield return obj.ReturnJson(OnPhotosComplete);
        }

        void OnPhotosComplete(List<Photo> list)
        {
            Ph = list;
        }

        void OnJsonsComplete(List<JsonFile> list)
        {
            JsonFiles = list;
        }
    }
}
