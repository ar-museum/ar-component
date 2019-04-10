using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TargetManagerTests
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TargetManager_LoadDatabase_WrongDatabase_Test()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            //Arange
            SceneManager.LoadScene("ARScene");
                
            yield return new WaitForSeconds(1);

            var targetManagerObject = GameObject.FindGameObjectWithTag("TargetManager");
            var targetManager = targetManagerObject.gameObject.GetComponent(typeof(TargetManager)) as TargetManager;
            //Act

            //Assert
            Assert.Throws<System.ArgumentException>(() =>
            {
                targetManager.LoadDatabase("");
            });

            yield return null;
        }

        [UnityTest]
        public IEnumerator TargetManager_LoadDatabase_RightDatabase_Test()
        {
            //Arange
            SceneManager.LoadScene("ARScene");

            yield return new WaitForSeconds(1);

            var targetManagerObject = GameObject.FindGameObjectWithTag("TargetManager");
            var targetManager = targetManagerObject.gameObject.GetComponent(typeof(TargetManager)) as TargetManager;
            string databaseName = "Muzeul-Mihai-Eminescu"; // or something correct

            //Act
         
            //Assert
            Assert.DoesNotThrow(() =>
            {
                targetManager.LoadDatabase(databaseName);
            });

            yield return null;
        }
    }
}
