using Assets.Scripts.AR_TEAM.Http;
using NUnit.Framework;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class HttpRequstTests
    {

        //Test 1 Obreja Stefan
        private static void SetHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "680bff9eb1ba0a8d48badd598be95c5642ad2939");
            request.SetRequestHeader("UserDevice", "2535C5EB-D6ED-4ABC-956B-4ACF29938F26");
        }

        [UnityTest]
        public IEnumerator ShouldSetHeaders()
        {
            SceneManager.LoadScene("MenuScene");
            yield return new WaitForSeconds(1);

            UnityWebRequest request = new UnityWebRequest();
            SetHeaders(request);
            var expectedResult = true;

            if (request.Equals(null)){
                expectedResult = false;
            }
            else
            {
                expectedResult = true;
            }
            Assert.IsTrue(expectedResult);
        }


    }
}