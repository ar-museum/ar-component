using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Museum
{
    private string museumName;
    private double latitude;
    private double longitude;
    private int textNumber;
    private GameObject museumImage;

    public Museum(string numeMuzeu, double latitudine, double longitudine, int numarText, GameObject museumImage)
    {
        this.museumName = numeMuzeu;
        this.latitude = latitudine;
        this.longitude = longitudine;
        this.textNumber = numarText + 1;
        this.museumImage = museumImage;
    }

    public string getMuseumName()
    {
        return this.museumName;
    }

    public double getLatitude()
    {
        return this.latitude;
    }

    public double getLongitude()
    {
        return this.longitude;
    }

    public int getTextNumber()
    {
        return this.textNumber;
    }

    public GameObject getMuseumImage()
    {
        return this.museumImage;
    }
}
