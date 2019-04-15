using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneBehaviourScript : MonoBehaviour
{
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                string sceneName = currentScene.name;
                if (string.Compare(sceneName, "ARScene") == 0 || string.Compare(sceneName, "GalleryScene") == 0 || string.Compare(sceneName, "GamesScene") == 0)
                {
                    SceneManager.LoadScene("MenuScene");
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }

    public void LoadScene(string sceneCamera)
    {
        if (string.Compare(sceneCamera, "MenuScene") == 0 || string.Compare(sceneCamera, "ARScene") == 0 || string.Compare(sceneCamera, "GalleryScene") == 0 || string.Compare(sceneCamera, "GamesScene") == 0)
        {
            SceneManager.LoadScene(sceneCamera);
        }
        else
        {
            throw new System.ArgumentException("Invalid argument.", "sceneCamera");
        }
    }
}
