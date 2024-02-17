using DG.Tweening;
using Eraslank.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField] Node nodePrefab;
    public int size = 5;

    public IntEvent OnGenerated;
    private Dictionary<Vector2Int, Node> _spawnedNodes = new Dictionary<Vector2Int, Node>();

    public Dictionary<Vector2Int, Node> SpawnedNodes
    {
        get
        {
            //Failsafe Check To Always Return Correct References
            if (_spawnedNodes.Count == 0 || _spawnedNodes.Any(n => !n.Value))
                _spawnedNodes = transform.GetComponentsInChildren<Node>().ToDictionary(n => n.coordinates, n => n);

            return _spawnedNodes;
        }
    }


    public void Generate()
    {
        Node.CAN_CLICK = false;
        transform.DestroyAllChildren(GameUtil.InEditor);
        var posOffset = Vector2.one * ((size / 2f) - .5f);
        transform.position = Vector3.zero - (Vector3)posOffset;

        _spawnedNodes.Clear();

        float delayCounter = 0;
        for (int i = 0; i < size; i++)
        {
            var row = new GameObject($"Row {i}");
            row.transform.SetParent(transform);
            row.transform.SetAsFirstSibling();
            row.transform.localPosition = Vector3.up * i;

            for (int j = 0; j < size; j++)
            {
                var node = Instantiate(nodePrefab, row.transform);
                node.name = $"Node {j}";
                node.transform.localPosition = Vector3.right * j;

                _spawnedNodes.Add(node.coordinates = new Vector2Int(j, i), node);

                if(!GameUtil.InEditor)
                {
                    node.transform.localScale = Vector3.zero;
                    node.transform.DOBounce().SetDelay(delayCounter);
                }
                delayCounter += .05f;
            }
        }


        this.SuperInvoke(() =>
        {
            foreach (var node in _spawnedNodes.Values)
                node.transform.DOScale(1f, .2f).SetEase(Ease.InExpo);
        }, delayCounter + .25f);
        Node.CAN_CLICK = true;
        OnGenerated?.Invoke(size);
    }
}
