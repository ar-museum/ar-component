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

    IEnumerator Start()
    {
        currentScene = SceneManager.GetActiveScene();
        return new HttpRequests().GetExhibits(OnExhibitLoaded);
    }

    void OnExhibitLoaded(List<Exhibit> exhibits) {
        Debug.Log(exhibits);
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
                    int backSceneIndex = (int) sceneStack[sceneStack.Count - 1];
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
        int backSceneIndex = (int) sceneStack[sceneStack.Count - 1];
        sceneStack.RemoveAt(sceneStack.Count - 1);
        SceneManager.LoadScene(backSceneIndex);
    }

    public void LoadNextScene(string sceneCamera)
    {
        if (string.Compare(sceneCamera, "MenuScene") == 0 || string.Compare(sceneCamera, "ARScene") == 0 || string.Compare(sceneCamera, "GalleryScene") == 0 || string.Compare(sceneCamera, "GamesScene") == 0)
        {
            sceneStack.Add(currentScene.buildIndex);
            SceneManager.LoadScene(sceneCamera);
        }
        else
        {
            throw new System.ArgumentException("Invalid argument.", "sceneCamera");
        }
    }
}
