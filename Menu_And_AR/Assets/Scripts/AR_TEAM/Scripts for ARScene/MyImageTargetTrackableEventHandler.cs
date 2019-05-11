﻿using UnityEngine;

public class MyImageTargetTrackableEventHandler : DefaultTrackableEventHandler
{
    private GameObject ARUIFrame;
    
    override
    protected void Start()
    {
        base.Start();
        ARUIFrame = GameObject.FindGameObjectWithTag("ARUIFrame");
    }

    override
    protected void OnTrackingFound()
    {
        base.OnTrackingFound();
        HideARUIFrame();
        ARDisplayTypeSwitcher.UpdateScreenAttachedInfo(GetTrackableName());
        ARDisplayTypeSwitcher.ShowScreenAttachedComponents(ARDisplayTypeSwitcher.getScreenAttachedObjects());

        string songname = GetTrackableName();
        GameObject.Find("Audio Source").GetComponent<AudioPlayer>().PlayMusic(songname);   
    }

    override
    protected void OnTrackingLost()
    {
        base.OnTrackingLost();
        ShowARUIFrame();
        ARDisplayTypeSwitcher.CleanScreenAttachedInfo();
        ARDisplayTypeSwitcher.HideScreenAttachedComponents(ARDisplayTypeSwitcher.getScreenAttachedObjects());

        GameObject.Find("Audio Source").GetComponent<AudioPlayer>().StopMusic();
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

    public string GetTrackableName()
    {
        return mTrackableBehaviour.TrackableName;
    }
}
