using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Vuforia;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public Camera arCamera;
    private Plane[] planes;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        arCamera = GetComponent<Camera>();
        Assert.IsNotNull(arCamera);
    }

    private void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(arCamera);
    }

    public bool TestBounds(Bounds bounds)
    {
        if (planes == null)
            return false;

        for (int i = 0; i < 3; ++i)
        {
            Vector3 axis = new Vector3();
            axis[i] = bounds.extents[i];

            for (int j = 0; j < 1; ++j)
            {
                Vector3 point = bounds.center + axis * (j == 0 ? 1.0f : -1.0f);

                foreach (var plane in planes)
                {
                    if (!plane.GetSide(point))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
