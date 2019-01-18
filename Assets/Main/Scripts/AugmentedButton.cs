using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Vuforia;

public class AugmentedButton : MonoBehaviour, IVirtualButtonEventHandler
{
    public float delay = 1.0f;
    public string buttonName;
    public Renderer buttonObject;

    Animator buttonAnimator;
    CustomTrackableEventHandler trackable;
    bool isPressed;
    bool pressedFromTouch;
    bool isOccluded;
    bool wasOccluded;
    Collider buttonCollider;

    bool waiting;

    void Start()
    {
        Assert.IsNotNull(buttonObject);
        buttonAnimator = buttonObject.GetComponent<Animator>();

        trackable = transform.parent.GetComponent<CustomTrackableEventHandler>();
        Assert.IsNotNull(trackable);

        var vb = GetComponent<VirtualButtonBehaviour>();
        Assert.IsNotNull(vb);
        vb.RegisterEventHandler(this);

        buttonCollider = GetComponent<Collider>();
    }

    void Update()
    {
        isOccluded = !CameraManager.Instance.TestBounds(buttonObject.bounds);
        if (!wasOccluded && isOccluded) {
            waiting = false;
        }
        wasOccluded = isOccluded;

        buttonObject.enabled = !isOccluded && buttonCollider.enabled;
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        SetPressed(true);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        SetPressed(false);
    }

    public void SetPressed(bool pressed, bool fromTouch = false)
    {
        if (isPressed == pressed || pressed && isOccluded || isPressed && (pressedFromTouch != fromTouch))
            return;

        isPressed = pressed;
        if (pressed)
            pressedFromTouch = fromTouch;

        SetAnimationState(pressed);

        string eventName = buttonName + (pressed ? "Pressed" : "Released");

        if (pressed)
        {
            if (waiting)
                return;

            waiting = true;
            StartCoroutine(FireDelayed(eventName));
        }
        else
        {
            waiting = false;
            trackable.Fire(eventName);
        }
    }
    
    void SetAnimationState(bool pressed)
    {
        if (buttonAnimator == null)
            return;

        buttonAnimator.SetBool("Pressed", pressed);
    }

    IEnumerator FireDelayed(string eventName)
    {
        yield return new WaitForSeconds(delay);

        if (!waiting)
            yield break;

        trackable.Fire(eventName);
        waiting = false;
    }
}
