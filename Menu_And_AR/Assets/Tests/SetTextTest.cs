using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class SetTextTest
    {
        // test 1 Lipan Matei
        [UnityTest]
        public IEnumerator SetText_SetInfoForTextComponents_Test()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
            var screeenAttachedObjects = GameObject.FindGameObjectsWithTag("ScreenAttached");

            //Act
            foreach (var screenAttachedObject in screeenAttachedObjects)
            {
                var screenAttachedTexts = screenAttachedObject.GetComponentsInChildren<SetText>();
                // update text
                SetText.SetInfoForTextComponents(screenAttachedTexts, "test");
            }

            //Assert
            foreach (var screenAttachedObject in screeenAttachedObjects)
            {
                var screenAttachedTexts = screenAttachedObject.GetComponentsInChildren<SetText>();
                foreach( var screenAttachedText in screenAttachedTexts)
                {
                    if(screenAttachedText.GetTextType() == SetText.TextType.TopText)
                    {
                        Assert.AreEqual("test", screenAttachedText.GetText());
                    }
                    else
                    {
                        Assert.AreEqual("test's author", screenAttachedText.GetText());
                    }
                }
            }

        }
    }
}
