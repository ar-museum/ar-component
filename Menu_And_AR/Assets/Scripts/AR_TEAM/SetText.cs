using System;
using UnityEngine;
using UnityEngine.UI;

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
        if(GetComponent<Text>())
            GetComponent<Text>().text = text;
        if (GetComponent<TextMesh>())
            GetComponent<TextMesh>().text = text;
    }

    public TextType getTextType()
    {
        return textType;
    }

}