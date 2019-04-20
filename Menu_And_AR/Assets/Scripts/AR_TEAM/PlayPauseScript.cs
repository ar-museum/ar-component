using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class PlayPauseScript : MonoBehaviour
{
    public Button btn_Play;
    public Sprite playButton;
    public Sprite pauseButton;

    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //btn_Play = GetComponents<Button>;
    }

    // Update is called once per frame
    public void ChangePlayButton()
    {
        counter++;
        if (counter % 2 == 1){
            btn_Play.image.overrideSprite = playButton;
        }
        else
        {
            btn_Play.image.overrideSprite = pauseButton;
        }
    }
}
