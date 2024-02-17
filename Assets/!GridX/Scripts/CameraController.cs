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

    private void OnValidate()
    {
        cam = GetComponent<Camera>();
    }
    private void OnEnable()
    {
        Grid.OnGenerated += OnGridGenerated;
    }
    private void OnDisable()
    {
        Grid.OnGenerated -= OnGridGenerated;
    }

    public void OnGridGenerated(int gridSize)
    {
        var scaleRatio = scaleFactor * cam.aspect;

        cam.orthographicSize = gridSize / scaleRatio;
    }
}
