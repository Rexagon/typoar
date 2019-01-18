using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class VRSwitcher : MonoBehaviour
{
    public bool DisableOnAwake = false;

    private void Start()
    {
        if (DisableOnAwake)
        {
            XRSettings.enabled = false;
        }
        else
        {
            StartCoroutine(SwitchToVR());
        }
    }

    private void OnDestroy()
    {
        XRSettings.enabled = false;
    }

    private void Update()
    {
        if (DisableOnAwake)
            return;

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            return;
        }
    }

    IEnumerator SwitchToVR()
    {
        string[] DaydreamDevices = new string[] { "daydream", "cardboard" };
        XRSettings.LoadDeviceByName(DaydreamDevices);
        
        yield return null;
        
        XRSettings.enabled = true;
    }
}
