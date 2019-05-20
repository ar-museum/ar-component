using Assets.Scripts.AR_TEAM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class PlayPause : MonoBehaviour
{
    public Button btn_Play;
    public Sprite playButton;
    public Sprite pauseButton;
    public MusicPlayer MusicPlayer { get; private set; }

    private bool IsPlaying { get; set; }


    public void Update()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            int exhibitId = PlayerPrefs.GetInt("Gallery_ExhibitID");
            var path = MuseumManager.Instance.CurrentMuseum.GetSongForExhibitId(exhibitId);
            MusicPlayer = gameObject.AddComponent<MusicPlayer>();
            var source = GetOrCreateSource();
            MusicPlayer.Set(path, source);
        }
        catch (Exception e)
        {
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
        }
        else
        {
            btn_Play.image.overrideSprite = pauseButton;
            MusicPlayer.Toggle();
        }
    }

    private AudioSource GetOrCreateSource()
    {
        return gameObject.AddComponent<AudioSource>();
    }
}
