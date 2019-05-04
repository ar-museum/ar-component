using UnityEngine;
using UnityEngine.UI;

public class ARDisplayTypeSwitcherBehaviour : MonoBehaviour
{
    private void Start()
    {
        if (ARDisplayTypeSwitcher.GetDisplayType() == ARDisplayTypeSwitcher.DisplayType.TargetAttached)
        {
            ARDisplayTypeSwitcher.ARDisplayTypeTargetAttached();
        }
        else
        {
            ARDisplayTypeSwitcher.ARDisplayTypeScreenAttached();
        }
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
            HideElement(screenAttachedComponent.transform);
        }
    }

    public static void ShowScreenAttachedComponents(GameObject[] screenAttachedComponents)
    {
        if( displayType == DisplayType.ScreenAttached)
        {
            foreach (var screenAttachedComponent in screenAttachedComponents)
            {
                ShowElement(screenAttachedComponent.transform);
            }
        }
    }

    public static void HideTargetAttachedComponents(GameObject[] targetAttachedComponents)
    {
        foreach (var targetAttachedComponent in targetAttachedComponents)
        {
            HideElement(targetAttachedComponent.transform);
        }
    }

    public static void ShowTargetAttachedComponents(GameObject[] targetAttachedComponents)
    {
        if( displayType == DisplayType.TargetAttached)
        {
            foreach (var targetAttachedComponent in targetAttachedComponents)
            {
                ShowElement(targetAttachedComponent.transform);
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

    public static void HideElement(Transform element)
    {
        element.localScale = new Vector3(0, 0, 0);
    }

    public static void ShowElement(Transform element)
    {
        element.localScale = new Vector3(1, 1, 1);
    }

    public static void UpdateScreenAttachedInfo(string targetName)
    {
        var screenAttachedComponents = getScreenAttachedObjects();
        foreach (var screenAttachedComponent in screenAttachedComponents)
        {
            //show text
            var screenAttachedTexts = screenAttachedComponent.GetComponentsInChildren<SetText>();
            foreach (var screenAttachedText in screenAttachedTexts)
            {
                ShowElement(screenAttachedText.transform);
            }
            // update text
            SetText.SetInfoForTextComponents(screenAttachedTexts, targetName);
            //show button
            var screenAttachedButtons = screenAttachedComponent.GetComponentsInChildren<Button>();
            foreach (var screenAttachedButton in screenAttachedButtons)
            {
                ShowElement(screenAttachedButton.transform);
            }
        }
    }

    public static void CleanScreenAttachedInfo()
    {
        var screenAttachedComponents = getScreenAttachedObjects();
        foreach (var screenAttachedComponent in screenAttachedComponents)
        {
            //hide text
            var screenAttachedTexts = screenAttachedComponent.GetComponentsInChildren<SetText>();
            foreach (var screenAttachedText in screenAttachedTexts)
            {
                HideElement(screenAttachedText.transform);
            }
            //hide button
            var screenAttachedButtons = screenAttachedComponent.GetComponentsInChildren<Button>();
            foreach (var screenAttachedButton in screenAttachedButtons)
            {
                HideElement(screenAttachedButton.transform);
            }
        }
    }
}
