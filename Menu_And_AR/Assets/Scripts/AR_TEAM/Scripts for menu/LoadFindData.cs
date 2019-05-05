﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class LoadFindData : MonoBehaviour
{
    public static double latitudine = 0;
    public static double longitudine = 0;
    public static MuseumArray museumData;

    void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    IEnumerator Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return StartCoroutine(LocationService());
        }

        this.LoadMuseumJson();

        SceneManager.LoadScene("MenuScene");
        //yield return null;
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

    void LoadMuseumJson()
    {
        string dataPath = "AR_TEAM/MuseumsData";
        TextAsset json = Resources.Load<TextAsset>(dataPath);

        museumData = JsonUtility.FromJson<MuseumArray>(json.text);
    }
}