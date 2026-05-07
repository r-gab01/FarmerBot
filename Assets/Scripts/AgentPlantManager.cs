using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentPlantManager : MonoBehaviour
{
    //classe per la gestione delle piante presenti nell'ambiente e del loro stato. 
    
    private Dictionary<string, PlantStats> plantList = new Dictionary<string, PlantStats>();     // insieme di piante piantate nel terreno  (nome posizione, pianta)    
    private Queue<string> monitoredQueue = new Queue<string>();                                  // coda di ID per gestire le piante da monitorare
    private Queue<string> searchQueue = new Queue<string>();                                     // coda con tutte le posizioni mancanti in cui cercare la piantina scomparsa
    private Dictionary<string, PlantStats> plantSickList = new Dictionary<string, PlantStats>(); // insieme di piante che sono malate e si trovano nel casotto  (nome posizione, pianta)    
    private Dictionary<string, PlantStats> plantDryList = new Dictionary<string, PlantStats>();  // insieme di piante che sono secche e si trovano nel casotto  (nome posizione, pianta) 
    private Dictionary<string, PlantStats> plantMatureList = new Dictionary<string, PlantStats>(); // insieme di piante che sono mature e si trovano nel casotto  (nome posizione, pianta) 

    List<string> positionNames = new List<string>
        {
            "PlantPos0",
            "PlantPos1",
            "PlantPos2",
            "PlantPos3",
            "PlantPos4",
            "PlantPos5",
            "PlantPos6",
            "PlantPos7",
            "PlantPos8",
            "PlantPos9",
            "PlantPos10",
            "PlantPos11",
            "PlantPosExtra0",
            "PlantPosExtra1",
            "PlantPosExtra2",
            "PlantPosExtra3"
        };

    private Dictionary<string, string> plantsRestrictions = new Dictionary<string, string>()
    {
        { "PlantPos0", "Tomato" },
        { "PlantPos1", "Tomato" },
        { "PlantPos2", "Tomato" },
        { "PlantPos3", "Tomato" },
        { "PlantPos4", "Zucchina" },
        { "PlantPos5", "Zucchina" },
        { "PlantPos6", "Zucchina" },
        { "PlantPos7", "Zucchina" },
        { "PlantPos8", "Mais" },
        { "PlantPos9", "Mais" },
        { "PlantPos10", "Mais" },
        { "PlantPos11", "Mais" }
    };

    private float resetMonitorTime = 200f; // Dopo il tempo indicato il robot rimonitora le piante

    private float timeSinceLastPrint = 0f;  // Timer per gestire il tempo
    private float printInterval = 8f;      // Intervallo di 5 secondi

    void Update()
    {
        // Incrementa il timer con il tempo trascorso
        timeSinceLastPrint += Time.deltaTime;

        // Se sono passati 5 secondi, stampa la coda e resetta il timer
        if (timeSinceLastPrint >= printInterval)
        {
            PrintQueue();
            timeSinceLastPrint = 0f;  // Reset del timer
        }
    }


    void PrintQueue()
    {
        string queueContents = "Contenuto della coda: ";

        foreach (var item in monitoredQueue)
        {
            queueContents += GetPlantPos(item) + " ";
        }

        Debug.Log(queueContents);  // Stampa la coda nella console
    }


    public PlantStats GetPlantStats(string key)
    {
        if (plantList.TryGetValue(key, out PlantStats plantStats))
        {
            return plantStats; // Chiave trovata, restituisci il valore
        }

        Console.WriteLine($"Chiave '{key}' non trovata.");
        return null; // Oppure restituisci un valore predefinito
    }

    public string GetPlantPos(string id)
    {
        foreach (var entry in plantList)
        {
            if (entry.Value.getPlantID() == id)
            {
                return entry.Key;
            }
        }

        return null;
    }

    public Dictionary<string, PlantStats> getPlantList() { return plantList; }

    public void PopulateSearchQueue()
    {
        foreach (string pos in positionNames)
        {
            searchQueue.Enqueue(pos);
        }
    }

    public void SearchQueueEnqueue(string plantKey)
    {
        searchQueue.Enqueue(plantKey);
    }

    // Rimuove e restituisce la pianta più vecchia dalla coda
    public string SearchQueueDequeue()
    {
        if (searchQueue.Count > 0)
        {
            return searchQueue.Dequeue();
        }
        else
        {
            throw new InvalidOperationException("La coda è vuota.");
        }
    }

    public bool SearchQueueIsEmpty()
    {
        return searchQueue.Count == 0;
    }

    public string SearchQueuePeek()
    {
        if (searchQueue.Count > 0)
        {
            return searchQueue.Peek();
        }
        else
        {
            throw new InvalidOperationException("La coda è vuota.");
        }
    }

    public void MonitoredQueueEnqueue(string plantKey)
    {
        monitoredQueue.Enqueue(plantKey);
    }

    // Rimuove e restituisce la pianta più vecchia dalla coda
    public string MonitoredQueueDequeue()
    {
        if (monitoredQueue.Count > 0)
        {
            return monitoredQueue.Dequeue();
        }
        else
        {
            throw new InvalidOperationException("La coda è vuota.");
        }
    }

    public bool MonitoredQueueIsEmpty()
    {
        if (monitoredQueue.Count == 0)
            Debug.LogWarning($"MonitoredQueue is empty: {monitoredQueue.Count}");
        return monitoredQueue.Count == 0;
    }

    public string MonitoredQueuePeek()
    {
        if (monitoredQueue.Count > 0)
        {
            return monitoredQueue.Peek();
        }
        else
        {
            throw new InvalidOperationException("La coda è vuota.");
        }
    }

    public string GetSlotRestriction(string slot)       // metodo per ottenere che tipo di pianta piantare nello slot passato come argomento
    {
        if (plantsRestrictions.TryGetValue(slot, out string allowedPlantType))
        {
            return allowedPlantType; 
        }
        else
        {
            return null;
        }
    }


    //Implementiamo il meccanismo di reset del variabile Monitored tramite coroutine
    IEnumerator ResetMonitoredStatus(string plantID, float delay) // IEnumerator è il tipo speciale che devono restituire le coroutine
    {
        yield return new WaitForSeconds(delay);

        if (!monitoredQueue.Contains(plantID))      // se non è già in coda l'appendo alla coda
        {
            MonitoredQueueEnqueue(plantID);
            Debug.Log($"La pianta '{plantID}' in posizione '{GetPlantPos(plantID)}' andrebbe monitorata!");
          
        }
    }

    public void SetMonitored(bool coroutine)
    {
        string plantID = MonitoredQueueDequeue();
        Debug.Log($"La pianta '{plantID}' in posizione '{GetPlantPos(plantID)}' è stata monitorata.");
        if (coroutine)
        {
            StartCoroutine(ResetMonitoredStatus(plantID, resetMonitorTime));
        }
        

    }

    public void PlantFound(string plantPos, PlantStats plantStats) // metodo richiamato quando viene trovata la pianta mancante in una nuova posizione. Il metodo aggiorna la posizione della pianta e svuota la coda di ricerca
    {
        if (!plantList.ContainsKey(plantPos))
        {
            plantList.Add(plantPos, plantStats);
            searchQueue.Clear();
            Debug.Log($"Pianta aggiunta alla lista di piante in posisione '{plantPos}'");
        }
        else
        {
            Debug.LogError($"Una pianta è già presente in posizione '{plantPos}'");
        }
    }


    public void AddPlant(string plantPos, PlantStats plantStats)
    {
        if (!plantList.ContainsKey(plantPos))
        {
            plantList.Add(plantPos, plantStats);
            monitoredQueue.Enqueue(GetPlantID(plantPos));
            Debug.Log($"Pianta aggiunta alla lista di piante in posisione '{plantPos}'");
        }
        else
        {
            Debug.LogError($"Una pianta è già presente in posizione '{plantPos}'");
        }
    }

    public void RemovePlant(string plantPos) 
    {
        if (plantList.ContainsKey(plantPos))
        {
            String plantID = GetPlantID(plantPos);
            plantList.Remove(plantPos);
            //monitoredPlants.Remove(plantID); Eliminato adesso perchè altrimenti la coroutine non poteva essere completata
            Debug.Log($"Rimuovo la pianta in posizione '{plantPos}' dalla lista di piante");
        }
        else
        {
            Debug.Log($"La pianta in posizione {plantPos} non è presente nella lista di piante.");
        }
    }

    public void AddSickPlant(string plantPos, PlantStats plantStats) // Aggiunge una pianta che è malata nel corrispettivo casotto
    {
        if (!plantSickList.ContainsKey(plantPos))
        {
            plantSickList.Add(plantPos, plantStats);
            Debug.Log($"Aggiungo la pianta in posizione '{plantPos}' alla lista di piante");
        }
        else
        {
            Debug.Log($"La pianta in posizione {plantPos} è già presente nella lista di piante.");
        }
    }

    public void RemoveSickPlant(string plantPos) // Rimuove una pianta che era malata dal casotto
    {
        if (plantSickList.ContainsKey(plantPos))
        {
            String plantID = GetPlantID(plantPos);
            plantSickList.Remove(plantPos);
            Debug.Log($"Rimuovo la pianta in posizione {plantPos} dalla lista di piante malate");
        }
        else
        {
            Debug.LogError($"La pianta '{plantPos}' non è presente nella lista delle piante malate.");
        }
    }

    public PlantStats GetSickSlot(string key) //Verifica nel casotto delle piante malate qual'è il prossimo slot libero
    {
        if (plantSickList.TryGetValue(key, out PlantStats plantStats))
        {
            return plantStats; 
        }

        Console.WriteLine($"Chiave '{key}' non trovata.");
        return null; 
    }

    public PlantStats GetMatureSlot(string key) //Verifica nel casotto delle piante mature qual'è il prossimo slot libero
    {
        if (plantMatureList.TryGetValue(key, out PlantStats plantStats))
        {
            return plantStats;
        }

        Console.WriteLine($"Chiave '{key}' non trovata.");
        return null;
    }

    public void AddMaturePlant(string plantPos, PlantStats plantStats) // Aggiunge una pianta che è matura nel corrispettivo casotto
    {
        if (!plantMatureList.ContainsKey(plantPos))
        {
            plantMatureList.Add(plantPos, plantStats);
            Debug.Log($"Pianta matura posizionata in {plantPos}");
        }
        else
        {
            Debug.LogError($"La pianta '{plantPos}' è già presente nella lista delle piante mature.");
        }
    }

    public void AddDryPlant(string plantPos, PlantStats plantStats) // Aggiunge una pianta che è secca nel corrispettivo casotto
    {
        if (!plantDryList.ContainsKey(plantPos))
        {
            plantDryList.Add(plantPos, plantStats);
            Debug.Log($"Pianta secca posizionata in {plantPos}");
        }
        else
        {
            Debug.LogError($"La pianta '{plantPos}' è già presente nella lista delle piante secche.");
        }
    }

    public void RemoveDryPlant(string plantPos) // Rimuove una pianta che era secca dal casotto
    {
        if (plantDryList.ContainsKey(plantPos))
        {
            String plantID = GetPlantID(plantPos);
            plantDryList.Remove(plantPos);
            Debug.Log($"Pianta: {plantPos} eliminata dalla lista di piante secche");
        }
        else
        {
            Debug.LogError($"La pianta '{plantPos}' non è presente nella lista delle piante secche.");
        }
    }

    public PlantStats GetDrySlot(string key) //Verifica nel casotto delle piante secche qual'è il prossimo slot libero
    {
        if (plantDryList.TryGetValue(key, out PlantStats plantStats))
        {
            return plantStats;
        }

        Console.WriteLine($"Chiave '{key}' non trovata.");
        return null;
    }

    public string GetNextPlantToWater()     // restituisce l'id della pianta da innaffiare
    {
        if (!MonitoredQueueIsEmpty())
        {
            string plantID = MonitoredQueuePeek();
            string otherID;
            foreach (var entry in plantList)
            {
                otherID = entry.Value.getPlantID();
                if (otherID == plantID)
                {
                    Debug.Log($"Prossima pianta da monitorare in posizione: {entry.Key}");
                    return entry.Key;
                }
            }
            Debug.LogWarning("Gli id non corrispondono");
        }
        Debug.LogWarning("RITORNO NULL");
        return null;
    }

    public string GetPlantID(string key)
    {
        if (plantList.ContainsKey(key))
        {
            return plantList[key].getPlantID();
        }
        else
        {
            Debug.LogWarning($"Chiave '{key}' non trovata nel dizionario.");
            return null;
        }
    }
}
