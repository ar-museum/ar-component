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

        [UnityTest]
        public IEnumerator GivenARSceneAndBackButtonWhenPressedThenImageMustChange()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
          

            var buttonToggle = GameObject.Find("ButtonToggle");
            var togglebutton = buttonToggle.GetComponent<Button>();
            var expected_image = "toggleOff";
            isButtonClicked = false;
            int expected_result = 1;
            int result = 1;
            //Act
   
            togglebutton.onClick.Invoke();
            //yield return new WaitForSeconds(5);
            if (togglebutton.GetComponent<Image>().sprite.Equals(expected_image))
            {
                result = 1;
            }else result = 0;

            //Assert
            Assert.AreEqual(result, expected_result);
        }
    }
}
