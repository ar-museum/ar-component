using Assets.Scripts.AR_TEAM.Http;
using Assets.Scripts.AR_TEAM.HttpRequests;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed class MuseumManager
{
    private static MuseumManager instance;

    public MuseumDto CurrentMuseum { get; private set; }

    public MuseumInfo MuseumInfo { get; private set; }

    private MuseumManager()
    {
    }

    public static MuseumManager Instance
    {
        get
        {
            if ( instance == null)
            {
                instance = new MuseumManager();
            }
            return instance;
        }
    }

    private IEnumerator OnMuseumLoaded(MuseumDto museum)
    {
        museum.PopulateExhibits();
        museum.ResolvePaths();
        CurrentMuseum = museum;
        yield return null;
    }

    public IEnumerator GetAllAudios() {
        if (CurrentMuseum != null)
        {
            List<Exhibit> exhibits = CurrentMuseum.Exhibits;
            var pathOnDisk = Application.persistentDataPath + "/Songs/";
            Directory.CreateDirectory(pathOnDisk);
            foreach (var exh in exhibits)
            {
                exh.AudioPathOnDisk = pathOnDisk + new FileInfo(exh.AudioUrl).Name;
                if (!File.Exists(exh.AudioPathOnDisk))
                {
                    Debug.Log("Downloading + " + exh.AudioPathOnDisk);
                    yield return new HttpRequests().DownloadData(exh.AudioUrl, exh.AudioPathOnDisk);
                }
            }
        }
    }

    private IEnumerator OnMuseumInfoLoaded(MuseumInfo info) {
        MuseumInfo = info;
        if (info == null)
        {
            CurrentMuseum = null;
        }
        else
        {
            yield return RequestMuseumByID(info.MuseumId);
        }
    }

    public IEnumerator RequestMuseumInfo(GeoCoordinate coordinate) {
        yield return new HttpRequests().GetMuseumInfo(OnMuseumInfoLoaded, coordinate);
    }

    public IEnumerator RequestMuseumByID(int id)
    {
        yield return new HttpRequests().GetMuseumData(OnMuseumLoaded, id);
    }
}
