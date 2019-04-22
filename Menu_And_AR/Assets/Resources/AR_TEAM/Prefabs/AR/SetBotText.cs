using System;
using System.Collections;
using UnityEngine;

public class SetBotText : MonoBehaviour
{

    public TextMesh bot;

    void Start()
    {

        bot = GetComponent<TextMesh>();

        SetTextBot("aaaaa");
    }

    public void SetTextBot(String textBot)
    {
        bot.text = textBot;
    }

}