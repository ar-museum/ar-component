﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class SceneLoaderTest
    {
        private bool isButtonClicked = false;
        private string sceneToLoad = null;

        [UnityTest]
        public IEnumerator MenuScene_ARButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetSceneByName("PreloadScene").isLoaded == false)
            {
                yield return new WaitForSeconds(1);
            }
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            var buttonARObject = GameObject.Find("ButtonAR");
            var buttonAR = buttonARObject.GetComponent<Button>();
            Debug.Log(buttonAR);

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneLoader)) as SceneLoader;
            Debug.Log(controlScript);

            isButtonClicked = false;

            //Act
            buttonAR.onClick.AddListener(() => { ClickButton("ARScene"); });
            buttonAR.onClick.Invoke();

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadNextScene(sceneToLoad); });
            }
        }

        [UnityTest]
        public IEnumerator MenuScene_GalleryButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetSceneByName("PreloadScene").isLoaded == false)
            {
                yield return new WaitForSeconds(1);
            }
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            var buttonGalleryObject = GameObject.Find("ButtonGallery");
            var buttonGallery = buttonGalleryObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneLoader)) as SceneLoader;

            isButtonClicked = false;

            //Act
            buttonGallery.onClick.AddListener(() => { ClickButton("GalleryScene"); });
            buttonGallery.onClick.Invoke();

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadNextScene(sceneToLoad); });
            }
        }

        [UnityTest]
        public IEnumerator MenuScene_GamesButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetSceneByName("PreloadScene").isLoaded == false)
            {
                yield return new WaitForSeconds(1);
            }
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            var buttonGamesObject = GameObject.Find("ButtonGames");
            var buttonGames = buttonGamesObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneLoader)) as SceneLoader;

            isButtonClicked = false;

            //Act
            buttonGames.onClick.AddListener(() => { ClickButton("Menu"); });
            buttonGames.onClick.Invoke();

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadNextScene(sceneToLoad); });
            }
        }

        [UnityTest]
        public IEnumerator ARScene_BackButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetSceneByName("PreloadScene").isLoaded == false)
            {
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            var buttonBackObject = GameObject.Find("ButtonBack");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneLoader)) as SceneLoader;

            isButtonClicked = false;

            //Act
            buttonBack.onClick.AddListener(() => { ClickButton(""); });
            buttonBack.onClick.Invoke();

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadBackScene(); });
            }
        }

        // Galeria nu are buton de back
        /*[UnityTest]
        public IEnumerator GalleryScene_BackButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("GalleryScene");
            yield return new WaitForSeconds(1);

            var buttonBackObject = GameObject.Find("ButtonBack");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneLoader)) as SceneLoader;

            isButtonClicked = false;

            //Act
            buttonBack.onClick.AddListener(() => { ClickButton(""); });
            buttonBack.onClick.Invoke();

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadBackScene(); });
            }
        }*/

        [UnityTest]
        public IEnumerator GamesScene_BackButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetSceneByName("PreloadScene").isLoaded == false)
            {
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene("Menu");
            yield return new WaitForSeconds(1);
            

            var buttonBackObject = GameObject.Find("BackButton");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneLoader)) as SceneLoader;

            isButtonClicked = false;

            //Act
            buttonBack.onClick.AddListener(() => { ClickButton(""); });
            buttonBack.onClick.Invoke();

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadBackScene(); });
            }
        }

        private void ClickButton(string nextScene)
        {
            isButtonClicked = true;
            sceneToLoad = nextScene;
        }

        [UnityTest]
        public IEnumerator LoadScene_WrongScene_Test()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetSceneByName("PreloadScene").isLoaded == false)
            {
                yield return new WaitForSeconds(1);
            }
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            var menuControlObject = GameObject.FindGameObjectWithTag("SceneControl");
            var menuControl = menuControlObject.gameObject.GetComponent(typeof(SceneLoader)) as SceneLoader;
            sceneToLoad = "Scene";

            //Act

            //Assert
            Assert.Catch(typeof(System.ArgumentException), () => { menuControl.LoadNextScene(sceneToLoad); });
        }

        [UnityTest]
        public IEnumerator LoadScene_RightScene_Test()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetSceneByName("PreloadScene").isLoaded == false)
            {
                yield return new WaitForSeconds(1);
            }
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            var menuControlObject = GameObject.FindGameObjectWithTag("SceneControl");
            var menuControl = menuControlObject.gameObject.GetComponent(typeof(SceneLoader)) as SceneLoader;
            sceneToLoad = "ARScene";

            //Act

            //Assert
            Assert.DoesNotThrow(() => { menuControl.LoadNextScene(sceneToLoad); });
        }
    }
}
