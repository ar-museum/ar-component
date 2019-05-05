using UnityEngine;

public class SceneBehaviourProxy : MonoBehaviour
{
    private SceneLoader sceneManager;

    // SceneBehaviourProxyTest test 1
    // Start is called before the first frame update
    void Start()
    {
        GameObject sceneManagerGameObject = GameObject.FindGameObjectWithTag("SceneControl");
        if(sceneManagerGameObject != null)
        {
            sceneManager = sceneManagerGameObject.GetComponent<SceneLoader>();
        }
    }

    // SceneBehaviourProxyTest test 1
    public void LoadScene(string sceneCamera)
    {
        sceneManager.LoadNextScene(sceneCamera);
    }
}
