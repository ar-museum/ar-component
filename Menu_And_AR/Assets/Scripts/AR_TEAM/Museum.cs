using System.Collections.Generic;

[System.Serializable]
public class Museum
{
    public string name;
    public double latitude;
    public double longitude;
}

[System.Serializable]
public class MuseumArray
{
    public List<Museum> museums;
}
