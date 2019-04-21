using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Museum
{
    private string museumName;
    private double latitude;
    private double longitude;
    private int textNumber;

    public Museum(string numeMuzeu, double latitudine, double longitudine, int numarText)
    {
        this.museumName = numeMuzeu;
        this.latitude = latitudine;
        this.longitude = longitudine;
        this.textNumber = numarText + 1;
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
}
