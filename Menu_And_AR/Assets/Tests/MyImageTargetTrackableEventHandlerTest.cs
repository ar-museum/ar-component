using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class MyImageTargetTrackableEventHandlerTest
    {
        // test 1 Matei Lipan
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

        // test 2 Matei Lipan
        [UnityTest]
        public IEnumerator MyImageTargetTrackableEventHandler_CleanScreenAttachedInfo_Test()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            //Act
            var imageTargetTrackableEventHandler = GameObject.FindGameObjectWithTag("TargetManager").GetComponentInChildren<MyImageTargetTrackableEventHandler>();
            imageTargetTrackableEventHandler.CleanScreenAttachedInfo();
            var screenAttachedObjects =  ARDisplayTypeSwitcher.getScreenAttachedObjects();

            //Assert
            foreach (var screenAttachedObject in screenAttachedObjects)
            {
                var childrenTransform = screenAttachedObject.GetComponentsInChildren<Transform>();
                foreach( var childTransform in childrenTransform)
                {
                    Assert.IsTrue(childTransform.localScale == new Vector3(0, 0, 0));
                }
            }
        }
    }
}
