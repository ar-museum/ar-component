using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;

namespace Tests
{
    public class SetTextTest
    {

        // test 1 Lipan Matei
        [UnityTest]
        public IEnumerator SetText_SetInfoForTextComponents_Test()
        {
            //Arrange
            LoadFindData.isUnitTest = false;
            SceneManager.LoadScene("PreloadScene");
            yield return new WaitForSeconds(10);
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
            var screeenAttachedObjects = GameObject.FindGameObjectsWithTag("ScreenAttached");
            var (title, author, authorId) = MuseumManager.Instance.CurrentMuseum.FindArSceneInfoByExhibitId(1); // un id valid

            //Act
            foreach (var screenAttachedObject in screeenAttachedObjects)
            {
                var screenAttachedTexts = screenAttachedObject.GetComponentsInChildren<SetText>();
                // update text
                SetText.SetInfoForTextComponents(screenAttachedTexts, "1");
            }

            //Assert
            foreach (var screenAttachedObject in screeenAttachedObjects)
            {
                var screenAttachedTexts = screenAttachedObject.GetComponentsInChildren<SetText>();
                foreach( var screenAttachedText in screenAttachedTexts)
                {
                    if(screenAttachedText.GetTextType() == SetText.TextType.TopText)
                    {
                        Assert.AreEqual(title, screenAttachedText.GetText());
                    }
                    else
                    {
                        Assert.AreEqual(author, screenAttachedText.GetText());
                    }
                }
            }

        }
    }
}
