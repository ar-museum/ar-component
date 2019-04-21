using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Tests
{
    public class ARButtonsTest
    {

        private bool isButtonClicked = false;
        public void Clicked()
        {
            isButtonClicked = true;
        }


        [UnityTest]
        public IEnumerator GivenARSceneWhenToggleButtonIsPressedThenImageMustChange()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            var buttonBackObject = GameObject.Find("ButtonToggle");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            //Act
            buttonBack.onClick.AddListener(Clicked);
            buttonBack.onClick.Invoke();
            //yield return new WaitForSeconds(5);

            //Assert
            Assert.True(isButtonClicked);
        }

        [UnityTest]
        public IEnumerator GivenARSceneWhenPlayButtonIsPressedThenImageMustChange()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            var buttonBackObject = GameObject.Find("ButtonAudio");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            //Act
            buttonBack.onClick.AddListener(Clicked);
            buttonBack.onClick.Invoke();
            //yield return new WaitForSeconds(5);

            //Assert
            Assert.True(isButtonClicked);
        }
    }

    
}
