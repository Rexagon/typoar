using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedAction : MonoBehaviour
{
    public float delay = 1.0f;
    public UnityEvent action;

    bool waiting = false;

    public void CallAction()
    {
        if (waiting)
            return;

        waiting = true;
        StartCoroutine(RunDelayed());
    }

    public void PreventAction()
    {
        waiting = false;
    }

    private IEnumerator RunDelayed()
    {
        yield return new WaitForSeconds(delay);

        if (!waiting)
            yield break;

        action.Invoke();
        waiting = false;
    }
}
