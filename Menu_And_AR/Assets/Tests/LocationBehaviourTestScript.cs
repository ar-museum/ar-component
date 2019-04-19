﻿using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class LocationBehaviourTestScript
    {
        [UnityTest]
        public IEnumerator Image_Based_On_Location_Test()
        {
            //Arrange
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            var controlScriptObject = GameObject.FindGameObjectWithTag("SceneControl");
            var controlScript = controlScriptObject.gameObject.GetComponent(typeof(LocationBehaviourScript)) as LocationBehaviourScript;

            double latitudine = controlScript.getLatitude();
            double longitudine = controlScript.getLongitude();

            bool rightImage = false;

            //Act
            if (Math.Abs(latitudine - 47.172032) < 0.00001 && Math.Abs(longitudine - 27.576216) < 0.00001) // Muzeul de Literatura
            {
                var image2Object = GameObject.FindGameObjectWithTag("Image2");
                if (image2Object != null)
                {
                    var image2 = image2Object.GetComponent<Image>();
                    if (image2.IsActive())
                    {
                        rightImage = true;
                    }
                }
            }
            else if (Math.Abs(latitudine - 47.167430) < 0.00001 && Math.Abs(longitudine - 27.578895) < 0.00001) // Muzeul Unirii
            {
                var image3Object = GameObject.FindGameObjectWithTag("Image3");
                if (image3Object != null)
                {
                    var image3 = image3Object.GetComponent<Image>();
                    if (image3.IsActive())
                    {
                        rightImage = true;
                    }
                }
            }
            else
            {
                var image1Object = GameObject.Find("Image1");
                if (image1Object != null)
                {
                    var image1 = image1Object.GetComponent<Image>();
                    if (image1.IsActive())
                    {
                        rightImage = true;
                    }
                }
            }

            //Assert
            Assert.True(rightImage);
        }

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
            //latitude = 47.167430;
            //longitude = 27.578895;
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
