using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationBehaviourScript : MonoBehaviour
{


    public GameObject text0;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;


    public GameObject noMuseum;
    public GameObject museulDeLiteratura;
    public GameObject museulUniri;

    static double latitudine = 0;
    static double longitudine = 0;

    ArrayList museumsList = new ArrayList();
    List<GameObject> textObjectList = new List<GameObject>();

    void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        textObjectList.Add(text0);
        textObjectList.Add(text1);
        textObjectList.Add(text2);
        textObjectList.Add(text3);

        text0.SetActive(false);
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);

        noMuseum.SetActive(false);
        museulDeLiteratura.SetActive(false);
        museulUniri.SetActive(false);

        museumsList.Add(new Museum("Muzeul de Literatura", 47.172032, 27.576216, 1, museulDeLiteratura));
        museumsList.Add(new Museum("Muzeul Unirii", 47.167430, 27.578895, 2, museulUniri));


    }

    IEnumerator StartLocationService()
    {

        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation)) // aplicatia are acces la locatie
        {
                if (latitudine == 0 && longitudine == 0)
                {
                    yield return StartCoroutine(LocationService());
                }
                else
                {
                    yield return StartCoroutine(SetText());
                }
            }
        else  // aplicatia nu are acces la locatie
        {
            textObjectList[0].SetActive(true);
        }
    }


    IEnumerator SetText()
    {
        bool gasit = false;
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            foreach (Museum muzeu in museumsList)
            {
                if (Math.Abs(latitudine - muzeu.getLatitude()) < 0.00001 && Math.Abs(longitudine - muzeu.getLongitude()) < 0.00001)
                {
                    string tagCautat = "Text" + muzeu.getTextNumber();

                    foreach(GameObject obiectText in textObjectList)
                    {
                        if (obiectText.name.Equals(tagCautat))
                        {
                            obiectText.SetActive(true);
                            muzeu.getMuseumImage().SetActive(true);
                            gasit = true;
                        }
                    }
                }
            }
            if (!gasit) // alta locatie
            {
                textObjectList[1].SetActive(true);
                noMuseum.SetActive(true);
            }
        }
        yield return null;
    }

    IEnumerator LocationService()
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
            backroundMuzeulLiteraturi.SetActive(true);
            backroundMuzeulUnirii.SetActive(false);
            backroundNoMuzeum.SetActive(false);
        }
        else if (Math.Abs(latitudine - 47.167430) < 0.00001 && Math.Abs(longitudine - 27.578895) < 0.00001) // Muzeul Unirii
        {
            img1.SetActive(false);
            img2.SetActive(false);
            img3.SetActive(true);
            backroundMuzeulLiteraturi.SetActive(false);
            backroundMuzeulUnirii.SetActive(true);
            backroundNoMuzeum.SetActive(false);
        }
        else
        {
            img1.SetActive(true);
            img2.SetActive(false);
            img3.SetActive(false);
            backroundMuzeulLiteraturi.SetActive(false);
            backroundMuzeulUnirii.SetActive(false);
            backroundNoMuzeum.SetActive(true);
        }
    }

    public double getLatitude()
    {
        return this.latitudine;
    }

    public double getLongitude()
    {
        return this.longitudine;
    }
}