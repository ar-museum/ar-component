using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class LocationTest
    {
        const string imagePath = "AR_TEAM/images/Museums/";
        public static MuseumArray museumData;

        [UnityTest]
        public IEnumerator Activate_Location_Test()
        {
            //Arrange
            LoadFindData.latitudine = -1;
            LoadFindData.longitudine = -1;
            LoadFindData.isUnitTest = true;
            Sprite imageNeeded = Resources.Load<Sprite>(imagePath + "No_Museum");
            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(1);

            bool rightLoader = false;

            //Act
            GameObject imageObject = GameObject.Find("BackgroundImage");
            GameObject textObject = GameObject.Find("TextBoxError/Text");
            if (imageObject != null && textObject != null)
            {
                Image image = imageObject.GetComponent<Image>();
                Text text = textObject.GetComponent<Text>();

                if (text.text == "Enable the location to let the application access the current coordinates of your position." && image.sprite == imageNeeded)
                {
                    GameObject buttonARObject = GameObject.Find("ButtonAR");
                    GameObject buttonGalleryObject = GameObject.Find("ButtonGallery");
                    GameObject buttonGamesObject = GameObject.Find("ButtonGames");

                    if (buttonARObject == null && buttonGalleryObject == null && buttonGamesObject == null)
                    {
                        rightLoader = true;
                    }
                }
            }

            //Assert
            Assert.True(rightLoader);
        }

        [UnityTest]
        public IEnumerator Location_Test_No_Museum()
        {
            //Arrange
            LoadFindData.latitudine = 1;
            LoadFindData.longitudine = 1;
            LoadFindData.isUnitTest = true;
            Sprite imageNeeded = Resources.Load<Sprite>(imagePath + "No_Museum");
            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(1);

            bool rightLoader = false;

            //Act
            GameObject imageObject = GameObject.Find("BackgroundImage");
            GameObject textObject = GameObject.Find("TextBoxMuseum/Text");
            if (imageObject != null && textObject != null)
            {
                Image image = imageObject.GetComponent<Image>();
                Text text = textObject.GetComponent<Text>();

                if (text.text == "You are not\n inside a museum" && image.sprite == imageNeeded)
                {
                    GameObject buttonARObject = GameObject.Find("ButtonAR");
                    GameObject buttonGalleryObject = GameObject.Find("ButtonGallery");
                    GameObject buttonGamesObject = GameObject.Find("ButtonGames");

                    if (buttonARObject == null && buttonGalleryObject && buttonGamesObject)
                    {
                        rightLoader = true;
                    }
                }
            }

            //Assert
            Assert.True(rightLoader);
        }

        void LoadMuseumJson()
        {
            string dataPath = "AR_TEAM/MuseumsData";
            TextAsset json = Resources.Load<TextAsset>(dataPath);

            museumData = JsonUtility.FromJson<MuseumArray>(json.text);
        }

        [UnityTest]
        public IEnumerator Location_Test_Literary_Museum()
        {
            //Arrange
            this.LoadMuseumJson();
            for (int i = 0; i < museumData.museums.Count; ++i)
            {
                if (museumData.museums[i].name.Equals("Muzeul de Literatura"))
                {
                    LoadFindData.latitudine = museumData.museums[i].latitude;
                    LoadFindData.longitudine = museumData.museums[i].longitude;
                }
            }
            LoadFindData.isUnitTest = true;
            Sprite imageNeeded = Resources.Load<Sprite>(imagePath + "Muzeul_de_Literatura");
            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(1);

            bool rightLoader = false;

            //Act
            GameObject imageObject = GameObject.Find("BackgroundImage");
            GameObject textObject = GameObject.Find("TextBoxMuseum/Text");
            if (imageObject != null && textObject != null)
            {
                Image image = imageObject.GetComponent<Image>();
                Text text = textObject.GetComponent<Text>();

                if (text.text == "Welcome to\nLiterary Museum" && image.sprite == imageNeeded)
                {
                    GameObject buttonARObject = GameObject.Find("ButtonAR");
                    GameObject buttonGalleryObject = GameObject.Find("ButtonGallery");
                    GameObject buttonGamesObject = GameObject.Find("ButtonGames");

                    if (buttonARObject && buttonGalleryObject && buttonGamesObject)
                    {
                        rightLoader = true;
                    }
                }
            }

            //Assert
            Assert.True(rightLoader);
        }

        [UnityTest]
        public IEnumerator Location_Test_Union_Museum()
        {
            //Arrange
            this.LoadMuseumJson();
            for (int i = 0; i < museumData.museums.Count; ++i)
            {
                if (museumData.museums[i].name.Equals("Muzeul Unirii"))
                {
                    LoadFindData.latitudine = museumData.museums[i].latitude;
                    LoadFindData.longitudine = museumData.museums[i].longitude;
                }
            }
            LoadFindData.isUnitTest = true;
            Sprite imageNeeded = Resources.Load<Sprite>(imagePath + "Muzeul_Unirii");
            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(1);

            bool rightLoader = false;

            //Act
            GameObject imageObject = GameObject.Find("BackgroundImage");
            GameObject textObject = GameObject.Find("TextBoxMuseum/Text");
            if (imageObject != null && textObject != null)
            {
                Image image = imageObject.GetComponent<Image>();
                Text text = textObject.GetComponent<Text>();

                if (text.text == "Welcome to\nUnion Museum" && image.sprite == imageNeeded)
                {
                    GameObject buttonARObject = GameObject.Find("ButtonAR");
                    GameObject buttonGalleryObject = GameObject.Find("ButtonGallery");
                    GameObject buttonGamesObject = GameObject.Find("ButtonGames");

                    if (buttonARObject && buttonGalleryObject && buttonGamesObject)
                    {
                        rightLoader = true;
                    }
                }
            }

            //Assert
            Assert.True(rightLoader);
        }

        [UnityTest]
        public IEnumerator Location_Test_Mihai_Eminescu_Museum()
        {
            //Arrange
            this.LoadMuseumJson();
            for (int i = 0; i < museumData.museums.Count; ++i)
            {
                if (museumData.museums[i].name.Equals("Muzeul Mihai Eminescu"))
                {
                    LoadFindData.latitudine = museumData.museums[i].latitude;
                    LoadFindData.longitudine = museumData.museums[i].longitude;
                }
            }
            LoadFindData.isUnitTest = true;
            Sprite imageNeeded = Resources.Load<Sprite>(imagePath + "Muzeul_Mihai_Eminescu");
            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(1);

            bool rightLoader = false;

            //Act
            GameObject imageObject = GameObject.Find("BackgroundImage");
            GameObject textObject = GameObject.Find("TextBoxMuseum/Text");
            if (imageObject != null && textObject != null)
            {
                Image image = imageObject.GetComponent<Image>();
                Text text = textObject.GetComponent<Text>();

                if (text.text == "Welcome to\nMihai Eminescu Museum" && image.sprite == imageNeeded)
                {
                    GameObject buttonARObject = GameObject.Find("ButtonAR");
                    GameObject buttonGalleryObject = GameObject.Find("ButtonGallery");
                    GameObject buttonGamesObject = GameObject.Find("ButtonGames");

                    if (buttonARObject && buttonGalleryObject && buttonGamesObject)
                    {
                        rightLoader = true;
                    }
                }
            }

            //Assert
            Assert.True(rightLoader);
        }

        [UnityTest]
        public IEnumerator Location_Test_National_History_Museum()
        {
            //Arrange
            this.LoadMuseumJson();
            for (int i = 0; i < museumData.museums.Count; ++i)
            {
                if (museumData.museums[i].name.Equals("Muzeul de Istorie Naturala"))
                {
                    LoadFindData.latitudine = museumData.museums[i].latitude;
                    LoadFindData.longitudine = museumData.museums[i].longitude;
                }
            }
            LoadFindData.isUnitTest = true;
            Sprite imageNeeded = Resources.Load<Sprite>(imagePath + "Muzeul_de_Istorie_Naturala");
            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(1);

            bool rightLoader = false;

            //Act
            GameObject imageObject = GameObject.Find("BackgroundImage");
            GameObject textObject = GameObject.Find("TextBoxMuseum/Text");
            if (imageObject != null && textObject != null)
            {
                Image image = imageObject.GetComponent<Image>();
                Text text = textObject.GetComponent<Text>();

                if (text.text == "Welcome to\nNational History Museum" && image.sprite == imageNeeded)
                {
                    GameObject buttonARObject = GameObject.Find("ButtonAR");
                    GameObject buttonGalleryObject = GameObject.Find("ButtonGallery");
                    GameObject buttonGamesObject = GameObject.Find("ButtonGames");

                    if (buttonARObject && buttonGalleryObject && buttonGamesObject)
                    {
                        rightLoader = true;
                    }
                }
            }

            //Assert
            Assert.True(rightLoader);
        }
    }
}