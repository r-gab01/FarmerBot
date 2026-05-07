using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Node
{
    public Vector3 position;
    public List<Node> neighbors = new List<Node>();

    public Node(Vector3 pos)
    {
        position = pos;
    }
}

public class NavMeshGraph : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();

    [Header("Graph Settings")]
    private Vector3 minBounds = new Vector3(-72, 0, -50);
    private Vector3 maxBounds = new Vector3(72, 0, 50);
    public float stepSize = 1.0f;
    public float connectionDistance = 1.5f;

    private void Start()
    {
        GenerateGraph(minBounds, maxBounds, stepSize);
        ConnectNodes(connectionDistance);
        Debug.Log($"Grafo generato con {nodes.Count} nodi.");
    }

    /// <summary>
    /// Genera i nodi validi sulla NavMesh in un'area definita.
    /// </summary>
    public void GenerateGraph(Vector3 minBounds, Vector3 maxBounds, float stepSize)
    {
        nodes.Clear();

        for (float x = minBounds.x; x <= maxBounds.x; x += stepSize)
        {
            for (float z = minBounds.z; z <= maxBounds.z; z += stepSize)
            {
                Vector3 samplePosition = new Vector3(x, 0, z);
                NavMeshHit hit;
                if (NavMesh.SamplePosition(samplePosition, out hit, stepSize, NavMesh.AllAreas))
                {
                    nodes.Add(new Node(hit.position));
                }
            }
        }
    }

    /// <summary>
    /// Connette i nodi vicini se non ci sono ostacoli tra di loro.
    /// </summary>
    public void ConnectNodes(float maxDistance)
    {
        foreach (Node node in nodes)
        {
            foreach (Node other in nodes)
            {
                if (node != other && Vector3.Distance(node.position, other.position) <= maxDistance)
                {
                    if (!NavMesh.Raycast(node.position, other.position, out _, NavMesh.AllAreas))
                    {
                        node.neighbors.Add(other);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Trova il nodo più vicino a una data posizione.
    /// </summary>
    public Node FindClosestNode(Vector3 position)
    {
        if (nodes.Count == 0)
        {
            Debug.LogError("Nessun nodo disponibile nel grafo.");
            return null;
        }

        Node closestNode = null;
        float minDistance = float.MaxValue;

        foreach (Node node in nodes)
        {
            float distance = Vector3.SqrMagnitude(node.position - position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestNode = node;
            }
        }

        if (closestNode == null)
        {
            Debug.LogWarning($"Nessun nodo trovato vicino a {position}");
        }

        return closestNode;
    }
}
