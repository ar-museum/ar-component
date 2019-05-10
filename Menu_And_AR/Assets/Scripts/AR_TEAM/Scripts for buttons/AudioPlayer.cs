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
    public int counter = 0;
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

    private IEnumerator LoadAudio()
    {
        WWW request = GetAudioFromFile(path, filename);
        yield return request;

        music = request.GetAudioClip();
        music.name = filename;

        PlayAudioFile();
        yield break;
    }

    private void PlayAudioFile()
    {
        source.clip = music;
        source.time = playTime;
        source.Play();
    }

    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }

    public void PlayMusic(string songname)
    {
        if(source.isPlaying)
        {
            
            playTime = source.time;
            source.Stop();
            ChangePlayButton();
            StopCoroutine(ienum);
            btn_Play.image.overrideSprite = playButton;
        }
        else
        {
            ChangePlayButton();
            song = songname;
            filename = songname + ".wav";
            StopCoroutine(ienum);
            ienum = LoadAudio();
            StartCoroutine(ienum);
            btn_Play.image.overrideSprite = pauseButton;
        }
        
    }

    public void StopMusic()
    {
        song = "";
        filename = "";
        source.Stop();
        source.time = 0;
        playTime = 0;
        StopCoroutine(ienum);
        if (btn_Play.image != null)
        {
            btn_Play.image.overrideSprite = playButton;
        }
    } 

    public void PlayMusic()
    {
        if(filename != "")
        {
            PlayMusic(song);
        }
    }

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