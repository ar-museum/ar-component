using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameRequest;

namespace Drag
{
    public class DragLoading : MonoBehaviour
    {
        Apelare requests = new Apelare();
        [SerializeField]
        TextMeshProUGUI text;
        [SerializeField]
        Slider slider;
        [SerializeField]
        TextMeshProUGUI tmp;
        public static String pathOnDisk;
        List<string> DiskPaths { get; set; }

        public IEnumerator DownloadData(string url, string pathOnDisk)
        {
            Debug.Log(url);
            Debug.Log(pathOnDisk);
            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerFile(pathOnDisk);
            request.certificateHandler = new CustomCertificateHandler();

            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
        }

        IEnumerator DownloadAllPhotos()
        {
            String[] stringBase = new String[10];
            List<string> photos = new List<string>();
            for (int i = 0; i < Apelare.Ph.Count ; i++)
            {
                //Debug.Log(Apelare.Ph[i].Path);
                photos.Add("https://armuseum.ml/" + Apelare.Ph[i].Path);
            }

            pathOnDisk = Application.persistentDataPath + "/Images";

            if (!Directory.Exists(pathOnDisk))
                Directory.CreateDirectory(pathOnDisk);

            DiskPaths = new List<string>();
            int j = 0;
            foreach (var photo in photos)
            {
                if (j % 4 == 0)
                    text.GetComponentInChildren<TextMeshProUGUI>().text = "Loading";
                else text.GetComponentInChildren<TextMeshProUGUI>().text = text.GetComponentInChildren<TextMeshProUGUI>().text + ".";
                var baseName = $"{photo}"; j++;
                //Debug.Log($"{photo}");
                String[] path = baseName.Split('/');
                var diskPath = $"{pathOnDisk}/{path[path.Length - 1]}";
                DiskPaths.Add(diskPath);
                yield return DownloadData($"{baseName}", diskPath);

                slider.value += (float)1 / photos.Count;
                tmp.text = (int)((slider.value) * 100) + "%";
            }
            slider.value += (float)1 / photos.Count;
        }

        IEnumerator GetPhotos()
        {

            yield return requests.StartTakingPhotosPaths();
            yield return DownloadAllPhotos();

            text.GetComponentInChildren<TextMeshProUGUI>().text = "Loading";
            int i = 0;
            foreach (var path in DiskPaths)
            {
                //Debug.Log(path);


                Images image = new Images(); i++;
                image.setLink(path);

                WWW www = new WWW("file:///" + path);
                while (!www.isDone)
                    yield return null;
                Texture2D myTexture = www.texture;
                //Debug.Log(myTexture.height);
                image.setTexture(myTexture);
                Manager.images.Add(image);

            }
            SceneManager.LoadScene("DragAndDrop");
        }

        public static void deleteFolder()
        {
            var dir = new DirectoryInfo(pathOnDisk);
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
        }

        void Start()
        {
            StartCoroutine(GetPhotos());
        }

    }
}
