using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameRequest
{
    public class Apelare : MonoBehaviour
    {
        public static List<Photo> Ph = new List<Photo>();
        public static string paths;
        public static List<JsonFile> JsonFiles = new List<JsonFile>();

        public IEnumerator StartTakingPhotosPaths()
        {
            HTTPRequest obj = new HTTPRequest();
            yield return obj.ReturnJson(OnPhotosComplete);
        }

        public IEnumerator StartTakingJsonNames()
        {
            HTTPRequest obj = new HTTPRequest();
            yield return obj.ReturnNames(OnJsonsComplete);
        }

        void OnPhotosComplete(List<Photo> list)
        {
            Ph = list;
        }

        void OnJsonsComplete(List<JsonFile> list)
        {
            JsonFiles = list;
            foreach(var item in list)
            {
                paths += item.Name+"\n";
            }
        }
    }
}
