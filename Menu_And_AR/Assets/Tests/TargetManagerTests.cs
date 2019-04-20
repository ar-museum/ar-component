using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Vuforia;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

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
            string databaseName = "Muzeul-Mihai-Eminescu"; // or something correct

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
    public class LocationBehaviorTests
    {
        [UnityTest]
        public IEnumerator LocationBehavior_MuzeulUnirii_LocationIsMuzeulUnirii_Test()
        {
            // Arrange
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            double expected_latitude = 47.167430;
            double expected_longitude = 27.578895;
            double latitude = 0;
            double longitude = 0;
            int expected_result = 1;
            int result;
            // Act
            Input.location.Start();
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            //latitude = 47.167430;
            //longitude = 27.578895;
            Input.location.Stop();

            if (Math.Abs(latitude - expected_latitude) < 0.00010 && Math.Abs(longitude - expected_longitude) < 0.00010)
            {
                result = 1;
            }
            else result = 0;
            // Assert
            Assert.AreEqual(expected_result, result);
        }

        [UnityTest]
        public IEnumerator LocationBehavior_MuzeulDeLiteratura_LocationIsMuzeulDeLiteratura_Test()
        {
            // Arrange
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            double expected_latitude = 47.172032;
            double expected_longitude = 27.576216;
            double latitude = 0;
            double longitude = 0;
            int expected_result = 1;
            int result;
            // Act
            Input.location.Start();
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            //latitude = 47.167430;
            //longitude = 27.578895;
            Input.location.Stop();
            if (Math.Abs(latitude - expected_latitude) < 0.00010 && Math.Abs(longitude - expected_longitude) < 0.00010)
            {
                result = 1;
            }
            else result = 0;
            // Assert
            Assert.AreEqual(expected_result, result);
        }

        [UnityTest]
        public IEnumerator LocationBehavior_DifferentLocation_LocationIsNotAccepted_Test()
        {
            // Arrange
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            double latitude = 0;
            double longitude = 0;
            int expected_result = 1;
            int result;
            // Act
            Input.location.Start();
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            Input.location.Stop();
            if (Math.Abs(latitude - 47.172032) != 0.00010 && Math.Abs(longitude - 27.576216) != 0.00010)
            {
                result = 1;
            }
            else result = 0;
            if (Math.Abs(latitude - 47.167430) != 0.00010 && Math.Abs(longitude - 27.578895) != 0.00010)
            {
                result = 1;
            }
            else result = 0;

            // Assert
            Assert.AreEqual(expected_result, result);
        }

    }
}
