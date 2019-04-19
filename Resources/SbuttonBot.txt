using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SbuttonBot : MonoBehaviour, IVirtualButtonEventHandler {

    public GameObject vbBtnObj;

    void Start () {
        vbBtnObj = GameObject.Find("LacieBtn");
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
     }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("Bot Button pressed");
    }
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("Bot Button released");
    }
}