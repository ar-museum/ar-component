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
    }

    override
    protected void OnTrackingLost()
    {
        base.OnTrackingLost();
        ShowARUIFrame();
    }

    public void HideARUIFrame()
    {
        GameObject.FindGameObjectWithTag("ARUIFrame").transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowARUIFrame()
    {
        GameObject.FindGameObjectWithTag("ARUIFrame").transform.localScale = new Vector3(1, 1, 1);
    }
}
