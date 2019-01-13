using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class BundleInfo
{
    public string _id;
    public string baseMarker;
    public string markerType;
    public string bundleName;
}

public class BundleManager : MonoBehaviour
{
    public Dictionary<string, AssetBundle> assetBundles = new Dictionary<string, AssetBundle>();

    public static BundleManager Instance = null;

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public IEnumerator LoadBundleAsync(string path, Action<AssetBundle> callback)
    {
        string baseUrl = StateManager.Instance.baseUrl;
        string bundleUrl = string.Format("{0}/bundles/{1}", baseUrl.TrimEnd('/'), path.TrimStart('/'));
        Debug.Log(bundleUrl);

        AssetBundle bundle = null;
        if (assetBundles.TryGetValue(bundleUrl, out bundle))
        {
            callback(bundle);
            yield break;
        }

        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            callback(null);
            yield break;
        }

        bundle = ((DownloadHandlerAssetBundle)request.downloadHandler).assetBundle;
        assetBundles.Add(bundleUrl, bundle);

        callback(bundle);
    }
}
