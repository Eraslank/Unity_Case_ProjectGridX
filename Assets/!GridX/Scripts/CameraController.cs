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

    public void OnGridGenerated(int size)
    {
        //var ortoSize = (float)size * 3000 / (float)cam.pixelHeight;
        var aspect = cam.aspect < 0.5f ? cam.aspect + cam.aspect : cam.aspect;
        Debug.Log(cam.pixelRect);
        Debug.Log(cam.aspect);
        Debug.Log(aspect);
        //cam.orthographicSize = ortoSize;




        var reference = new Rect(0, 0, 1080, 1920);
        var ortoSize = (cam.pixelWidth / reference.width) /*+ (cam.pixelHeight / reference.height)*/;
        var scaleRatio = .6f;
        if (cam.aspect < .5f)
            scaleRatio += 1;

        cam.orthographicSize = (ortoSize * (size / 2f)) * scaleFactor * scaleRatio;
    }
}
