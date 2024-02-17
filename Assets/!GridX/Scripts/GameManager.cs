using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [Header("COMMON")]
    [SerializeField] Grid grid;
    [SerializeField] TextMeshProUGUI scoreText;


    [Header("EVENTS")]
    [SerializeField] IntEvent gridGeneratedEvent;
    [SerializeField] DynamicEvent onClusterFindEvent;

    public int clusterScore = 0;

    private void Awake()
    {
        gridGeneratedEvent?.RegisterAction(OnGridGeneratedWrapper);
        onClusterFindEvent?.RegisterAction(OnClusterFindWrapper);
        OnGridGenerated();
    }
    private void OnDisable()
    {
        gridGeneratedEvent?.UnRegisterAction(OnGridGeneratedWrapper);
        onClusterFindEvent?.UnRegisterAction(OnClusterFindWrapper);
    }
    private void OnGridGeneratedWrapper(int _) => OnGridGenerated();
    public void OnGridGenerated()
    {
        clusterScore = 0;
        scoreText.text = "Cluster Score : 0";
        if (GameUtil.InEditor)
            return;
        foreach (var n in grid.SpawnedNodes.Values)
        {
            for (int i = 0; i < ESide.COUNT.Int(); i++)
            {
                var side = ESideExtensions.FromInt(i);
                var checkCoor = n.coordinates + side.GetV2Int();
                if (grid.SpawnedNodes.TryGetValue(checkCoor, out var neighbor))
                {
                    n.SetNeighbors(new KeyValuePair<ESide, Node>(side, neighbor));
                }
            }
        }
    }

    private void OnClusterFindWrapper(dynamic cluster) => OnClusterFind(cluster);
    public void OnClusterFind(List<Node> cluster)
    {
        scoreText.text = $"Cluster Score : {++clusterScore}";
    }
}
