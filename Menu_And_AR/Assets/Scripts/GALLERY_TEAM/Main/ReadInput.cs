using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    [SerializeField]
    private InputField input;

    public void GetInput(string param)
    {
        Debug.Log("You entered " + param);
        input.text = "";
    }
}
