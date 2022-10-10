using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Camera camera;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
    }
    private void LateUpdate()
    {
        camera.transform.position = transform.position;
    }
}
