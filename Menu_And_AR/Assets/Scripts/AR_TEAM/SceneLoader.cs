using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneLoader : MonoBehaviour
{
    Scene currentScene;

    float pausedTime;
    double oldLatitude, oldLongitude, newLatitude, newLongitude;
    const double pi = 3.141592653589793;
    const double radius = 6371;
    const double distanceMuseum = 0.05; //50 m

    static ArrayList sceneStack = new ArrayList();

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                string sceneName = currentScene.name;
                if (string.Compare(sceneName, "MenuScene") == 0)
                {
                    Application.Quit();
                }
                else
                {
                    if (string.Compare(currentScene.name, "Menu") == 0)
                    {
                        GameObject music = GameObject.FindGameObjectWithTag("music");
                        Destroy(music);
                    }

                    int backSceneIndex = (int)sceneStack[sceneStack.Count - 1];
                    sceneStack.RemoveAt(sceneStack.Count - 1);
                    SceneManager.LoadScene(backSceneIndex);
                }
            }
        }
    }

    IEnumerator OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            pausedTime = Time.realtimeSinceStartup;
            oldLatitude = LoadFindData.latitudine;
            oldLongitude = LoadFindData.longitudine;
        }
        else
        {
            yield return LocationService();

            if (!verifyLocation(oldLatitude, oldLongitude))
            {
                SceneManager.LoadScene("PreloadScene");
            }
            else
            {
                if ((Time.realtimeSinceStartup - pausedTime > 30f) && (string.Compare(currentScene.name, "MenuScene") != 0))
                {
                    SceneManager.LoadScene("MenuScene");
                }
            }
        }
    }

    public void LoadBackScene()
    {
        if (sceneStack.Count > 0)
        {
            if (string.Compare(currentScene.name, "Menu") == 0)
            {
                GameObject music = GameObject.FindGameObjectWithTag("music");
                Destroy(music);
            }

            int backSceneIndex = (int)sceneStack[sceneStack.Count - 1];
            sceneStack.RemoveAt(sceneStack.Count - 1);
            SceneManager.LoadScene(backSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void LoadBackToMenuScene()
    {
        if (string.Compare(currentScene.name, "Menu") == 0)
        {
            GameObject music = GameObject.FindGameObjectWithTag("music");
            Destroy(music);
        }
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadNextScene(string sceneCamera)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneCamera))
        {
            sceneStack.Add(currentScene.buildIndex);
            SceneManager.LoadScene(sceneCamera);
        }
        else
        {
            throw new System.ArgumentException("Invalid argument.", "sceneCamera");
        }
    }

    public bool verifyLocation(double latitude, double longitude)
    {
        double latitudeUserRad = degreesToRadians(newLatitude);
        double latitudeLocationRad = degreesToRadians(latitude);

        double distanceLatitudeRad = degreesToRadians(newLatitude - latitude);
        double distanceLongitudeRad = degreesToRadians(newLongitude - longitude);

        double value = Math.Sin(distanceLatitudeRad / 2) * Math.Sin(distanceLatitudeRad / 2) + Math.Cos(latitudeUserRad) * Math.Cos(latitudeLocationRad) * Math.Sin(distanceLongitudeRad / 2) * Math.Sin(distanceLongitudeRad / 2);
        double result = 2 * radius * Math.Atan2(Math.Sqrt(value), Math.Sqrt(1 - value));

        if (result <= distanceMuseum) return true;
        else return false;
    }

    public double degreesToRadians(double value)
    {
        return value * pi / 180.0;
    }

    IEnumerator LocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            print("Utlizatorul nu a activat GPS-ul.");
            newLatitude = -1;
            newLongitude = -1;
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timpul a expirat.");
            newLatitude = -1;
            newLongitude = -1;
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Nu s-a putut determina locatia device-ului.");
            newLatitude = -1;
            newLongitude = -1;
            yield break;
        }
        else
        {
            newLatitude = Input.location.lastData.latitude;
            newLongitude = Input.location.lastData.longitude;
        }

        Input.location.Stop();
    }
}
