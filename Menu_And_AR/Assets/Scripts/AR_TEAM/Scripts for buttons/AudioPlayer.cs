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
    private string filename = "";
    private string path;
    private string song;

    IEnumerator ienum;

    void Start()
    {
        path = "file://" + Application.streamingAssetsPath + '/';
        if (Application.isMobilePlatform)
        {
            path = "jar:file://" + Application.dataPath + "!/assets/";
        }
        path += "Sound/";
        source = GetComponent<AudioSource>();
        ienum = LoadAudio();
    }

    // AudioPlayerTests test 1 test 2 test 3 test 4
    private IEnumerator LoadAudio()
    {
        WWW request = GetAudioFromFile(path, filename);
        yield return request;

        music = request.GetAudioClip();
        music.name = filename;

        PlayAudioFile();
        yield break;
    }

    // AudioPlayerTests test 1 test 2 test 3 test 4
    private void PlayAudioFile()
    {
        source.clip = music;
        source.time = playTime;
        source.Play();
    }

    // AudioPlayerTests test 1 test 2 test 3 test 4
    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }

    // AudioPlayerTests test 1 test 2 test 3 test 4
    public void PlayMusic(string songname)
    {
        if(source.isPlaying)
        {
            // test 2 test 3 test 4
            StopMusic();
        }
        else
        {
            // test 1 test 2 test 3 test 4
            ChangePlayButton();
            song = songname;
            filename = songname + ".wav";
            StopCoroutine(ienum);
            ienum = LoadAudio();
            StartCoroutine(ienum);
            btn_Play.image.overrideSprite = pauseButton;
        }
        
    }
    // AudioPlayerTests test 2 test 3 test 4
    public void StopMusic()
    {
        playTime = source.time;
        source.Stop();
        ChangePlayButton();
        StopCoroutine(ienum);
        btn_Play.image.overrideSprite = playButton;
    }
    // AudioPlayerTests test 3 test 4
    public void PlayMusic()
    {
        if(filename != "")
        {
            PlayMusic(song);
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
        if(filename != "")
        {
            source.Stop();
            source.time = 0;
            source.Play();
            btn_Play.image.overrideSprite = pauseButton;
        }
    }
}