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

    public void SetMyText(String text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }

    public TextType getTextType()
    {
        return textType;
    }

    public string GetText()
    {
        return GetComponent<TextMeshProUGUI>().text;
    }

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