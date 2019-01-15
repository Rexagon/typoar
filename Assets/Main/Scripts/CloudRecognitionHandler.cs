using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Vuforia;

public class CloudRecognitionHandler : MonoBehaviour, ICloudRecoEventHandler
{
    public string currentTargetName;

    bool isScanning = false;
    CloudRecoBehaviour cloudRecoBehaviour;

    void Start()
    {
        cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        Assert.IsNotNull(cloudRecoBehaviour);

        cloudRecoBehaviour.RegisterEventHandler(this);
    }
    
    public void OnInitialized(TargetFinder targetFinder)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(TargetFinder.InitState initError)
    {
        Debug.LogError("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        Debug.LogError("Cloud Reco update error " + updateError.ToString());
    }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        var cloudRecoSearchResult = (TargetFinder.CloudRecoSearchResult)targetSearchResult;

        Debug.LogWarning("Cloud target recognized: " + cloudRecoSearchResult.TargetName);

        StartCoroutine(StateManager.Instance.UpdateConfig(cloudRecoSearchResult.TargetName));
        cloudRecoBehaviour.CloudRecoEnabled = false;
    }

    public void OnStateChanged(bool scanning)
    {
        isScanning = scanning;
        if (!scanning)
            return;

        var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(100, 100, 200, 50), isScanning ? "Scanning" : "Not scanning");
        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + currentTargetName);

        if (!isScanning)
        {
            if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
            {
                cloudRecoBehaviour.CloudRecoEnabled = true;
            }
        }
    }
}
