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

    // DisplayTypeSwitcherTest test 1 test 2 test 3
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

    // DisplayTypeSwitcherTest test 2 test 4
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

    // DisplayTypeSwitcherTest test 3 test 5
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

    // DisplayTypeSwitcherTest test 2 test 4
    public static void HideScreenAttachedComponents(GameObject[] screenAttachedComponents)
    {
        foreach (var screenAttachedComponent in screenAttachedComponents)
        {
            HideElement(screenAttachedComponent.transform);
        }
    }

    // DisplayTypeSwitcherTest test 3 test 5
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

    // DisplayTypeSwitcherTest test 3 test 5
    public static void HideTargetAttachedComponents(GameObject[] targetAttachedComponents)
    {
        foreach (var targetAttachedComponent in targetAttachedComponents)
        {
            HideElement(targetAttachedComponent.transform);
        }
    }

    // DisplayTypeSwitcherTest test 2 test 4
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

    // DisplayTypeSwitcherTest test 2 test 3 test 4 test 5
    public static GameObject[] getTargetAttachedObjects()
    {
        return GameObject.FindGameObjectsWithTag("TargetAttached");
    }

    // DisplayTypeSwitcherTest test 2 test 3 test 4 test 5
    public static GameObject[] getScreenAttachedObjects()
    {
        return GameObject.FindGameObjectsWithTag("ScreenAttached");
    }

    // DisplayTypeSwitcherTest test 8
    public static void HideElement(Transform element)
    {
        element.localScale = new Vector3(0, 0, 0);
    }

    // DisplayTypeSwitcherTest test 9
    public static void ShowElement(Transform element)
    {
        element.localScale = new Vector3(1, 1, 1);
    }

    // DisplayTypeSwitcherTest test 6
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

    // DisplayTypeSwitcherTest test 7
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
