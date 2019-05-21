using Assets.Scripts.AR_TEAM.Http;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
            if (instance == null)
            {
                instance = new MuseumManager();
            }
            return instance;
        }
    }

    private void OnMuseumLoaded(MuseumDto museum)
    {
        museum.PopulateFields();
        museum.ResolvePaths();
        museum.SetPhotoPath();
        CurrentMuseum = museum;
    }
    

    public async Task DownloadAllAudios()
    {
        var toDownload = new List<(string, string)>();
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
                    toDownload.Add((exh.AudioUrl, exh.AudioPathOnDisk));
                }
            }
        }

        await MuseumRequests.DownloadFiles(toDownload);
    }

    public async Task DownloadAllPhotos()
    {
        var toDownload = new List<(string, string)>();

        if (CurrentMuseum != null)
        {
            List<Exhibit> exhibits = CurrentMuseum.Exhibits;
            var pathOnDisk = GetPhotoFilesPath();
            Directory.CreateDirectory(pathOnDisk);
            foreach (var exh in exhibits)
            {
                exh.PhotoPathOnDisk = pathOnDisk + new FileInfo(exh.PhotoUrl).Name;
                if (!File.Exists(exh.PhotoPathOnDisk))
                {
                    toDownload.Add(("armuseum.ml/" + exh.PhotoUrl, exh.PhotoPathOnDisk));
                }
            }

            List<Author> authors = CurrentMuseum.Authors;
            pathOnDisk = GetPhotoFilesPath();
            foreach (var auth in authors)
            {
                auth.PhotoPathOnDisk = pathOnDisk + new FileInfo(auth.PhotoPath).Name;
                if (!File.Exists(auth.PhotoPathOnDisk))
                {
                    toDownload.Add(("armuseum.ml/" + auth.PhotoPath, auth.PhotoPathOnDisk));
                }
            }
        }
        await MuseumRequests.DownloadFiles(toDownload);
    }

    public async Task DownloadVuforiaFiles()
    {
        var toDownload = new List<(string, string)>();
        if (CurrentMuseum != null)
        {
            bool updateNeeded = NewerVuforiaVersion(MuseumInfo.VuforiaDatabaseVersion);

            CurrentMuseum.VuforiaFilesOnDisk = new List<string>();

            List<string> vuforiaUrls = MuseumInfo.VuforiaFiles;
            var pathOnDisk = GetVuforiaFilesPath();
            Directory.CreateDirectory(pathOnDisk);
            foreach (var vuforiaFileUrl in vuforiaUrls)
            {
                string vuforiaFileOnDisk = pathOnDisk + CurrentMuseum.Name.Replace(" ", "_") + new FileInfo(vuforiaFileUrl).Extension;
                CurrentMuseum.VuforiaFilesOnDisk.Add(vuforiaFileOnDisk);
                if (updateNeeded)
                {
                    toDownload.Add((vuforiaFileUrl, vuforiaFileOnDisk));
                }
            }
            if (updateNeeded)
            {
                UpdateVuforiaVersion(MuseumInfo.VuforiaDatabaseVersion);
            }
        }
        await MuseumRequests.DownloadFiles(toDownload);
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
        if (updateJson == null)
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
            LoadFindData.messageToShow = "Versions file didn't exist";
            Debug.Log("Versions file didn't exist");
            return true;
        }
        var readJson = JSON.Parse(File.ReadAllText(versionsFilePath));
        string readVersion = readJson[CurrentMuseum.Name.Replace(" ", "_")];
        if (CompareVersions(readVersion, version) < 0)
        {
            LoadFindData.messageToShow = "Prepering to download the newer version " + version;
            Debug.Log("Prepering to download the newer version " + version);
            return true;
        }
        LoadFindData.messageToShow = "Version " + version + " is up to date";
        Debug.Log("Version " + version + " is up to date");
        return false;
    }

    private int CompareVersions(string version1, string version2)
    {
        if (version1 == null)
        {
            return -1;
        }
        if (version2 == null)
        {
            return 1;
        }
        string[] version1Numbers = version1.Split('.');
        string[] version2Numbers = version2.Split('.');
        int n = (version1Numbers.Length < version2.Length) ? version1Numbers.Length : version2Numbers.Length;
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
        if (version1Numbers.Length < n)
        {
            return -1;
        }
        if (version2Numbers.Length < n)
        {
            return 1;
        }
        return 0;
    }

    private async Task OnMuseumInfoLoaded(MuseumInfo info)
    {
        MuseumInfo = info;
        if (info == null)
        {
            CurrentMuseum = null;
        }
        else
        {
            await RequestMuseumByID(info.MuseumId);
        }
    }

    public async Task RequestMuseumInfo(GeoCoordinate coordinate) {
        await OnMuseumInfoLoaded(await MuseumRequests.DownloadMuseumInfo(coordinate));
    }

    public async Task RequestMuseumByID(int id)
    {
        OnMuseumLoaded(await MuseumRequests.DownloadMuseum(id));
    }

    public string GetVuforiaFilesPath()
    {
        return Application.persistentDataPath + "/Vuforia/";
    }

    public string GetSoundFilesPath()
    {
        return Application.persistentDataPath + "/Songs/" + CurrentMuseum.Name.Replace(" ", "_") + "/";
    }

    public string GetPhotoFilesPath()
    {
        return Application.persistentDataPath + "/Photos/" + CurrentMuseum.Name.Replace(" ", "_") + "/";
    }
    public string GetVersionsFile()
    {
        return Application.persistentDataPath + "/Vuforia/versions.json";
    }
}
