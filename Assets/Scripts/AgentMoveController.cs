using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentMoveController : MonoBehaviour
{
    public AStarPathfinder pathfinder; // Algoritmo A*
    public NavMeshGraph graph; // Collegamento al grafo dei nodi
    private List<Node> path; // Percorso calcolato
    private Node targetNode;

    private int currentNodeIndex = 0;
    private float moveSpeed = 6;
    private float speed;
    private float rotationSpeed = 80;       // Velocità di movimento
    public float reachThreshold; // Distanza per considerare raggiunto un nodo

    public float remainingDistance;

    void Start()
    {

    }

    void Update()
    {
        if (targetNode != null)
        {
            remainingDistance = Vector3.Distance(transform.position, targetNode.position);
        }
        
    }

    public void SetTarget(Vector3 targetPosition)
    {
        
        Node startNode = graph.FindClosestNode(transform.position);
        targetNode = graph.FindClosestNode(targetPosition);

        if (startNode == null || targetNode == null)
        {
            Debug.LogError("Nodi di partenza o arrivo non trovati!");
            return;
        }

        path = pathfinder.FindPath(startNode, targetNode);

        if (path == null || path.Count == 0)
        {
            Debug.LogWarning("Nessun percorso trovato!");
            return;
        }
       
        currentNodeIndex = 0;
        StartCoroutine(FollowPath());
    }

    /// <summary>
    /// Segue il percorso calcolato muovendosi nodo per nodo
    /// </summary>
    /// 
    IEnumerator FollowPath()
    {
        while (currentNodeIndex < path.Count)
        {
            if (currentNodeIndex == 0)
            {
                speed = 2f;
            }
            else
            {
                speed = moveSpeed;
            }

            if (currentNodeIndex == path.Count-1)
            {
                reachThreshold = 0.6f;
            }
            Debug.Log(path[currentNodeIndex].position);
            Vector3 direction = (path[currentNodeIndex].position - transform.position).normalized;
            direction.y = 0;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            
            // Muove l'agente verso la posizione del nodo corrente
            while (Vector3.Distance(transform.position, path[currentNodeIndex].position) > reachThreshold)
            {
                // Muovere l'agente verso il prossimo waypoint
                Debug.Log(Vector3.Distance(transform.position, path[currentNodeIndex].position));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, path[currentNodeIndex].position, speed * Time.deltaTime);
                yield return null;
            }

            // Una volta arrivato al waypoint corrente, passa al successivo
            currentNodeIndex++;
        }
    }
}
