using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] IntEvent gridGeneratedEvent;

    [SerializeField] Grid grid;

    private void OnValidate()
    {
        gridGeneratedEvent.RegisterAction(SetNeighborsWrapper);
    }
    private void Start()
    {
        OnValidate();
        SetNeighbors();
    }
    private void OnDisable()
    {
        gridGeneratedEvent.UnRegisterAction(SetNeighborsWrapper);
    }
    private void SetNeighborsWrapper(int _) => SetNeighbors();
    public void SetNeighbors()
    {
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
}
