using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BumperSensor : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 lastDestination; // Memorizza la destinazione originale

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastDestination = agent.destination; // Salva la destinazione iniziale
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plant"))
        {
            Debug.Log("Urto rilevato con: " + collision.gameObject.name);
            StartCoroutine(HandleCollision());
        }
    }

    IEnumerator HandleCollision()
    {
        // Ferma il robot
        agent.isStopped = true;

        // Aspetta 1 secondo
        yield return new WaitForSeconds(1f);

        // Ricalcola il percorso verso la stessa destinazione
        agent.SetDestination(lastDestination);
        agent.isStopped = false;

        Debug.Log("Ricalcolato il percorso verso: " + lastDestination);
    }
}

