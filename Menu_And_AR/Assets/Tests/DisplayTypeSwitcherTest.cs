using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Tests
{
    public class DisplayTypeSwitcherTest
    {
        // test 1
        private static DisplayType displayType = DisplayType.TargetAttached;


        private bool isButtonClicked = false;
        public void Clicked()
        {
            isButtonClicked = true;
        }

        [UnityTest]
        public IEnumerator GivenDisplayTypeTargetWhenToggleButtonIsPressedThenChangeDisplayType()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
            var expected_result = DisplayType.ScreenAttached;

            var buttonBackObject = GameObject.Find("ButtonToggle");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            //Act
            buttonBack.onClick.AddListener(Clicked);
            buttonBack.onClick.Invoke();
            if (isButtonClicked)
            {
                if (displayType == DisplayType.TargetAttached)
                    displayType = DisplayType.ScreenAttached;
                else
                    displayType = DisplayType.TargetAttached;
            }
            //Assert
            Assert.AreEqual(displayType, expected_result);
        }

        // test 2
        private enum DisplayType
        {
            TargetAttached,
            ScreenAttached
        }

        GameObject[] targetAttachedObjects = GameObject.FindGameObjectsWithTag("TargetAttached");
        
        [UnityTest]
        public IEnumerator GivenGetTargetAttachedObjectsWhenToggleButtonIsPressedThenTargetAttachedObjectsNotBeNull()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
            int result=0; int expected_result = 1;

            var buttonBackObject = GameObject.Find("ButtonToggle");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            //Act
            buttonBack.onClick.AddListener(Clicked);
            buttonBack.onClick.Invoke();
            if (isButtonClicked) {
                if (targetAttachedObjects != null)
                {
                    result = 1;
                }
                else result = 0;
            }
            //Assert
            Assert.AreEqual(result, expected_result);
        }

        // test 3

        GameObject[] screenAttachedObjects = GameObject.FindGameObjectsWithTag("ScreenAttached");

        [UnityTest]
        public IEnumerator GivenGetScreenAttachedObjectsWhenToggleButtonIsPressedThenTargetAttachedObjectsNotBeNull()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
            int result = 0; int expected_result = 1;
            displayType = DisplayType.ScreenAttached;

            var buttonBackObject = GameObject.Find("ButtonToggle");
            var buttonBack = buttonBackObject.GetComponent<Button>();

            //Act
            buttonBack.onClick.AddListener(Clicked);
            buttonBack.onClick.Invoke();
            if (isButtonClicked)
            {
                if (screenAttachedObjects != null)
                {
                    result = 1;
                }
                else result = 0;
            }
            //Assert
            Assert.AreEqual(result, expected_result);
        }
    }
}