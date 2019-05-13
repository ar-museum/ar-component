using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts;

public class ChooseScene2 : MonoBehaviour
{
    public void changeIt2(int i)
    {
        SceneManager.LoadScene(i);
        Globals.exhibit = "op4";
    }
}
