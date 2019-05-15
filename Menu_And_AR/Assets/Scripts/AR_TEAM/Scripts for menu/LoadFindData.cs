using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using Assets.Scripts.AR_TEAM.Http;

public class LoadFindData : MonoBehaviour
{
    public static double latitudine = 0;
    public static double longitudine = 0;
    public static bool isUnitTest = false;
    public static string messageToShow;
    Text textDownloads;


    void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    IEnumerator Start()
    {
        GameObject textDownloadsObject = GameObject.Find("TextDownloads");
        textDownloads = textDownloadsObject.GetComponent<Text>();

#if UNITY_EDITOR
        if (latitudine == 0 && longitudine == 0)
        {
            // Testing in editor va fi facut pe muzeul Mihai Eminescu
            latitudine =  47.173975638;// 47.179035;// 47.16686875;
            longitudine = 27.574884630;// 27.567063;// 27.5841265;
        }
#elif UNITY_ANDROID
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return StartCoroutine(LocationService());
        }
#endif
        
        yield return MuseumManager.Instance.RequestMuseumInfo(new GeoCoordinate(latitudine, longitudine));

        if(MuseumManager.Instance.CurrentMuseum != null)
        {
            yield return MuseumManager.Instance.GetAllAudios();
            
            yield return MuseumManager.Instance.GetVuforiaFiles();
        }
        SceneManager.LoadScene("MenuScene");
        yield return null;
    }

    void Update()
    {
        textDownloads.text = messageToShow;
    }

    IEnumerator LocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            print("Utlizatorul nu a activat GPS-ul.");
            latitudine = -1;
            longitudine = -1;
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timpul a expirat.");
            latitudine = -1;
            longitudine = -1;
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Nu s-a putut determina locatia device-ului.");
            latitudine = -1;
            longitudine = -1;
            yield break;
        }
        else
        {
            latitudine = Input.location.lastData.latitude;
            longitudine = Input.location.lastData.longitude;
        }

        Input.location.Stop();
    }
}
