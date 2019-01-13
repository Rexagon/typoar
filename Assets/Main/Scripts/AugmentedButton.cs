using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Vuforia;

public class AugmentedButton : MonoBehaviour, IVirtualButtonEventHandler
{
    public string buttonName;
    public Renderer buttonObject;

    Animator buttonAnimator;
    CustomTrackableEventHandler trackable;
    bool isPressed;
    bool pressedFromTouch;
    bool isOccluded;
    Collider buttonCollider;

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

        trackable.Fire(buttonName + (pressed ? "Pressed" : "Released"));
        SetAnimationState(pressed);
    }
    
    void SetAnimationState(bool pressed)
    {
        if (buttonAnimator == null)
            return;

        buttonAnimator.SetBool("Pressed", pressed);
    }
}
