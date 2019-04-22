using Assets.Scripts.AR_TEAM;
using System;
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
    public MusicPlayer MusicPlayer { get; private set; }

    private bool IsPlaying { get; set; }

    public PlayPauseScript()
    {
        
    }

    public void Update() {
    }

    // Start is called before the first frame update
    void Start()
    {
        try {
            var path = @"D:\facultate\anul2sem2\ip2\repo\ar-component\Menu_And_AR\Assets\Resources\AR_TEAM\Music\Song.wav";
            MusicPlayer = gameObject.AddComponent<MusicPlayer>();
            var source = GetOrCreateSource();
            MusicPlayer.Set(path, source);
        } catch (Exception e) {
            Debug.Log("exception: " + e.ToString() + e.StackTrace.ToString());
        }
    }

    // Update is called once per frame
    public void ChangePlayButton()
    {
        IsPlaying = !IsPlaying;
        if (IsPlaying) 
        {
            btn_Play.image.overrideSprite = playButton;
            MusicPlayer.Toggle();

            //AudioSource audio = gameObject.AddComponent<AudioSource>();
            //audio.PlayOneShot((AudioClip)Resources.Load("AR_TEAM/Music/Song"));
        }
        else
        {
            btn_Play.image.overrideSprite = pauseButton;
            MusicPlayer.Toggle();
        }
    }

    private AudioSource GetOrCreateSource() {
        return gameObject.AddComponent<AudioSource>();
        //var MusicSource = GetComponent<AudioSource>();
        //if (MusicSource == null) {
        //    MusicSource = gameObject.AddComponent<AudioSource>();
        //}
        //return MusicSource;
    }
}
