using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Grid grid = (Grid)target;
        if (GUILayout.Button("Generate"))
            grid.Generate();
    }
}
