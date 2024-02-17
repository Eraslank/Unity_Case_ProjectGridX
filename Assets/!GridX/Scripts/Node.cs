using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject xImage;

    public Vector2Int coordinates;

    [SerializeField] List<Neighbor> neighbors;

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
        SetXState(true);


        var cluster = FindCluster();
        if (cluster.Count > 2)
        {
            foreach (var n in cluster)
                n.SetXState(false);
            Debug.Log("Cluster");
        }

    }

    public void SetXState(bool state)
    {
        xImage.SetActive(state);
    }

    public List<Node> FindCluster(List<Node> cluster = null, List<Node> alreadyChecked = null)
    {
        if (alreadyChecked == null)
        {
            alreadyChecked = new List<Node>();
            cluster = new List<Node>();
        }

        alreadyChecked.Add(this);

        if (xImage.activeSelf)
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