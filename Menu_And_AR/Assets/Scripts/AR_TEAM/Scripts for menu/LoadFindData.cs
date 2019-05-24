using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using Assets.Scripts.AR_TEAM.Http;
using System.Threading.Tasks;
using System;

public class LoadFindData : MonoBehaviour
{
    public static double latitudine = 0;
    public static double longitudine = 0;
    public static string messageToShow;
    Text textDownloads;
    Image loadingImage;
    Sprite[] frames;


    void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    IEnumerator Start() {
        yield return DoStart().AsIEnumerator();
    }

    async Task DoStart()
    {
        GameObject textDownloadsObject = GameObject.Find("TextDownloads");
        textDownloads = textDownloadsObject.GetComponent<Text>();

        GameObject loadingImageObject = GameObject.Find("LoadingImage");
        loadingImage = loadingImageObject.GetComponent<Image>();
        frames = Resources.LoadAll<Sprite>("AR_TEAM/images/LoadingFrames");

#if UNITY_EDITOR
        if (latitudine == 0 && longitudine == 0)
        {
            // Testing in editor va fi facut pe muzeul Mihai Eminescu
            latitudine =  47.17910387;//47.173975638;// 47.16686875;
            longitudine = 27.56697617; // 27.574884630;// 27.5841265;
        }
#elif UNITY_ANDROID
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            await LocationService();
        }
#endif
        await MuseumManager.Instance.RequestMuseumInfo(new GeoCoordinate(latitudine, longitudine));

        if (MuseumManager.Instance.CurrentMuseum != null) {
            await MuseumManager.Instance.DownloadAllAudios();

            await MuseumManager.Instance.DownloadAllPhotos();

            await MuseumManager.Instance.DownloadVuforiaFiles();
        }
        SceneManager.LoadScene("MenuScene");
    }

    void Update()
    {
        double index = Time.time * 10.0;
        int aux = (int)index % frames.Length;
        loadingImage.sprite = frames[aux];
        textDownloads.text = messageToShow;
    }

    public static async Task LocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            print("Utlizatorul nu a activat GPS-ul.");
            latitudine = -1;
            longitudine = -1;
            return;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timpul a expirat.");
            latitudine = -1;
            longitudine = -1;
            return;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Nu s-a putut determina locatia device-ului.");
            latitudine = -1;
            longitudine = -1;
            return;
        }
        else
        {
            latitudine = Input.location.lastData.latitude;
            longitudine = Input.location.lastData.longitude;
        }

        Input.location.Stop();
    }
}
