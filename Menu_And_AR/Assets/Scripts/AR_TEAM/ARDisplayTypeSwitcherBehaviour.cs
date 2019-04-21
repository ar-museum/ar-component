using UnityEngine;
using UnityEngine.UI;

public class ARDisplayTypeSwitcherBehaviour : MonoBehaviour
{
    private void Start()
    {
        ARDisplayTypeSwitcher.ARDisplayTypeTargetAttached();
    }

    public void SwitchARDisplayType()
    {
        ARDisplayTypeSwitcher.SwitchARDisplayType();
    }
}

public class ARDisplayTypeSwitcher
{
    public enum DisplayType
    {
        TargetAttached,
        ScreenAttached
    }


    private static DisplayType displayType = DisplayType.TargetAttached;

    public static DisplayType GetDisplayType()
    {
        return displayType;
    }

    public static void SetDisplayType(DisplayType value)
    {
        displayType = value;
    }

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

    public static void ARDisplayTypeTargetAttached()
    {

        if (displayType == DisplayType.TargetAttached)
        {
            // hide the ScreenAttached components
            var screenAttachedComponents = getScreenAttachedObjects();
            HideScreenAttachedComponents(screenAttachedComponents);
            // show the TargetAttached components
            var targetAttachedComponents = getTargetAttachedObjects ();
            ShowTargetAttachedComponents(targetAttachedComponents);

        }
    }

    public static void ARDisplayTypeScreenAttached()
    {

        if (displayType == DisplayType.ScreenAttached)
        {
            // hide the TargetAttached components
            var targetAttachedComponents = getTargetAttachedObjects();
            HideTargetAttachedComponents(targetAttachedComponents);

            // show the ScreenAttached components
            var screenAttachedComponents = getScreenAttachedObjects();
            ShowScreenAttachedComponents(screenAttachedComponents);


        }
    }

    public static void HideScreenAttachedComponents(GameObject[] screenAttachedComponents)
    {
        foreach (var screenAttachedComponent in screenAttachedComponents)
        {
            screenAttachedComponent.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public static void ShowScreenAttachedComponents(GameObject[] screenAttachedComponents)
    {
        if( displayType == DisplayType.ScreenAttached)
        {
            foreach (var screenAttachedComponent in screenAttachedComponents)
            {
                screenAttachedComponent.transform.localScale = new Vector3(1, 0.1f, 1);
            }
        }
    }

    public static void HideTargetAttachedComponents(GameObject[] targetAttachedComponents)
    {
        foreach (var targetAttachedComponent in targetAttachedComponents)
        {
            targetAttachedComponent.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public static void ShowTargetAttachedComponents(GameObject[] targetAttachedComponents)
    {
        if( displayType == DisplayType.TargetAttached)
        {
            foreach (var targetAttachedComponent in targetAttachedComponents)
            {
                targetAttachedComponent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    public static GameObject[] getTargetAttachedObjects()
    {
        return GameObject.FindGameObjectsWithTag("TargetAttached");
    }

    public static GameObject[] getScreenAttachedObjects()
    {
        return GameObject.FindGameObjectsWithTag("ScreenAttached");
    }
}
