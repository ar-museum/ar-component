using UnityEngine;


public class ARDisplayTypeSwitcherBehaviour : MonoBehaviour
{

    public void SwitchARDisplayType()
    {
        ARDisplayTypeSwitcher.SwitchARDisplayType();
    }
}

public class ARDisplayTypeSwitcher
{
    enum DisplayType
    {
        TargetAttached,
        ScreenAttached
    }

    private static DisplayType displayType = DisplayType.TargetAttached;

    public static void SwitchARDisplayType()
    {
        if(displayType == DisplayType.ScreenAttached)
        {
            displayType = DisplayType.TargetAttached;
            ARDisplayTypeTargetAttached();
        }
        else
        {
            displayType = DisplayType.ScreenAttached;
            ARDisplayTypeScreenAttached();
        }
    }

    private static void ARDisplayTypeTargetAttached()
    {
        if(displayType == DisplayType.TargetAttached)
        {
            // hide the ScreenAttached components

            // show the TargetAttached components
            var targetAttachedComponents = GameObject.FindGameObjectsWithTag("TargetAttached");
            foreach (var targetAttachedComponent in targetAttachedComponents)
            {
                targetAttachedComponent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    private static void ARDisplayTypeScreenAttached()
    {
        if(displayType == DisplayType.ScreenAttached)
        {
            // hide the TargetAttached components
            var targetAttachedComponents = GameObject.FindGameObjectsWithTag("TargetAttached");
            foreach(var targetAttachedComponent in targetAttachedComponents)
            {
                targetAttachedComponent.transform.localScale = new Vector3(0, 0, 0);
            }

            // show the ScreenAttached components
        }
    }
}
