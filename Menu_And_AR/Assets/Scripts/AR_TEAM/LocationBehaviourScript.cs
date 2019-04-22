using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class LocationBehaviourScript : MonoBehaviour
{

    public GameObject text0;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    public GameObject imgNone;
    public GameObject imgUnirii;
    public GameObject imgLiteratura;

    //Sprite sp = Resources.Load("Assets/AR_TEAM/images/Museums/muzeul_unirii") as Sprite;

    public GameObject bg;

    static double latitudine = 0;
    static double longitudine = 0;

    ArrayList museumsList = new ArrayList();
    List<GameObject> textObjectList = new List<GameObject>();
    List<GameObject> imageObjectList = new List<GameObject>();

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

        imageObjectList.Add(imgNone);
        imageObjectList.Add(imgUnirii);
        imageObjectList.Add(imgLiteratura);

        imgUnirii.SetActive(false);
        imgLiteratura.SetActive(false);

        text0.SetActive(false);
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);

        museumsList.Add(new Museum("Muzeul de Literatura", 47.172032, 27.576216, 1, "imgLiteratura"));
        museumsList.Add(new Museum("Muzeul Unirii", 47.167430, 27.578895, 2, "imgUnirii"));

    }

    IEnumerator Start()
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
                    string numeCautat = muzeu.getImgName();

                    foreach(GameObject obiectText in textObjectList)
                    {
                        if (obiectText.name.Equals(tagCautat))
                        {
                            obiectText.SetActive(true);
                            gasit = true;
                        }
                        else
                            obiectText.SetActive(false);
                    }

                    foreach(GameObject imgObj in imageObjectList)
                    {
                        if (imgObj.name.Equals(numeCautat))
                        {
                            imgObj.SetActive(true);
                            gasit = true;
                        }
                        else
                            imgObj.SetActive(false);
                    }
                }
            }
            if (!gasit) // alta locatie
            {
                imgNone.SetActive(true);
                textObjectList[1].SetActive(true);
            }
        }
        yield return null;
    }

    IEnumerator LocationService()
    {

        if (!Input.location.isEnabledByUser)
        {
            print("Utlizatorul nu a activat GPS-ul.");
            latitudine = -1;
            longitudine = -1;
            yield return StartCoroutine(SetText());
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
            yield return StartCoroutine(SetText());
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Nu s-a putut determina locatia device-ului.");
            latitudine = -1;
            longitudine = -1;
            yield return StartCoroutine(SetText());
            yield break;
        }
        else
        {
            latitudine = Input.location.lastData.latitude;
            longitudine = Input.location.lastData.longitude;
            yield return StartCoroutine(SetText());
        }

        Input.location.Stop();
    }

    public double getLatitude()
    {
        return latitudine;
    }

    public double getLongitude()
    {
        return longitudine;
    }
}