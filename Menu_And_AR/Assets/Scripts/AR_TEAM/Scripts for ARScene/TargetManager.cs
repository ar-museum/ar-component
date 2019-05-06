using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;
using System.IO;
using UnityEditor;
using System;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private string CurrentVuforiaDatabase;
    [SerializeField] private GameObject ImageTargetChildPrefab;

    private List<TrackableBehaviour> Targets = new List<TrackableBehaviour>();

    private void Awake()
    {
        // After all from Vuforia is loaded, call my function
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnDestroy()
    {
        // Unregister my function from being called 
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    // TargetManagerTests test 3
    void OnVuforiaStarted() {

        // The 'path' string determines the location of xml file
        // For convinence the RealTime.xml is placed in the StreamingAssets folder
        // This file can be downloaded and the relative path will be used accordingly

        //Debug.Log("App path: " + Application.dataPath);
        //File.WriteAllText("output.txt", "App path: " + Application.dataPath);

        var androidPath = "/storage/emulated/0/vufo/ARMuseum.xml";
        var androiddddd = Application.persistentDataPath + "/ARMuseum.xml";

        Directory.CreateDirectory(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/output.txt", "App path: " + Application.dataPath
            + "\n" + "sdcard: " + Application.persistentDataPath + "\nfff: " + androidPath
            + "\nxxx: " + androiddddd);
        File.AppendAllText(Application.persistentDataPath + "/output.txt", "\n" + File.ReadAllText(androiddddd));


        string path = "";
#if UNITY_IPHONE
		path = Application.dataPath + "/Raw/RealTime.xml";
#elif UNITY_ANDROID
        path = androiddddd; //"jar:file://" + Application.dataPath + "!/assets/RealTime.xml";
#else
		path = Application.dataPath + "/StreamingAssets/Vuforia/testy/StreamingAssets/ARMuseum.xml";
#endif

        bool status = false;
        try {
            status = LoadDataSet(path, VuforiaUnity.StorageType.STORAGE_ABSOLUTE);
        } catch (Exception e) {
            File.AppendAllText(Application.persistentDataPath + "/output.txt", "\nexception: " + e.ToString());
        }
        File.AppendAllText(Application.persistentDataPath + "/output.txt", "\nstatus: " + status.ToString());


        if (status) {
            Debug.Log("Dataset Loaded");
        } else {
            Debug.Log("Dataset Load Failed");
        }

        SetupTargets(GetTargets(), ImageTargetChildPrefab);
    }

    // Load and activate a data set at the given path.
    private bool LoadDataSet(string dataSetPath, VuforiaUnity.StorageType storageType) {
        // Request an ImageTracker instance from the TrackerManager.
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        objectTracker.Stop();
        IEnumerable<DataSet> dataSetList = objectTracker.GetActiveDataSets();
        foreach (DataSet set in dataSetList.ToList()) {
            objectTracker.DeactivateDataSet(set);
        }

        // Check if the data set exists at the given path.
        if (!DataSet.Exists(dataSetPath, storageType)) {
            Debug.LogError("Data set " + dataSetPath + " does not exist.");
            return false;
        }

        // Create a new empty data set.
        DataSet dataSet = objectTracker.CreateDataSet();

        // Load the data set from the given path.
        if (!dataSet.Load(dataSetPath, storageType)) {
            Debug.LogError("Failed to load data set " + dataSetPath + ".");
            return false;
        }

        // (Optional) Activate the data set.
        objectTracker.ActivateDataSet(dataSet);
        objectTracker.Start();


        return true;
    }

    // TargetManagerTests test 1 test 2 test 3
    public void LoadDatabase(string databaseName)
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        objectTracker.Stop();

        if (DataSet.Exists(databaseName))
        {
            // Delete the previous vuforia databases and all their active targerts from tracking
            DeleteTargets(objectTracker);

            DataSet dataSet = objectTracker.CreateDataSet();

            // Load the given Vuforia Database
            dataSet.Load(databaseName);
            objectTracker.ActivateDataSet(dataSet);
        }
        else
        {
            throw new System.ArgumentException("Invalid argument.", "databaseName");
        }
        objectTracker.Start();
    }

    // TargetManagerTests test 3
    private List<TrackableBehaviour> GetTargets()
    {
        // Getting all the targets from the current Vuforia database
        List<TrackableBehaviour> trackables = new List<TrackableBehaviour>();
        trackables = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours().ToList();

        return trackables;
    }

    // TargetManagerTests test 3 test 4 test 5
    public void DeleteTargets(ObjectTracker objectTracker)
    {
        // Getting all the targets from the current Vuforia database and delete them both from 
        // objectTracker's memory and from the initialised objects in unity
        if (!objectTracker.IsActive)
        {
            var activeDataSets = objectTracker.GetActiveDataSets().ToList();
            foreach (var dataset in activeDataSets)
            {
                objectTracker.DeactivateDataSet(dataset);
                var trackables = dataset.GetTrackables().ToList();

                foreach (var trackable in trackables)
                {
                    TrackerManager.Instance.GetStateManager().DestroyTrackableBehavioursForTrackable(trackable, true);
                }
            }
            objectTracker.DestroyAllDataSets(false);
        }
        else
        {
            throw new System.ArgumentException("Cannot operate while the argument is still active. Please call Stop().", "objectTracker");
        }
    }

    // TargetManagerTests test 3
    private void SetupTargets(List<TrackableBehaviour> targets, GameObject childPrefab)
    {
        foreach(TrackableBehaviour target in targets)
        {
            // Other components needed to make this an ImageTarget
            target.gameObject.AddComponent<MyImageTargetTrackableEventHandler>();
            target.gameObject.AddComponent<TurnOffBehaviour>();
            // Child
            CreateChildOfTarget(target, childPrefab);
            // Parent
            target.gameObject.transform.parent = transform;
            // Rename
            target.gameObject.name = "Image Target: " + target.TrackableName;
            // Printf
            Debug.Log(target.gameObject.name + " Created");
        }
    }

    // TargetManagerTests test 3
    private void CreateChildOfTarget(TrackableBehaviour target, GameObject childPrefab)
    {
        // The child is what will be shown when the target is recognized
        var child = Instantiate(childPrefab, target.transform);
        child.transform.parent = target.transform;


        string targetName = target.TrackableName;
        var textComponents = child.GetComponentsInChildren<SetText>(true);
        SetText.SetInfoForTextComponents(textComponents, targetName);
    }
}
