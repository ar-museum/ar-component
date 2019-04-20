using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ARDisplayTypeSwitcher.HideScreenAttachedComponents(ARDisplayTypeSwitcher.getScreenAttachedObjects());
    }

    public void HideARUIFrame()
    {
        GameObject.FindGameObjectWithTag("ARUIFrame").transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowARUIFrame()
    {
        GameObject.FindGameObjectWithTag("ARUIFrame").transform.localScale = new Vector3(1, 1, 1);
    }

    public void UpdateScreenAttachedInfo()
    {
        var screenAttachedComponents = ARDisplayTypeSwitcher.getScreenAttachedObjects();
        var targetName = mTrackableBehaviour.TrackableName;
        foreach (var screenAttachedComponent in screenAttachedComponents)
        {
            var screenAttachedTexts = screenAttachedComponent.GetComponentsInChildren<SetText>();
            TargetManager.SetInfoForTextComponents(screenAttachedTexts, targetName);
        }
    }
}
