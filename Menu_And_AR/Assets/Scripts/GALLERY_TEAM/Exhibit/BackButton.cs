using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;

public class BackButton : MonoBehaviour
{
    public void goTo(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        FileStream fs = File.Create("Assets/nameeee.txt");
        string path = "Assets/nameeee.txt";
        fs.Dispose();
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine("op1");
            writer.WriteLine("cevaaa");
        }

    }
}
