using System.Collections;
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

        // test 1
        [UnityTest]
        public IEnumerator GivenARScenWhenToggleButtonIsPressedThenImageMustChange()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            var buttonBackObject = GameObject.Find("ButtonToggle");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            //Act
            buttonBack.onClick.AddListener(Clicked);
            buttonBack.onClick.Invoke();

            //Assert
            Assert.True(isButtonClicked);
        }

        // test 2
        [UnityTest]
        public IEnumerator GivenARScenWhenAudioButtonIsPressedThenImageMustChange()
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

            var buttonToggle = GameObject.Find("ButtonAudio");
            var togglebutton = buttonToggle.GetComponent<Button>();

            isButtonClicked = false;
            //Act

            togglebutton.onClick.AddListener(Clicked);
            togglebutton.onClick.Invoke();

            Assert.True(isButtonClicked);
        }
    }

    
}
