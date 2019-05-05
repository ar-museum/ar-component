using UnityEngine;

public class SceneBehaviourProxy : MonoBehaviour
{
    private SceneLoader sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject sceneManagerGameObject = GameObject.FindGameObjectWithTag("SceneControl");
        if(sceneManagerGameObject != null)
        {
            sceneManager = sceneManagerGameObject.GetComponent<SceneLoader>();
        }
    }

    public void LoadScene(string sceneCamera)
    {
        sceneManager.LoadNextScene(sceneCamera);
    }
}
