using System.Collections;
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

    private void DoAfterVuforiaStarted()
    {
        // Load database
        LoadDatabase(CurrentVuforiaDatabase);

        // Get trackable targers
        Targets = GetTargets();

        // Setup targets
        SetupTargets(Targets, ImageTargetChildPrefab);
    }

    public void LoadDatabase(string databaseName)
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        objectTracker.Stop();

        if (DataSet.Exists(databaseName))
        {
            // Delete the previous vuforia database and all its active targerts from tracking
            objectTracker.DestroyAllDataSets(false);

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

    private List<TrackableBehaviour> GetTargets()
    {
        // Getting all the targets from the current Vuforia database
        List<TrackableBehaviour> trackables = new List<TrackableBehaviour>();
        trackables = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours().ToList();

        return trackables;
    }

    private void SetupTargets(List<TrackableBehaviour> targets, GameObject childPrefab)
    {
        foreach(TrackableBehaviour target in targets)
        {
            // Other components needed to make this an ImageTarget
            target.gameObject.AddComponent<DefaultTrackableEventHandler>();
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

    private void CreateChildOfTarget(TrackableBehaviour target, GameObject childPrefab)
    {
        // The child is what will be shown when the target is recognized
        var child = Instantiate(childPrefab, target.transform);
        child.transform.parent = target.transform;
    }
}
