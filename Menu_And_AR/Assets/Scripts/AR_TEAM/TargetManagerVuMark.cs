using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class TargetManagerVuMark : MonoBehaviour
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

    private List<TrackableBehaviour> GetTargets()
    {
        // Getting all the targets from the current Vuforia database
        List<TrackableBehaviour> trackables = new List<TrackableBehaviour>();
        trackables = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours().ToList();

        return trackables;
    }

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

    private void CreateChildOfTarget(TrackableBehaviour target, GameObject childPrefab)
    {
        // The child is what will be shown when the target is recognized
        var child = Instantiate(childPrefab, target.transform);
        child.transform.parent = target.transform;
        string targetName = target.TrackableName;



        var authors = child.GetComponentsInChildren<SetBotText>(true);
        var titles = child.GetComponentsInChildren<SetTopText>(true);

        // Adding the title for each target
        // The title coincides with the name of the target
        foreach (var title in titles)
        {
            title.SetTextTop(targetName);
        }
        // Adding the author for each target
        // TODO: get the author from the database based on the name of the target
        foreach (var author in authors)
        {
            author.SetTextBot(targetName);
        }
    }
}
