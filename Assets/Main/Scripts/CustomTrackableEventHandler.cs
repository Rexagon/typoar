using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        Fire("TrackingFound");
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        Fire("TrackingLost");
    }

    public void Fire(string eventName)
    {
        foreach (var eventManager in GetComponentsInChildren<EventManager>())
        {
            eventManager.Fire(eventName);
        }
    }
}
