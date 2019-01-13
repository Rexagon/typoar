using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public LayerMask interactableLayers;

    AugmentedButton pressedButton;

    void Update()
    {
        switch(Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began && pressedButton == null)
                        HandleTouchBegin(touch.position);
                    else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                        HandleTouchEnd();
                }
                break;

            case RuntimePlatform.WindowsEditor:
                if (Input.GetMouseButtonDown(0) && pressedButton == null)
                    HandleTouchBegin(Input.mousePosition);
                else if (Input.GetMouseButtonUp(0))
                    HandleTouchEnd();
                break;

            default:
                break;
        }
    }

    void HandleTouchBegin(Vector3 screenPosition)
    {
        var ray = CameraManager.Instance.arCamera.ScreenPointToRay(screenPosition);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 100.0f, interactableLayers))
            return;

        pressedButton = hit.collider.gameObject.GetComponent<AugmentedButton>();
        if (pressedButton == null)
            return;

        pressedButton.SetPressed(true, true);
    }

    void HandleTouchEnd()
    {
        if (pressedButton == null)
            return;

        pressedButton.SetPressed(false, true);
        pressedButton = null;
    }
}
