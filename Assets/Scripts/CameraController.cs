using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void InvertCamera(bool invert)
    {
        mainCam.transform.eulerAngles = invert ? new Vector3(0, 0, 180) : new Vector3(0, 0, 0);
    }
}
