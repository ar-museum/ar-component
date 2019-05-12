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
    private AudioClip music;

    private AudioSource source;
    public Button btn_Toggle;
    public Button btn_Play;

    public Sprite playButton;
    public Sprite pauseButton;
    public int counterPlay = 0;
    private int playv = 0;
    private float playTime = (float)0;
    private string songPath = "";

    IEnumerator ienum;

    void Start()
    {
        
        source = GetComponent<AudioSource>();
        ienum = LoadAudio();
    }

    // AudioPlayerTests test 1 test 2 test 3 test 4 test 5
    private IEnumerator LoadAudio()
    {
        WWW request = new WWW("file://" + songPath);
        yield return request;

        music = request.GetAudioClip();
        music.name = songPath;

        PlayAudioFile();
        yield break;
    }

    // AudioPlayerTests test 1 test 2 test 3 test 4 test 5
    private void PlayAudioFile()
    {
        source.clip = music;
        source.time = playTime;
        source.Play();
    }

    // AudioPlayerTests test 1 test 2 test 3 test 4 test 5
    public void PlayMusic(string songPath)
    {
        if(source.isPlaying)
        {
            // test 2 test 3 test 4
            PauseMusic();
        }
        else
        {
            // test 1 test 2 test 3 test 4
            ChangePlayButton();
            this.songPath = songPath;
            StopCoroutine(ienum);
            ienum = LoadAudio();
            StartCoroutine(ienum);
            btn_Play.image.overrideSprite = pauseButton;
        }
        
    }
    // AudioPlayerTests test 2 test 3 test 4
    public void PauseMusic()
    {
        playTime = source.time;
        source.Stop();
        ChangePlayButton();
        StopCoroutine(ienum);
        btn_Play.image.overrideSprite = playButton;
    }
    // AudioPlayerTests test 5
    public void StopMusic()
    {
        source.Stop();
        source.time = 0;
        songPath = "";
        ChangePlayButton();
        StopCoroutine(ienum);
        btn_Play.image.overrideSprite = playButton;
    }

    // AudioPlayerTests test 3 test 4
    public void PlayMusic()
    {
        if(songPath != "")
        {
            PlayMusic(songPath);
        }
    }

    // ARButtonsTest test 2
    public void ChangePlayButton()
    {
        
        counterPlay = counterPlay + 1;
        if (counterPlay % 2 == 0)
        {
            btn_Play.image.overrideSprite = playButton;
        }
        else
        {
            btn_Play.image.overrideSprite = pauseButton;
            
        }
        
    }

    // AudioPlayerTests test 4
    public void ReplayMusic()
    {
        if(songPath != "")
        {
            source.Stop();
            source.time = 0;
            source.Play();
            btn_Play.image.overrideSprite = pauseButton;
        }
    }
}