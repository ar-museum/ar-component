using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Sbutton : MonoBehaviour, IVirtualButtonEventHandler {

    public GameObject vbBtnObj;

    void Start () {
        vbBtnObj = GameObject.Find("LacieBtnTop");
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        
     }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("Top Button pressed");
    }
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("Top Button released");
    }
}