using UnityEngine;
using System.Collections.Generic;

public class PlantPrefabManager : MonoBehaviour
{

    [System.Serializable]
    public struct PlantPrefabEntry
    {
        public string key;
        public GameObject prefab;
    }

    [Header("Prefabs delle Piante")]
    [SerializeField] private PlantPrefabEntry[] prefabEntries;

    private Dictionary<string, GameObject> plantPrefabs = new Dictionary<string, GameObject>();



    public GameObject sickPrefab;
    public GameObject dryPrefab;
    public GameObject matureTomatoPrefab;
    public GameObject matureZucchinaPrefab;
    public GameObject matureMaisPrefab;

    private void Awake()
    {

        foreach (PlantPrefabEntry entry in prefabEntries)
        {
            if (!plantPrefabs.ContainsKey(entry.key))
            {
                plantPrefabs.Add(entry.key, entry.prefab);
            }
            else
            {
                Debug.LogWarning($"La chiave '{entry.key}' � gi� presente nel dizionario. Controlla i duplicati nell'Inspector.");
            }
        }
    }

    public void SetSickPrefab(GameObject currentPlant, string plantType)
    {
        if (currentPlant != null)
        {
            // Salviamo posizione e rotazione del vecchio GameObject
            Vector3 position = currentPlant.transform.position;
            Quaternion rotation = currentPlant.transform.rotation;
            Transform parent = currentPlant.transform.parent;

            GameObject newPlant = Instantiate(sickPrefab, position, rotation);
            newPlant.transform.parent = parent;
            if (plantType == "Mais")
            {
                position.y -= 0.6f;
            }
            // Se il prefab contiene altri componenti specifici, aggiorna anche quelli
            PlantStats newPlantStats = newPlant.AddComponent<PlantStats>();
            PlantStats currentPlantStats = currentPlant.GetComponent<PlantStats>();
            //Debug.Log($"GET IS SICK '{currentPlantStats.getIsSick()}'.");
            if (newPlantStats != null && currentPlantStats != null)
            {
                newPlantStats.setPlantType(currentPlantStats.getPlantType());
                newPlantStats.setIsMature(currentPlantStats.getIsMatured());
                newPlantStats.setIsDry(currentPlantStats.getIsDry());
                newPlantStats.setIsSick(currentPlantStats.getIsSick());
                newPlantStats.setPlantID(currentPlantStats.getPlantID());
                newPlantStats.setPlantType(currentPlantStats.getPlantType());
                newPlantStats.setPosition(position);
                newPlantStats.setRotation(rotation);
                newPlantStats.setSoilMoisture(currentPlantStats.getSoilMoisture());

                Debug.Log($"PlantStats aggiornato!");
            }
            Destroy(currentPlant);
        }
        else
        {
            Debug.LogError("Il riferimento al vecchio GameObject è nullo.");
        }
    }

    public void SetDryPrefab(GameObject currentPlant, string plantType)
    {
        if (currentPlant != null)
        {
            // Salviamo posizione e rotazione del vecchio GameObject
            Vector3 position = currentPlant.transform.position;
            Quaternion rotation = currentPlant.transform.rotation;
            Transform parent = currentPlant.transform.parent;

            GameObject newPlant = Instantiate(dryPrefab, position, rotation);
            newPlant.transform.parent = parent;
            if (plantType == "Mais")
            {
                position.y -= 0.3f;
            }
            // Se il prefab contiene altri componenti specifici, aggiorna anche quelli
            PlantStats newPlantStats = newPlant.AddComponent<PlantStats>();
            PlantStats currentPlantStats = currentPlant.GetComponent<PlantStats>();
            //Debug.Log($"GET IS SICK '{currentPlantStats.getIsSick()}'.");
            if (newPlantStats != null && currentPlantStats != null)
            {
                newPlantStats.setPlantType(currentPlantStats.getPlantType());
                newPlantStats.setIsMature(currentPlantStats.getIsMatured());
                newPlantStats.setIsDry(currentPlantStats.getIsDry());
                newPlantStats.setIsSick(currentPlantStats.getIsSick());
                newPlantStats.setPlantID(currentPlantStats.getPlantID());
                newPlantStats.setPlantType(currentPlantStats.getPlantType());
                newPlantStats.setPosition(position);
                newPlantStats.setRotation(rotation);
                newPlantStats.setSoilMoisture(currentPlantStats.getSoilMoisture());

                Debug.Log($"PlantStats aggiornato!");
            }
            Destroy(currentPlant);
        }
        else
        {
            Debug.LogError("Il riferimento al vecchio GameObject è nullo.");
        }
    }


