using Assets.Scripts.AR_TEAM.Http;
using Assets.Scripts.AR_TEAM.HttpRequests;
using SimpleJSON;
using System;
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
            var pathOnDisk = GetSoundFilesPath();
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

    public IEnumerator GetVuforiaFiles()
    {
        if (CurrentMuseum != null)
        {
            if (NewerVuforiaVersion(MuseumInfo.VuforiaDatabaseVersion))
            {
                CurrentMuseum.VuforiaFilesOnDisk = new List<string>();

                List<string> vuforiaUrls = MuseumInfo.VuforiaFiles;
                var pathOnDisk = GetVuforiaFilesPath();
                Directory.CreateDirectory(pathOnDisk);
                foreach (var vuforiaFileUrl in vuforiaUrls)
                {
                    string vuforiaFileOnDisk = pathOnDisk + CurrentMuseum.Name.Replace(" ", "_") + new FileInfo(vuforiaFileUrl).Extension;
                    CurrentMuseum.VuforiaFilesOnDisk.Add(vuforiaFileOnDisk);
                    Debug.Log("Downloading + " + vuforiaFileOnDisk);
                    yield return new HttpRequests().DownloadData(vuforiaFileUrl, vuforiaFileOnDisk);
                }

                UpdateVuforiaVersion(MuseumInfo.VuforiaDatabaseVersion);
            }
        }
    }

    private void UpdateVuforiaVersion(string version)
    {
        string versionsFilePath = GetVersionsFile();
        if (!File.Exists(versionsFilePath))
        {
            File.Create(versionsFilePath).Dispose();
        }
        var updateJson = JSON.Parse(File.ReadAllText(versionsFilePath)).AsObject;
        string museum_name = CurrentMuseum.Name.Replace(" ", "_");
        if(updateJson == null)
        {
            updateJson = new JSONObject();
        }
        if (updateJson.HasKey(museum_name))
        {
            updateJson[museum_name] = version;
        }
        else
        {
            updateJson.Add(museum_name, version);
        }
        File.WriteAllText(versionsFilePath, updateJson.ToString());
    }

    private bool NewerVuforiaVersion(string version)
    {
        string versionsFilePath = GetVersionsFile();
        if (!File.Exists(versionsFilePath))
        {
            Debug.Log("Versions file didn't exist");
            return true;
        }
        var readJson = JSON.Parse(File.ReadAllText(versionsFilePath));
        string readVersion = readJson[CurrentMuseum.Name.Replace(" ", "_")];
        if(CompareVersions(readVersion, version) < 0)
        {
            Debug.Log("Prepering to download the newer version " + version);
            return true;
        }
        Debug.Log("Version " + version + " is up to date");
        return false;
    }

    private int CompareVersions(string version1, string version2)
    {
        if(version1 == null)
        {
            return -1;
        }
        if(version2 == null)
        {
            return 1;
        }
        string[] version1Numbers = version1.Split('.');
        string[] version2Numbers = version2.Split('.');
        int n = (version1Numbers.Length < version2.Length)? version1Numbers.Length : version2Numbers.Length;
        for (int i = 0; i < n; ++i)
        {
            if (Convert.ToInt32(version1Numbers[i]) < Convert.ToInt32(version2Numbers[i]))
            {
                return -1;
            }
            if (Convert.ToInt32(version1Numbers[i]) > Convert.ToInt32(version2Numbers[i]))
            {
                return 1;
            }
        }
        if( version1Numbers.Length < n)
        {
            return -1;
        }
        if (version2Numbers.Length < n)
        {
            return 1;
        }
        return 0;
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

    public string GetVuforiaFilesPath()
    {
        return Application.persistentDataPath + "/Vuforia/";
    }

    public string GetSoundFilesPath()
    {
        return Application.persistentDataPath + "/Songs/" + CurrentMuseum.Name.Replace(" ", "_") + "/";
    }

    public string GetVersionsFile()
    {
        return Application.persistentDataPath + "/Vuforia/versions.json";
    }
}
