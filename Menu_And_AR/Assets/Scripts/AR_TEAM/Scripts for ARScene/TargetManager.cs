using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private string CurrentVuforiaDatabase;
    [SerializeField] private GameObject ImageTargetChildPrefab;

    private List<TrackableBehaviour> Targets = new List<TrackableBehaviour>();

    private void Awake()
    {
        // After all from Vuforia is loaded, call my function
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(DoAfterVuforiaStarted);
    }

    private void OnDestroy()
    {
        // Unregister my function from being called 
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(DoAfterVuforiaStarted);
    }

    // TargetManagerTests test 3
    private void DoAfterVuforiaStarted()
    {
        if (Application.isMobilePlatform)
        {
            string path = Application.persistentDataPath + "/" + CurrentVuforiaDatabase + ".xml";
        }
        // Load database
        LoadDatabase(CurrentVuforiaDatabase);

        // Get trackable targers
        Targets = GetTargets();

        // Setup targets
        SetupTargets(Targets, ImageTargetChildPrefab);
    }

    // TargetManagerTests test 1 test 2 test 3
    public void LoadDatabase(string databaseName)
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        objectTracker.Stop();
        string path = Application.persistentDataPath + "/" + databaseName + ".xml";
        if (DataSet.Exists(databaseName) || 
            (Application.isMobilePlatform && DataSet.Exists(path, VuforiaUnity.StorageType.STORAGE_ABSOLUTE)))
        {
            // Delete the previous vuforia databases and all their active targerts from tracking
            DeleteTargets(objectTracker);

            DataSet dataSet = objectTracker.CreateDataSet();

            // Load the given Vuforia Database
            //dataSet.Load(databaseName);
            if (Application.isMobilePlatform)
            {
                dataSet.Load(path, VuforiaUnity.StorageType.STORAGE_ABSOLUTE);
            }
            else
            {
                dataSet.Load(databaseName);
            }
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
        foreach (TrackableBehaviour target in targets)
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