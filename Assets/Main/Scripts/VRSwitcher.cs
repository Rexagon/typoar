using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRSwitcher : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SwitchToVR());
    }

    private void OnDestroy()
    {
        XRSettings.enabled = false;
    }

    IEnumerator SwitchToVR()
    {
        string[] DaydreamDevices = new string[] { "daydream", "cardboard" };
        XRSettings.LoadDeviceByName(DaydreamDevices);
        
        yield return null;
        
        XRSettings.enabled = true;
    }
}
