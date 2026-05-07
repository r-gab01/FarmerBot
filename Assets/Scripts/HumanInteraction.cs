using System.Collections.Generic;
using UnityEngine;

public class HumanInteraction : MonoBehaviour
{
    [SerializeField] public AgentPlantManager agentPlantManager;
    private Dictionary<string, PlantStats> plantList; // Lista di piante e relative posizioni nella mappa

    private float timer = 0f;
    private float checkInterval = 30f; // Ogni 30 secondi controlliamo la probabilità
    private float probabilityThreshold = 0.1f;

    private List<Vector3> occupiedPositions = new List<Vector3>(); // Posizioni occupate

    void Start()
    {
        if (agentPlantManager == null)
        {
            Debug.LogError("Riferimento all'agentPlantManager non assegnato al contadino!");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 25f)
        {
            // Avviamo solo se ci sono almeno quattro piante
            if (agentPlantManager.getPlantList().Count < 4)
            {
                return;
            }
        }

        if (timer >= checkInterval)
        {
            float randomValue = Random.value;

            if (randomValue < probabilityThreshold)
            {
                UpdatePlantList();
                MoveRandomPlant();
            }

            timer = 0f;
        }
    }

    private void UpdatePlantList()
    {
        plantList = agentPlantManager.getPlantList();

        if (plantList != null)
        {
            Debug.Log($"Lista delle piante aggiornata: {plantList.Count} piante trovate.");
        }
        else
        {
            Debug.LogError("Impossibile ottenere la lista delle piante dal robot.");
        }
    }

    private void MoveRandomPlant()
    {
        if (plantList == null || plantList.Count == 0)
        {
            Debug.LogWarning("Non ci sono piante nella lista del robot.");
            return;
        }

        // Scegliamo una pianta casuale che il contadino deve spostare.
        int randomIndex = Random.Range(0, plantList.Count);
        string randomKey = new List<string>(plantList.Keys)[randomIndex];
        PlantStats selectedPlant = plantList[randomKey];

        Vector3 randomPosition = GetRandomPosition();

        if (randomPosition != Vector3.zero)
        {

            selectedPlant.transform.position = randomPosition;

            occupiedPositions.Add(randomPosition);

            Debug.Log($"Il contadino ha spostato la pianta '{randomKey}' nella posizione '{randomPosition}'.");
        }
        else
        {
            Debug.LogWarning("Non è stato possibile trovare una posizione libera per spostare la pianta.");
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Posizioni possibili
        List<Vector3> positions = new List<Vector3>
        {
            new Vector3(45, 0.02f, 3),
            new Vector3(38, 0.02f, 3),
            new Vector3(31, 0.02f, 3),
            new Vector3(24, 0.02f, 3)
        };

        // Rimuoviamo le posizioni occupate dalla lista delle posizioni possibili
        List<Vector3> availablePositions = new List<Vector3>(positions);
        foreach (Vector3 occupied in occupiedPositions)
        {
            availablePositions.Remove(occupied);
        }

        // Se ci sono posizioni disponibili, ne scegliamo una casuale
        if (availablePositions.Count > 0)
        {
            return availablePositions[Random.Range(0, availablePositions.Count)];
        }

        // Nessuna posizione libera
        return Vector3.zero;
    }
}
