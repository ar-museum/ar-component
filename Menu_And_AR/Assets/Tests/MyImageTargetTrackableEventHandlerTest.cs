using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    // test 1 Matei Lipan
    public class MyImageTargetTrackableEventHandlerTest
    {
        [UnityTest]
        public IEnumerator MyImageTargetTrackableEventHandler_UpdateScreenAttachedInfo_Test()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            //Act
            var imageTargetTrackableEventHandler = GameObject.FindGameObjectWithTag("TargetManager").GetComponentInChildren<MyImageTargetTrackableEventHandler>();
            imageTargetTrackableEventHandler.UpdateScreenAttachedInfo();
            var setTexts = imageTargetTrackableEventHandler.GetComponents<SetText>();

            //Assert
            foreach(var setText in setTexts) {
                if (setText.textType == SetText.TextType.TopText)
                {
                    Assert.AreEqual(imageTargetTrackableEventHandler.GetTrackableName(), setText.GetText());
                }
            }
        }
    }
}
