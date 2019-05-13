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
        RemovePrefsForGallery();
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
        int authorID, exhibitID;
        if (MuseumManager.Instance.CurrentMuseum != null)
            (title, exhibitID, author, authorID) = MuseumManager.Instance.CurrentMuseum.FindArSceneInfoByExhibitId(Convert.ToInt32(GetTrackableID()));
        else
        {
            (title, exhibitID, author, authorID) = ("Missing Exhibit", 0, "Missing Author", 0);
        }
        PlayerPrefs.SetString("Gallery_Author", author.Replace(" ", "_"));
        PlayerPrefs.SetInt("Gallery_AuthorID", authorID);
        PlayerPrefs.SetString("Gallery_Exhibit", title.Replace(" ", "_"));
        PlayerPrefs.SetInt("Gallery_ExhibitID", exhibitID);
    }

    private void RemovePrefsForGallery()
    {
        PlayerPrefs.DeleteKey("Gallery_Author");
        PlayerPrefs.DeleteKey("Gallery_AuthorID");
        PlayerPrefs.DeleteKey("Gallery_Exhibit");
        PlayerPrefs.DeleteKey("Gallery_ExhibitID");
    }
}
