using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameRequest
{
    public class Apelare : MonoBehaviour
    {
        List<Photo> Ph = new List<Photo>();
        List<JsonFile> JsonFiles = new List<JsonFile>();

        public IEnumerator Start()
        {
            HTTPRequest obj = new HTTPRequest();
            yield return obj.ReturnJson(OnPhotosComplete);
        }

        void OnPhotosComplete(List<Photo> list)
        {
            Debug.Log(list);
        }

        void OnJsonsComplete(List<JsonFile> list)
        {
            Debug.Log(list);
        }
    }
}
