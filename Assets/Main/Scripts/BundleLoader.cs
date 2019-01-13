using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BundleLoader : MonoBehaviour
{
    public Transform origin;
    public ImageTargetBehaviour target;

    GameObject spawnedObject;
    string targetMarkerName;

    string bundleName = null;

    private void Start()
    {
        if (origin == null)
        {
            origin = transform.parent;
        }

        targetMarkerName = target.TrackableName;
    }

    public void Spawn()
    {
        if (spawnedObject != null)
            return;

        BundleInfo bundleInfo;
        if (!StateManager.Instance.targetBundles.TryGetValue(targetMarkerName, out bundleInfo))
            return;

        if (bundleInfo.bundleName == null)
            return;

        bundleName = bundleInfo.bundleName;
        StartCoroutine(BundleManager.Instance.LoadBundleAsync(bundleInfo.bundleName, OnBundleLoaded));
    }

    private void OnBundleLoaded(AssetBundle bundle)
    {
        if (bundle == null)
        {
            Debug.Log("Unable to load bundle " + bundleName);
            return;
        }

        GameObject prefab = bundle.LoadAsset<GameObject>(bundleName);
        if (prefab == null)
        {
            Debug.Log("Unable to load prefab \"" + bundleName + "\" of \"" + bundleName + "\"");
            return;
        }

        spawnedObject = Instantiate(prefab, origin);
    }
}
