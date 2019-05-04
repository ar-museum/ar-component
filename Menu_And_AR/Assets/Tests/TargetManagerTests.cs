using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Vuforia;
using System.Linq;

namespace Tests
{
    public class TargetManagerTests
    {
        [UnityTest]
        public IEnumerator TargetManager_LoadDatabase_WrongDatabase_Test()
        {
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
            string databaseName = "ARMuseum"; // or something correct

            //Act

            //Assert
            Assert.DoesNotThrow(() =>
            {
                targetManager.LoadDatabase(databaseName);
            });

            yield return null;
        }

        [UnityTest]
        public IEnumerator TargetManager_DoAfterVuforiaStarted_RightNumberOfTrackedObjects_Test()
        {

            // Arrange
            SceneManager.LoadScene("ARScene");

            yield return new WaitForSeconds(1);

            var targetManagerObject = GameObject.FindGameObjectWithTag("TargetManager");
            var targetManager = targetManagerObject.gameObject.GetComponent(typeof(TargetManager)) as TargetManager;

            // Act
            int numberOfChildren = targetManager.transform.childCount;
            int correctNumber = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours().ToList().Count;

            // Assert
            Assert.AreEqual(numberOfChildren, correctNumber);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TargetManager_DeleteTargets_DeletesAll_Test()
        {
            // Arrange
            SceneManager.LoadScene("ARScene");

            yield return new WaitForSeconds(1);

            var targetManagerObject = GameObject.FindGameObjectWithTag("TargetManager");
            var targetManager = targetManagerObject.gameObject.GetComponent(typeof(TargetManager)) as TargetManager;

            // Act
            ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            objectTracker.Stop();
            targetManager.DeleteTargets(objectTracker);
            objectTracker.Start();

            yield return new WaitForSeconds(1); // a frame needs to be skipped in order to delete the children

            int numberOfChildren = targetManager.transform.childCount;
            int numberOfTargetsInObjectTracker = 0;
            var activeDataSets = objectTracker.GetActiveDataSets().ToList();
            foreach (var dataset in activeDataSets)
            {
                numberOfTargetsInObjectTracker += dataset.GetTrackables().ToList().Count;
            }

            // Assert
            Assert.AreEqual(0, numberOfChildren); // deletes all gameObjects in unity
            Assert.AreEqual(0, numberOfTargetsInObjectTracker); // deletes all trackers of the active datasets in objectTracker

            yield return null;
        }

        [UnityTest]
        public IEnumerator TargetManager_DeleteTargets_ActiveObjectTracker_Test()
        {
            // Arrange
            SceneManager.LoadScene("ARScene");

            yield return new WaitForSeconds(1);

            var targetManagerObject = GameObject.FindGameObjectWithTag("TargetManager");
            var targetManager = targetManagerObject.gameObject.GetComponent(typeof(TargetManager)) as TargetManager;

            // Act
            ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            objectTracker.Start(); // activate the objectTracker

            // Assert
            Assert.Throws<System.ArgumentException>(() =>
            {
                targetManager.DeleteTargets(objectTracker); // tries to delete targets from an active objectTracker
            });
            yield return null;
        }
    }
}
