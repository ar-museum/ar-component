using UnityEngine;

public class SceneBehaviourProxy : MonoBehaviour
{
    private SceneBehaviourScript sceneManager;

    // SceneBehaviourProxyTest test 1
    // Start is called before the first frame update
    void Start()
    {
        GameObject sceneManagerGameObject = GameObject.FindGameObjectWithTag("SceneControl");
        if(sceneManagerGameObject != null)
        {
            sceneManager = sceneManagerGameObject.GetComponent<SceneBehaviourScript>();
        }
    }

    // SceneBehaviourProxyTest test 1
    public void LoadScene(string sceneCamera)
    {
        sceneManager.LoadScene(sceneCamera);
    }
}
