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
}
