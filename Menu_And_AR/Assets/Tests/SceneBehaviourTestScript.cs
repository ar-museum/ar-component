using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class SceneBehaviourTestScript
    {
        private bool isButtonClicked = false;
        private string sceneToLoad = null;

        [UnityTest]
        public IEnumerator MenuScene_ARButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            var buttonARObject = GameObject.Find("ButtonAR");
            var buttonAR = buttonARObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneBehaviourScript)) as SceneBehaviourScript;

            isButtonClicked = false;

            //Act
            buttonAR.onClick.AddListener(() => { ClickButton("ARScene"); } );
            yield return new WaitForSeconds(5);


            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadScene(sceneToLoad); });
            }
        }

        [UnityTest]
        public IEnumerator MenuScene_GalleryButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            var buttonGalleryObject = GameObject.Find("ButtonGallery");
            var buttonGallery = buttonGalleryObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneBehaviourScript)) as SceneBehaviourScript;

            isButtonClicked = false;

            //Act
            buttonGallery.onClick.AddListener(() => { ClickButton("GalleryScene"); });
            yield return new WaitForSeconds(5);

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadScene(sceneToLoad); });
            }
        }

        [UnityTest]
        public IEnumerator MenuScene_GamesButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            var buttonGamesObject = GameObject.Find("ButtonGames");
            var buttonGames = buttonGamesObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneBehaviourScript)) as SceneBehaviourScript;

            isButtonClicked = false;

            //Act
            buttonGames.onClick.AddListener(() => { ClickButton("GamesScene"); });
            yield return new WaitForSeconds(5);

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadScene(sceneToLoad); });
            }
        }

        [UnityTest]
        public IEnumerator ARScene_BackButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            var buttonBackObject = GameObject.Find("ButtonBack");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneBehaviourScript)) as SceneBehaviourScript;

            isButtonClicked = false;

            //Act
            buttonBack.onClick.AddListener(() => { ClickButton("MenuScene"); });
            yield return new WaitForSeconds(5);

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadScene(sceneToLoad); });
            }
        }

        [UnityTest]
        public IEnumerator GalleryScene_BackButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("GalleryScene");
            yield return new WaitForSeconds(1);

            var buttonBackObject = GameObject.Find("ButtonBack");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneBehaviourScript)) as SceneBehaviourScript;

            isButtonClicked = false;

            //Act
            buttonBack.onClick.AddListener(() => { ClickButton("MenuScene"); });
            yield return new WaitForSeconds(5);

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadScene(sceneToLoad); });
            }
        }

        [UnityTest]
        public IEnumerator GamesScene_BackButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("GamesScene");
            yield return new WaitForSeconds(1);

            var buttonBackObject = GameObject.Find("ButtonBack");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(SceneBehaviourScript)) as SceneBehaviourScript;

            isButtonClicked = false;

            //Act
            buttonBack.onClick.AddListener(() => { ClickButton("MenuScene"); });
            yield return new WaitForSeconds(5);

            //Assert
            Assert.True(isButtonClicked);
            if (isButtonClicked)
            {
                Assert.DoesNotThrow(() => { controlScript.LoadScene(sceneToLoad); });
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
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            var menuControlObject = GameObject.FindGameObjectWithTag("SceneControl");
            var menuControl = menuControlObject.gameObject.GetComponent(typeof(SceneBehaviourScript)) as SceneBehaviourScript;
            //Debug.Log(menuControl);
            sceneToLoad = "Scene";

            //Act

            //Assert
            Assert.Catch(typeof(System.ArgumentException), () => { menuControl.LoadScene(sceneToLoad); });
        }

        [UnityTest]
        public IEnumerator LoadScene_RightScene_Test()
        {
            //Arrange
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            var menuControlObject = GameObject.FindGameObjectWithTag("SceneControl");
            var menuControl = menuControlObject.gameObject.GetComponent(typeof(SceneBehaviourScript)) as SceneBehaviourScript;
            sceneToLoad = "ARScene";

            //Act

            //Assert
            Assert.DoesNotThrow(() => { menuControl.LoadScene(sceneToLoad); });
        }
    }
}
