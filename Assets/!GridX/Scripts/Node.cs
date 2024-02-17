using DG.Tweening;
using Eraslank.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerClickHandler
{
    [Header("COMMON")]
    public Vector2Int coordinates;

    [SerializeField] SpriteRenderer xImage;
    [SerializeField] List<Neighbor> neighbors;

    public static bool CAN_CLICK = true;

    [Header("EVENTS")]
    [SerializeField] DynamicEvent OnClusterFind;

    public void SetNeighbors(params KeyValuePair<ESide, Node>[] neighbors)
    {
        if (this.neighbors == null)
            this.neighbors = new List<Neighbor>();

        foreach (var n in neighbors)
            if (!this.neighbors.Any(x => x.node == n.Value))
                this.neighbors.Add(new Neighbor(n.Key, n.Value));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CAN_CLICK)
            return;

        if (xImage.gameObject.activeSelf)
            return;

        CAN_CLICK = false;

        Spawn();

        Invoke(nameof(TryFindCluster), .5f);
    }

    public void SetXState(bool state)
    {
        xImage.gameObject.SetActive(state);
    }

    public void TryFindCluster()
    {
        var cluster = FindCluster();
        if (cluster.Count > 2)
        {
            this.SuperInvoke(() =>
            {
                foreach (var n in cluster)
                    n.DeSpawn();
                OnClusterFind?.Invoke(cluster);
                CAN_CLICK = true;
            }, .25f);

            return;
        }
        CAN_CLICK = true;
    }
    public List<Node> FindCluster(List<Node> cluster = null, List<Node> alreadyChecked = null)
    {
        if (alreadyChecked == null)
        {
            alreadyChecked = new List<Node>();
            cluster = new List<Node>();
        }

        alreadyChecked.Add(this);

        if (xImage.gameObject.activeSelf)
            cluster.Add(this);
        else
            return cluster;

        foreach (var n in neighbors)
        {
            if (!alreadyChecked.Contains(n.node))
                n.node.FindCluster(cluster, alreadyChecked);
        }

        return cluster;
    }

    public void Spawn()
    {
        SetXState(true);
        xImage.color = new Color();
        xImage.transform.localScale = Vector3.one * .9f;
        DOVirtual.Float(0, 1, .5f, x =>
        {
            xImage.color = new Color(1, 1, 1, x);
        });

        xImage.transform.DOBounce().SetDelay(.25f);
        xImage.transform.DORotate(Vector3.zero, .25f)
                        .From(Vector3.forward * -30f)
                        .SetEase(Ease.InOutBack);
    }

    public void DeSpawn()
    {
        xImage.transform.DOScale(0, .25f).SetEase(Ease.InBack).OnComplete(() =>
        {
            SetXState(false);
        });
    }
}

[System.Serializable]
public struct Neighbor
{
    public ESide side;
    public Node node;

    public Neighbor(ESide side, Node node)
    {
        this.side = side;
        this.node = node;
    }
}