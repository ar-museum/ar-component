using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class SceneBehaviourProxyTest
    {
        // test 1 Lipan Matei
        [UnityTest]
        public IEnumerator MenuScene_ARButton_Press_Test()
        {
            //Arrange
            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            string sceneToLoad = "GalleryScene";
            var sceneBehaviourProxy = GameObject.FindGameObjectWithTag("TargetAttached").GetComponentInChildren<SceneBehaviourProxy>();

            //Act
            sceneBehaviourProxy.LoadScene(sceneToLoad);
            yield return new WaitForSeconds(1);

            //Assert
            Assert.AreEqual(sceneToLoad, SceneManager.GetActiveScene().name);
        }
    }
}
