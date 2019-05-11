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
    public void SetMyText(string text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }

    // SetTextTest test 1
    public TextType GetTextType()
    {
        return textType;
    }

    // SetTextTest test 1
    public string GetText()
    {
        return GetComponent<TextMeshProUGUI>().text;
    }

    // SetTextTest test 1
    public static void SetInfoForTextComponents(SetText[] texts, string targetID)
    {
        var (title, author, id) = LoadFindData.MuseumDto.FindArSceneInfoByExhibitId(Convert.ToInt32(targetID));

        foreach (var textComponent in texts)
        {
            if (textComponent.GetTextType() == SetText.TextType.TopText)
            {
                // Title
                textComponent.SetMyText(title);
            }
            else if (textComponent.GetTextType() == SetText.TextType.BottomText)
            {
                // Author
                // TODO: get the author from the database based on the name of the target
                textComponent.SetMyText(author);
            }
        }
    }

}