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
        public IEnumerator GivenARScenWhenToggleButtonIsPressedThenImageMustChange()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
          

            var buttonToggle = GameObject.Find("ButtonToggle");
            var togglebutton = buttonToggle.GetComponent<Button>();

            isButtonClicked = false;
            //Act
   
            togglebutton.onClick.AddListener(Clicked);
            togglebutton.onClick.Invoke();

            Assert.True(isButtonClicked);
        }
        [UnityTest]
        public IEnumerator GivenARScenWhenAudioButtonIsPressedThenImageMustChange()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);


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
