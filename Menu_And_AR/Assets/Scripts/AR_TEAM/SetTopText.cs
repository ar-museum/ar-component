using System;
using System.Collections;
using UnityEngine;

public class SetTopText : MonoBehaviour
{

    public TextMesh top;

    void Start()
    {

        top = GetComponent<TextMesh>();

        //SetTextTop( "Default2");
    }

    public void SetTextTop(String textTop)
    {
        top.text = textTop;
    }

}