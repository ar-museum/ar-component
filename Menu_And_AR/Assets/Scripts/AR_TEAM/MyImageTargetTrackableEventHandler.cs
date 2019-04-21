using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyImageTargetTrackableEventHandler : DefaultTrackableEventHandler
{
    override
    protected void OnTrackingFound()
    {
        base.OnTrackingFound();
        HideARUIFrame();
        UpdateScreenAttachedInfo();
        ARDisplayTypeSwitcher.ShowScreenAttachedComponents(ARDisplayTypeSwitcher.getScreenAttachedObjects());
    }

    override
    protected void OnTrackingLost()
    {
        base.OnTrackingLost();
        ShowARUIFrame();
        CleanScreenAttachedInfo();
        ARDisplayTypeSwitcher.HideScreenAttachedComponents(ARDisplayTypeSwitcher.getScreenAttachedObjects());
    }

    private void HideARUIFrame()
    {
        GameObject.FindGameObjectWithTag("ARUIFrame").transform.localScale = new Vector3(0, 0, 0);
    }

    private void ShowARUIFrame()
    {
        GameObject.FindGameObjectWithTag("ARUIFrame").transform.localScale = new Vector3(1, 1, 1);
    }

    public void UpdateScreenAttachedInfo()
    {
        var screenAttachedComponents = ARDisplayTypeSwitcher.getScreenAttachedObjects();
        var targetName = mTrackableBehaviour.TrackableName;
        foreach (var screenAttachedComponent in screenAttachedComponents)
        {
            //show text
            var screenAttachedTexts = screenAttachedComponent.GetComponentsInChildren<SetText>();
            foreach (var screenAttachedText in screenAttachedTexts)
            {
                screenAttachedText.transform.localScale = new Vector3(1, 4, 1);
            }
            // update text
            TargetManager.SetInfoForTextComponents(screenAttachedTexts, targetName);
            //show button
            var screenAttachedButtons = screenAttachedComponent.GetComponentsInChildren<Button>();
            foreach(var screenAttachedButton in screenAttachedButtons)
            {
                screenAttachedButton.transform.localScale = new Vector3(1, 10, 1);
            }
        }
    }

    public void CleanScreenAttachedInfo()
    {
        var screenAttachedComponents = ARDisplayTypeSwitcher.getScreenAttachedObjects();
        var targetName = mTrackableBehaviour.TrackableName;
        foreach (var screenAttachedComponent in screenAttachedComponents)
        {
            //hide text
            var screenAttachedTexts = screenAttachedComponent.GetComponentsInChildren<SetText>();
            foreach( var screenAttachedText in screenAttachedTexts)
            {
                screenAttachedText.transform.localScale = new Vector3(0,0,0);
            }
            //hide button
            var screenAttachedButtons = screenAttachedComponent.GetComponentsInChildren<Button>();
            foreach (var screenAttachedButton in screenAttachedButtons)
            {
                screenAttachedButton.transform.localScale = new Vector3(0,0,0);
            }
        }
    }

    public string GetTrackableName()
    {
        return mTrackableBehaviour.TrackableName;
    }
}
