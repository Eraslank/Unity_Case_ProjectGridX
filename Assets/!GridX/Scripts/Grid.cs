using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField] Node nodePrefab;
    public int size = 5;

    public static UnityAction<int> OnGenerated;
    private bool Immediate //True if in editor
    {
        get { return !Application.isPlaying; }
    }

    public void Generate()
    {
        transform.DestroyAllChildren(Immediate);
        var posOffset = Vector2.one * ((size / 2f) - .5f);
        transform.position = Vector3.zero - (Vector3)posOffset;

        for (int i = 0; i < size; i++)
        {
            var row = new GameObject($"Row {i}");
            row.transform.SetParent(transform);
            row.transform.SetAsFirstSibling();
            row.transform.localPosition = Vector3.up * i;

            for(int j = 0; j < size; j++)
            {
                var node = Instantiate(nodePrefab, row.transform);
                node.name = $"Node {j}";
                node.transform.localPosition = Vector3.right * j;
            }
        }

        OnGenerated?.Invoke(size);
        CameraController.Instance.OnGridGenerated(size); //TEST

    }
}