    public void SetMaturePrefab(GameObject currentPlant, string plantType)
    {
        if (currentPlant != null)
        {
            // Salviamo posizione e rotazione del vecchio GameObject
            Vector3 position = currentPlant.transform.position;
            Quaternion rotation = currentPlant.transform.rotation;
            Transform parent = currentPlant.transform.parent;
            if (plantType == "Tomato")
            {
                GameObject newPlant = Instantiate(matureTomatoPrefab, position, rotation);
                newPlant.transform.parent = parent;
                // Se il prefab contiene altri componenti specifici, aggiorna anche quelli
                PlantStats newPlantStats = newPlant.AddComponent<PlantStats>();
                PlantStats currentPlantStats = currentPlant.GetComponent<PlantStats>();
                if (newPlantStats != null && currentPlantStats != null)
                {
                    newPlantStats.setPlantType(currentPlantStats.getPlantType());
                    newPlantStats.setIsMature(currentPlantStats.getIsMatured());
                    newPlantStats.setIsDry(currentPlantStats.getIsDry());
                    newPlantStats.setIsSick(currentPlantStats.getIsSick());
                    newPlantStats.setPlantID(currentPlantStats.getPlantID());
                    newPlantStats.setPlantType(currentPlantStats.getPlantType());
                    newPlantStats.setPosition(position);
                    newPlantStats.setRotation(rotation);
                    newPlantStats.setSoilMoisture(currentPlantStats.getSoilMoisture());

                    Debug.Log($"PlantStats aggiornato!");
                }
                Destroy(currentPlant);
            }
            else if (plantType == "Zucchina")
            {
                GameObject newPlant = Instantiate(matureZucchinaPrefab, position, rotation);
                newPlant.transform.parent = parent;
                // Se il prefab contiene altri componenti specifici, aggiorna anche quelli
                PlantStats newPlantStats = newPlant.AddComponent<PlantStats>();
                PlantStats currentPlantStats = currentPlant.GetComponent<PlantStats>();
                if (newPlantStats != null && currentPlantStats != null)
                {
                    newPlantStats.setPlantType(currentPlantStats.getPlantType());
                    newPlantStats.setIsMature(currentPlantStats.getIsMatured());
                    newPlantStats.setIsDry(currentPlantStats.getIsDry());
                    newPlantStats.setIsSick(currentPlantStats.getIsSick());
                    newPlantStats.setPlantID(currentPlantStats.getPlantID());
                    newPlantStats.setPlantType(currentPlantStats.getPlantType());
                    newPlantStats.setPosition(position);
                    newPlantStats.setRotation(rotation);
                    newPlantStats.setSoilMoisture(currentPlantStats.getSoilMoisture());

                    Debug.Log($"PlantStats aggiornato!");
                }
                Destroy(currentPlant);

            }else
            {
                position.y -= 1.5f;
                GameObject newPlant = Instantiate(matureMaisPrefab, position, rotation);
                newPlant.transform.parent = parent;
                // Se il prefab contiene altri componenti specifici, aggiorna anche quelli
                PlantStats newPlantStats = newPlant.AddComponent<PlantStats>();
                PlantStats currentPlantStats = currentPlant.GetComponent<PlantStats>();
                if (newPlantStats != null && currentPlantStats != null)
                {
                    newPlantStats.setPlantType(currentPlantStats.getPlantType());
                    newPlantStats.setIsMature(currentPlantStats.getIsMatured());
                    newPlantStats.setIsDry(currentPlantStats.getIsDry());
                    newPlantStats.setIsSick(currentPlantStats.getIsSick());
                    newPlantStats.setPlantID(currentPlantStats.getPlantID());
                    newPlantStats.setPlantType(currentPlantStats.getPlantType());
                    newPlantStats.setPosition(position);
                    newPlantStats.setRotation(rotation);
                    newPlantStats.setSoilMoisture(currentPlantStats.getSoilMoisture());

                    Debug.Log($"PlantStats aggiornato!");
                }
                Destroy(currentPlant);

            }

        }
        else
        {
            Debug.LogError("Il riferimento al vecchio GameObject è nullo.");
        }
    }

}




    /*public void ChangePlantPrefab(GameObject currentPlant, string plantType, string state)
    {
        string key = $"{plantType}_{state}";

        if (plantPrefabs.TryGetValue(key, out GameObject newPrefab))
        {
            Debug.Log($"Prefab trovato per la chiave '{key}'.");

            if (currentPlant != null)
            {
                // Salviamo posizione e rotazione del vecchio GameObject
                Vector3 position = currentPlant.transform.position;
                Quaternion rotation = currentPlant.transform.rotation;
                Transform parent = currentPlant.transform.parent;

                GameObject newPlant = Instantiate(newPrefab, position, rotation);
                newPlant.transform.parent = parent;
                // Se il prefab contiene altri componenti specifici, aggiorna anche quelli
                PlantStats newPlantStats = newPlant.AddComponent<PlantStats>();
                PlantStats currentPlantStats = currentPlant.GetComponent<PlantStats>();
                Debug.Log($"GET IS SICK '{currentPlantStats.getIsSick()}'.");
                if (newPlantStats != null && currentPlantStats != null)
                {
                    // Aggiorna eventuali propriet� del componente PlantStats
                    newPlantStats.setPlantType(currentPlantStats.getPlantType());
                    newPlantStats.setIsMature(currentPlantStats.getIsMatured());
                    newPlantStats.setIsDry(currentPlantStats.getIsDry());
                    newPlantStats.setIsSick(currentPlantStats.getIsSick());
                    newPlantStats.setPlantID(currentPlantStats.getPlantID());
                    newPlantStats.setPlantType(currentPlantStats.getPlantType());
                    newPlantStats.setPosition(position);
                    newPlantStats.setRotation(rotation);
                    newPlantStats.setSoilMoisture(currentPlantStats.getSoilMoisture());

                    //newPlantStats.setGameObjectType(newPlant);


                    Debug.Log($"PlantStats aggiornato!");
                }
                Destroy(currentPlant);
                Debug.Log($"Prefab cambiato per '{key}'.");
            }
            else
            {
                Debug.LogError("Il riferimento al vecchio GameObject è nullo.");
            }
        }
        else
        {
            Debug.LogError($"Prefab non trovato per la chiave '{key}'.");
        }
    */


