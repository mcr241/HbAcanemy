using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera camera;
    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
    }

    private void LateUpdate()
    {
        camera.transform.position = transform.position;
    }

}
