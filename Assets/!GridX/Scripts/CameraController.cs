using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviourSingleton<CameraController>
{
    Camera cam;

    public float scaleFactor = 2f;

    [SerializeField] IntEvent gridGeneratedEvent;
    private void OnValidate()
    {
        cam = GetComponent<Camera>();
        gridGeneratedEvent?.RegisterAction(OnGridGenerated);
    }
    private void Awake()
    {
        OnValidate();
    }
    private void OnDisable()
    {
        gridGeneratedEvent?.UnRegisterAction(OnGridGenerated);
    }

    public void OnGridGenerated(int gridSize)
    {
        var scaleRatio = scaleFactor * cam.aspect;

        cam.orthographicSize = gridSize / scaleRatio;
    }
}
