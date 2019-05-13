using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts;

public class ChooseScene : MonoBehaviour
{
    public void changeIt(int i)
    {
        SceneManager.LoadScene(i);
        Globals.exhibit = "op2";
    }
}
