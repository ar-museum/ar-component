using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ReadInput : MonoBehaviour
{
    [SerializeField]
    private InputField input;

    public void GetInput(string param)
    {
        PlayerPrefs.SetString("searchInput",param);
        Debug.Log("You entered " + param);
        input.text = "";
    }
}
