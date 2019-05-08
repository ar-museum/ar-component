using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Android;

public class SettingsBasedOnLocation : MonoBehaviour
{
    string imagePath = "AR_TEAM/images/Museums/";

    void Awake()
    {
        GameObject textBoxErrorObject = GameObject.Find("TextBoxError");
        textBoxErrorObject.SetActive(false);

        GameObject textBoxMuseumObject = GameObject.Find("TextBoxMuseum");
        textBoxMuseumObject.SetActive(false);

        GameObject buttonARObject = GameObject.Find("ButtonAR");
        buttonARObject.SetActive(false);

        GameObject buttonGalleryObject = GameObject.Find("ButtonGallery");
        buttonGalleryObject.SetActive(false);

        GameObject buttonGamesObject = GameObject.Find("ButtonGames");
        buttonGamesObject.SetActive(false);

        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (LoadFindData.latitudine == -1 && LoadFindData.longitudine == -1) // locatia nu este activata
            {
                Sprite image = Resources.Load<Sprite>(imagePath + "No_Museum");
                GameObject background = GameObject.Find("BackgroundImage");
                background.GetComponent<Image>().sprite = image;

                textBoxErrorObject.SetActive(true);
                GameObject textErrorObject = GameObject.Find("TextBoxError/Text");
                Text textError = textErrorObject.GetComponent<Text>();
                textError.text = "Activati locatia pentru a putea permite aplicatiei sa acceseze coordonatele actuale ale pozitiei dumneavoastra.";
            }
            else
            {
                bool gasit = false;
                for (int i = 0; i < LoadFindData.museumData.museums.Count; ++i)
                {
                    if (Math.Abs(LoadFindData.latitudine - LoadFindData.museumData.museums[i].latitude) < 0.001 && Math.Abs(LoadFindData.longitudine - LoadFindData.museumData.museums[i].longitude) < 0.001)
                    {
                        gasit = true;
                        Sprite image = Resources.Load <Sprite> (imagePath + LoadFindData.museumData.museums[i].name.Replace(" ","_"));
                        if(image == null)
                        {
                            image = Resources.Load<Sprite>(imagePath + "No_Museum");
                        }
                        GameObject background = GameObject.Find("BackgroundImage");
                        background.GetComponent<Image>().sprite = image;

                        textBoxMuseumObject.SetActive(true);
                        GameObject textMuseumObject = GameObject.Find("TextBoxMuseum/Text");
                        Text textMuseum = textMuseumObject.GetComponent<Text>();
                        textMuseum.text = "Bine ati venit la\n" + LoadFindData.museumData.museums[i].name;

                        buttonARObject.SetActive(true);
                        buttonGalleryObject.SetActive(true);
                        buttonGamesObject.SetActive(true);
                    }
                }
                if (!gasit) // alta locatie
                {
                    Sprite image = Resources.Load<Sprite>(imagePath + "No_Museum");
                    GameObject background = GameObject.Find("BackgroundImage");
                    background.GetComponent<Image>().sprite = image;

                    textBoxMuseumObject.SetActive(true);
                    GameObject textMuseumObject = GameObject.Find("TextBoxMuseum/Text");
                    Text textMuseum = textMuseumObject.GetComponent<Text>();
                    textMuseum.text = "Nu va aflati in\n incinta unui muzeu";

                    buttonGalleryObject.SetActive(true);
                    buttonGamesObject.SetActive(true);
                }
            }
        }
        else // nu este permis accesul la locatie
        {
            Sprite image = Resources.Load<Sprite>(imagePath + "No_Museum");
            GameObject background = GameObject.Find("BackgroundImage");
            background.GetComponent<Image>().sprite = image;

            textBoxErrorObject.SetActive(true);
            GameObject textErrorObject = GameObject.Find("TextBoxError/Text");
            Text textError = textErrorObject.GetComponent<Text>();
            textError.text = "Permiteti aplicatiei sa aiba acces la locatia dumneavoastra astfel incat sa puteti fi localizat.";
        }
    }

    public double getLatitude()
    {
        return LoadFindData.latitudine;
    }

    public double getLongitude()
    {
        return LoadFindData.longitudine;
    }
}