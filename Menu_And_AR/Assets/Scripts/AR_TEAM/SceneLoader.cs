using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.AR_TEAM.HttpRequests;
using Assets.Scripts.AR_TEAM.Http;

public class SceneLoader : MonoBehaviour
{
    Scene currentScene;
    float pausedTime;

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
                    int backSceneIndex = (int)sceneStack[sceneStack.Count - 1];
                    sceneStack.RemoveAt(sceneStack.Count - 1);
                    SceneManager.LoadScene(backSceneIndex);
                }
            }
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            pausedTime = Time.realtimeSinceStartup;
        }
        else
        {
            float waitingTime;
            string sceneName = currentScene.name;
            if (string.Compare(sceneName, "MenuScene") != 0)
            {
                waitingTime = 30f;
            }
            else
            {
                waitingTime = 10f;
            }
            if (Time.realtimeSinceStartup - pausedTime > waitingTime)
            {
                SceneManager.LoadScene("PreloadScene");
            }
        }
    }

    public void LoadBackScene()
    {
        if (sceneStack.Count > 0)
        {
            int backSceneIndex = (int)sceneStack[sceneStack.Count - 1];
            sceneStack.RemoveAt(sceneStack.Count - 1);
            SceneManager.LoadScene(backSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void LoadNextScene(string sceneCamera)
    {
        
        if (string.Compare(sceneCamera, "MenuScene") == 0 || string.Compare(sceneCamera, "ARScene") == 0 || string.Compare(sceneCamera, "GalleryScene") == 0 || string.Compare(sceneCamera, "GamesScene") == 0 || string.Compare(sceneCamera, "AuthorScene") == 0 || string.Compare(sceneCamera, "ExhibitScene") == 0)
        {
            sceneStack.Add(currentScene.buildIndex);
            SceneManager.LoadScene(sceneCamera);
            if(string.Compare(currentScene.name, "ARScene") == 0)
            {
                PlayerPrefs.SetString("author", "Mircea_Ispir");
                PlayerPrefs.SetString("exhibit", "Mirtil_si_Chloe");
               
            }
        }
        else
        {
            throw new System.ArgumentException("Invalid argument.", "sceneCamera");
        }
    }
}
