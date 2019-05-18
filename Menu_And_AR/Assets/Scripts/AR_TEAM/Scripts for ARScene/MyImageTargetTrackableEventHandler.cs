using System;
using UnityEngine;

public class MyImageTargetTrackableEventHandler : DefaultTrackableEventHandler
{
    private GameObject ARUIFrame;
    private AudioPlayer AudioPlayer;
    
    override
    protected void Start()
    {
        base.Start();
        ARUIFrame = GameObject.FindGameObjectWithTag("ARUIFrame");
        AudioPlayer = GameObject.Find("Audio Source").GetComponent<AudioPlayer>();
    }

    override
    protected void OnTrackingFound()
    {
        base.OnTrackingFound();
        HideARUIFrame();
        ARDisplayTypeSwitcher.UpdateScreenAttachedInfo(GetTrackableID());
        ARDisplayTypeSwitcher.ShowScreenAttachedComponents(ARDisplayTypeSwitcher.getScreenAttachedObjects());
        SetPrefsForGallery();
        PlayAudio(); 
    }

    override
    protected void OnTrackingLost()
    {
        base.OnTrackingLost();
        ShowARUIFrame();
        ARDisplayTypeSwitcher.CleanScreenAttachedInfo();
        ARDisplayTypeSwitcher.HideScreenAttachedComponents(ARDisplayTypeSwitcher.getScreenAttachedObjects());
        StopAudio();
    }

    private void PlayAudio()
    {
        if(AudioPlayer != null)
        {
            var songPath = MuseumManager.Instance.CurrentMuseum.GetSongForExhibitId(Convert.ToInt32(GetTrackableID()));
            AudioPlayer.PlayMusic(songPath);
        }
    }

    private void StopAudio()
    {
        if(AudioPlayer != null)
        {
            AudioPlayer.StopMusic();
        }
    }

    private void HideARUIFrame()
    {
        if (ARUIFrame != null)
        {
            ARDisplayTypeSwitcher.HideElement(ARUIFrame.transform);
        }
    }

    private void ShowARUIFrame()
    {
        if (ARUIFrame != null)
        {
            ARDisplayTypeSwitcher.ShowElement(ARUIFrame.transform);
        }
    }

    public string GetTrackableID()
    {
        return mTrackableBehaviour.TrackableName;
    }

    private void SetPrefsForGallery()
    {
        string title, author;
        int authorID = 0, exhibitID = 0;
        if (MuseumManager.Instance.CurrentMuseum != null && int.TryParse(GetTrackableID(), out exhibitID))
            (title, author, authorID) = MuseumManager.Instance.CurrentMuseum.FindArSceneInfoByExhibitId(exhibitID);
        else
        {
            (title, author, authorID) = ("Missing Title", "Missing Author", 0);
        }
        PlayerPrefs.SetString("Gallery_Author", author.Replace(" ", "_"));
        PlayerPrefs.SetInt("Gallery_AuthorID", authorID);
        PlayerPrefs.SetString("Gallery_Exhibit", title.Replace(" ", "_"));
        PlayerPrefs.SetInt("Gallery_ExhibitID", exhibitID);
    }
}
