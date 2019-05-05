using System;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    public enum TextType
    {
        TopText,
        BottomText
    }

    [SerializeField] public TextType textType;

    // SetTextTest test 1
    public void SetMyText(String text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }

    // SetTextTest test 1
    public TextType getTextType()
    {
        return textType;
    }

    // SetTextTest test 1
    public string GetText()
    {
        return GetComponent<TextMeshProUGUI>().text;
    }

    // SetTextTest test 1
    public static void SetInfoForTextComponents(SetText[] texts, string targetName)
    {
        foreach (var textComponent in texts)
        {
            if (textComponent.getTextType() == SetText.TextType.TopText)
            {
                // Title
                textComponent.SetMyText(targetName);
            }
            else if (textComponent.getTextType() == SetText.TextType.BottomText)
            {
                // Author
                // TODO: get the author from the database based on the name of the target
                textComponent.SetMyText(targetName + "\'s author");
            }
        }
    }

}