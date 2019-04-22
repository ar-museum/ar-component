using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Museum
{
    private string museumName;
    private double latitude;
    private double longitude;
    private int textNumber;
    private string imgName;

    public Museum(string numeMuzeu, double latitudine, double longitudine, int numarText, string img_Name)
    {
        this.museumName = numeMuzeu;
        this.latitude = latitudine;
        this.longitude = longitudine;
        this.textNumber = numarText + 1;
        this.imgName = img_Name;
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

    public string getImgName()
    {
        return this.imgName;
    }
}
