using Assets.Scripts.AR_TEAM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public AudioClip music;

    private AudioSource source;
    public Button btn_Toggle;
    public Button btn_Play;

    public Sprite toggleOn;
    public Sprite toggleOff;
    public Sprite playButton;
    public Sprite pauseButton;
    public int counterPlay = 0;
    public int counter = 0;
    private float playTime = (float)0;


    void Start()
    {
        source = GetComponent<AudioSource>();
        //PlayMusic();
    }

    public void PlayMusic()
    {
        if(source.isPlaying)
        {
            
            playTime = source.time;
            source.Stop();
            ChangeMuteUnmute();

        }
        else
        {
            source.clip = music;
            source.Play();
            source.time = playTime;
            ChangePlayButton();
            ChangeMuteUnmute();
        }
        
    }

    IEnumerator WaitForMusicClip()
    {
        while(source.isPlaying)
        {
            yield return null;
        }
    }

    public void StopMusic()
    {
        source.Stop();
    } 

    public void MuteMusic()
    {
        source.mute = !source.mute;
    }
     

    public void ChangePlayButton()
    {
        
        counterPlay++;
        if (counterPlay % 2 == 0)
        {
            btn_Play.image.overrideSprite = pauseButton;
        }
        else
        {
            btn_Play.image.overrideSprite = playButton;
        }
        
    }
    
    // ARButtonsTest test 3
    public void ChangeMuteUnmute()
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