using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts;

public class ChooseScene1 : MonoBehaviour
{
    public void changeIt1(int i)
    {
        SceneManager.LoadScene(i);
        Globals.exhibit = "op3";
    }
}
