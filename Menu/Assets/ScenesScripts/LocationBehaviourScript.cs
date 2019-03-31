using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationBehaviourScript : MonoBehaviour
{

    public GameObject img1;
    public GameObject img2;
    public GameObject img3;
    double latitudine;
    double longitudine;
    
    void Start()
    {
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            print("Utlizatorul nu a activat GPS-ul.");
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
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Nu s-a putut determina locatia device-ului.");
            yield break;
        }
        else
        {
            latitudine = Input.location.lastData.latitude;
            longitudine = Input.location.lastData.longitude;
        }

        Input.location.Stop();
    }

    void Update()
    {
        if (Math.Abs(latitudine - 47.172032) < 0.00001 && Math.Abs(longitudine - 27.576216) < 0.00001) // Muzeul de Literatura
        {
            img1.SetActive(false);
            img2.SetActive(true);
            img3.SetActive(false);
        }
        else if (Math.Abs(latitudine - 47.167430) < 0.00001 && Math.Abs(longitudine - 27.578895) < 0.00001) // Muzeul Unirii
        {
            img1.SetActive(false);
            img2.SetActive(false);
            img3.SetActive(true);
        }
        else
        {
            img1.SetActive(true);
            img2.SetActive(false);
            img3.SetActive(false);
        }
    }
}