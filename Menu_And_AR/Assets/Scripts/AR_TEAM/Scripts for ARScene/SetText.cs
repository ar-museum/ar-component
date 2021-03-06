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
        string title, author;
        int authorID = 0, exhibitID = 0;
        if(MuseumManager.Instance.CurrentMuseum != null && int.TryParse(targetID, out exhibitID))
            (title, author, authorID) = MuseumManager.Instance.CurrentMuseum.FindArSceneInfoByExhibitId(exhibitID);
        else
        {
            (title, author, authorID) = ("Missing Title", "Missing Author", 0);
        }

        foreach (var textComponent in texts)
        {
            if (textComponent.GetTextType() == SetText.TextType.TopText)
            {
                // Title
                textComponent.SetMyText(title);
                PlayerPrefs.SetString("exhibit", title.Replace(" ", "_"));
            }
            else if (textComponent.GetTextType() == SetText.TextType.BottomText)
            {
                // Author
                textComponent.SetMyText(author);
                PlayerPrefs.SetString("author", author.Replace(" ", "_"));
            }
        }
    }

}