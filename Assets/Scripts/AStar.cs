using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder : MonoBehaviour
{
    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        List<Node> openSet = new List<Node> { startNode };
        HashSet<Node> closedSet = new HashSet<Node>();
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        Dictionary<Node, float> gCost = new Dictionary<Node, float>();
        Dictionary<Node, float> fCost = new Dictionary<Node, float>();

        foreach (var node in openSet) gCost[node] = float.MaxValue;
        foreach (var node in openSet) fCost[node] = float.MaxValue;

        gCost[startNode] = 0;
        fCost[startNode] = Heuristic(startNode, targetNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            foreach (Node node in openSet)
            {
                if (fCost[node] < fCost[currentNode])
                    currentNode = node;
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
                return RetracePath(cameFrom, startNode, targetNode);

            foreach (Node neighbor in currentNode.neighbors)
            {
                if (closedSet.Contains(neighbor)) continue;

                float tentativeGCost = gCost[currentNode] + Vector3.Distance(currentNode.position, neighbor.position);
                if (tentativeGCost < gCost.GetValueOrDefault(neighbor, float.MaxValue))
                {
                    cameFrom[neighbor] = currentNode;
                    gCost[neighbor] = tentativeGCost;
                    fCost[neighbor] = gCost[neighbor] + Heuristic(neighbor, targetNode);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return null; // Nessun percorso trovato
    }

    float Heuristic(Node a, Node b)
    {
        return Vector3.Distance(a.position, b.position);
    }

    List<Node> RetracePath(Dictionary<Node, Node> cameFrom, Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = cameFrom[currentNode];
        }
        path.Reverse();
        return path;
    }
}
