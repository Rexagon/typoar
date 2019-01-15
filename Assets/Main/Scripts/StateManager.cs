using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JsonHelper
{
    public static T[] GetJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public string baseUrl = "http://typoar.rtuitlab.ru:8082/";

    public Dictionary<string, BundleInfo> targetBundles = new Dictionary<string, BundleInfo>();
    
    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public IEnumerator UpdateConfig(string target)
    {
        string url = string.Format("{0}/base/{1}", baseUrl.TrimEnd('/'), target.TrimStart('/'));

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogWarning("Unable to load config of " + url);
            yield break;
        }

        string response = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
        var bundles = JsonHelper.GetJsonArray<BundleInfo>(response);

        Debug.LogWarning(response);

        var loaders = FindObjectsOfType<BundleLoader>();
        foreach (var loader in loaders)
        {
            loader.DestroySpawned();
        }

        targetBundles.Clear();
        foreach(var bundle in bundles)
        {
            targetBundles.Add(bundle.markerType, bundle);
            Debug.Log(bundle.markerType + " " + bundle.bundleName);
        }
    }

    private void HandleConfig()
    {

    }
}
