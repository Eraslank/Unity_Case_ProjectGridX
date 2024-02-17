using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviourSingleton<CameraController>
{
    [Header("COMMON")]
    public float scaleFactor = 2f;

    Camera cam;

    [Header("EVENTS")]
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
