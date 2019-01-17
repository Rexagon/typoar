using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleAction : MonoBehaviour
{
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    bool isActivated;

    public void Activate()
    {
        if (isActivated)
            return;

        isActivated = true;
        onActivate.Invoke();
    }

    public void Deactivate()
    {
        if (!isActivated)
            return;

        isActivated = false;
        onDeactivate.Invoke();
    }

    public void Toggle()
    {
        if (isActivated)
            Deactivate();
        else
            Activate();
    }
}
