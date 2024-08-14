using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script planejado para se utilizado em um power down que viraria a c�mera de ponta cabe�a, mas ficou de fora nessa vers�o

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
