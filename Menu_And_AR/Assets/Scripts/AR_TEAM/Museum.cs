using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Museum
{
    public string name;
    public string name_eng;
    public double latitude;
    public double longitude;
}

[System.Serializable]
public class MuseumArray
{
    public List<Museum> museums;
}
