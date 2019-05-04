using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ToggleScript : MonoBehaviour
{
    public Button btn_Toggle;

    public Sprite toggleOn;
    public Sprite toggleOff;

    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void ChangeToggle()
    {
        counter++;
        if (counter % 2 == 0)
        {
            btn_Toggle.image.overrideSprite = toggleOn;
        }
        else
        {
            btn_Toggle.image.overrideSprite = toggleOff;
        }
    }
}
