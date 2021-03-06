﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using Assets.Scripts.AR_TEAM.Http;

public class SettingsBasedOnLocation : MonoBehaviour
{
    const string imagePath = "AR_TEAM/images/Museums/No_Museum";
    string textAllowLocation_ro = "Permiteti aplicatiei sa aiba acces la locatia dumneavoastra astfel incat sa puteti fi localizat.";
    string textAllowLocation_eng = "Allow the application to access your location so you can be located.";
    string textActivateLocation_ro = "Activati locatia pentru a putea permite aplicatiei sa acceseze coordonatele actuale ale pozitiei dumneavoastra.";
    string textActivateLocation_eng = "Enable the location to let the application access the current coordinates of your position.";
    string textWelcome_ro = "Bine ati venit la\n";
    string textWelcome_eng = "Welcome to\n";
    string textNoMuseum_ro = "Nu va aflati in\n incinta unui muzeu";
    string textNoMuseum_eng = "You are not\n inside a museum";

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
                Sprite image = Resources.Load<Sprite>(imagePath);
                GameObject background = GameObject.Find("BackgroundImage");
                background.GetComponent<Image>().sprite = image;

                textBoxErrorObject.SetActive(true);
                GameObject textErrorObject = GameObject.Find("TextBoxError/Text");
                Text textError = textErrorObject.GetComponent<Text>();
                textError.text = textActivateLocation_ro; // _eng nu avem in engleza in baza de date
            }
            else
            {
                MuseumDto museum = MuseumManager.Instance.CurrentMuseum;
                if (museum != null)
                {
                    //sent museum id to games part
                    PlayerPrefs.SetInt("Games_Museum", museum.MuseumId);
                    
                    Sprite image = Resources.Load<Sprite>(museum.PhotoPath);
                    if (image == null)
                    {
                        image = Resources.Load<Sprite>(imagePath);
                    }
                    GameObject background = GameObject.Find("BackgroundImage");
                    background.GetComponent<Image>().sprite = image;

                    textBoxMuseumObject.SetActive(true);
                    GameObject textMuseumObject = GameObject.Find("TextBoxMuseum/Text");
                    Text textMuseum = textMuseumObject.GetComponent<Text>();
                    // nu avem nume in engleza in baza de date
                    // textMuseum.text = textWelcome_eng + LoadFindData.museumData.museums[i].name_eng;
                    textMuseum.text = textWelcome_ro + museum.Name;

                    if (museum.Expositions.Count > 0)
                    {
                        buttonARObject.SetActive(true);
                        buttonGalleryObject.SetActive(true);
                    }
                    buttonGamesObject.SetActive(true);
                }
                else // alta locatie
                {
                    Sprite image = Resources.Load<Sprite>(imagePath);
                    GameObject background = GameObject.Find("BackgroundImage");
                    background.GetComponent<Image>().sprite = image;

                    textBoxErrorObject.SetActive(true);
                    GameObject textMuseumObject = GameObject.Find("TextBoxError/Text");
                    Text textMuseum = textMuseumObject.GetComponent<Text>();
                    textMuseum.text = textNoMuseum_ro;// _eng nu avem in engleza in baza de date 

                }
            }
        }
        else // nu este permis accesul la locatie
        {
            Sprite image = Resources.Load<Sprite>(imagePath);
            GameObject background = GameObject.Find("BackgroundImage");
            background.GetComponent<Image>().sprite = image;

            textBoxErrorObject.SetActive(true);
            GameObject textErrorObject = GameObject.Find("TextBoxError/Text");
            Text textError = textErrorObject.GetComponent<Text>();
            textError.text = textAllowLocation_ro; // _eng nu avem in engleza in baza de date
        }
    }
}