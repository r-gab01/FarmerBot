using UnityEngine;

public class PlantStats : MonoBehaviour
{
    public string plantID; // Identificativo univoco della pianta
    private string plantType;   // tipo di coltura
    private GameObject plant;   //Riferimento al game object della pianta
    private Vector3 positionPlant; // Posizione della pianta
    private Quaternion rotationPlant; //Rotazione della pianta

    private float soilMoisture = 40f;
    private float requiredMoisture = 80f; // Livello di umidit� per rendere la pianta annaffiata

    private bool isSick = false;     //di default la piantina non è malata
    private bool isDry = false;      //di default la piantina non è secca
    private bool isMature = false;

    private float growth = 0f;  // float per inidicare la crescita della pianta

    private float timeSpent = 0f; // Tempo accumulato
    private float sicknessCheckInterval = 60f; // Intervallo di controllo in secondi
    private float sicknessProbability = 0.02f; // Probabilità che la pianta si ammali (3%)

    [SerializeField]
    private PlantPrefabManager plantPrefabManager;

    void Start()
    {

        if (plantPrefabManager == null)
        {
            //Debug.LogError("NON C'E");
            plantPrefabManager = FindFirstObjectByType<PlantPrefabManager>();
        }
        //setGameObjectType(gameObject);
        //Debug.Log($"PlantStats attaccato a {gameObject.name} con ID: {getPlantID()}");
    }
    void Update()
    {
        DecreaseSoilMoisture();

        if (getSoilMoisture() < 0.1f)
        {
            if (plantPrefabManager != null && isDry == false) //pianta secca
            {
                isDry = true;
                plantPrefabManager.SetDryPrefab(plant, plantType);
                Debug.Log($"La pianta con ID: {plantID} in posizione {transform.position} è seccata per mancanza d'acqua!");
            }
        }

        SimulateGrowing();

        timeSpent += Time.deltaTime;
        if (timeSpent >= sicknessCheckInterval && isSick==false)
        {
            SetSicknessRandomly();
            timeSpent = 0f;
        }
    }

    private void DecreaseSoilMoisture()
    {
        if (isSick == false && isDry == false && isMature == false)
        {
            float moisture = getSoilMoisture();
            moisture -= Time.deltaTime * 0.06f;
            setSoilMoisture(moisture);
        }
    }

    private void SetSicknessRandomly()
    {
        float randomValue = Random.value;   //restituisce un numero tra 0 e 1
        if (randomValue < sicknessProbability)
        {
            isSick = true;

            if (plantPrefabManager != null)
            {
                Debug.Log($"La pianta con ID: {plantID} che si trova in {transform.position} si è ammalata!");
                plantPrefabManager.SetSickPrefab(plant, plantType);
                
            }
            else
            {
                Debug.LogError("PlantPrefabManager non è assegnato o non trovato!");
            }

        }
    }

    public void setSoilMoisture(float moisture)
    {
        if (moisture >= 0 && moisture <= 100)
        {
            soilMoisture = moisture;
        }
    }
    public float getSoilMoisture() { return soilMoisture; }
    public float getRequiredMoisture() { return requiredMoisture; }

    private void SimulateGrowing()
    {
        if (isMature == false)
        {
            if (isDry == false && isSick == false && growth < 100.0f)
            {
                growth += Time.deltaTime * 0.2f;
            }
            else if (isDry == false && isSick == false && growth > 100.0f)
            {
                growth = 100.0f;
                isMature = true;

                if (plantPrefabManager != null)
                {
                    plantPrefabManager.SetMaturePrefab(plant, plantType);
                    Debug.Log($"La pianta con ID: {plantID} che si trova in {transform.position} è matura!");
                }
                else
                {
                    Debug.LogError("PlantPrefabManager non è assegnato o non trovato!");
                }

            }
        }
    }



    public void setPlantID(string newID = null)
    {
        if (newID == null)
        {
            // Se l'ID non è stato impostato, genera un nuovo ID
            plantID = System.Guid.NewGuid().ToString();
        }
        else
        {
            plantID = newID;
            //Debug.Log($"ID: {plantID}");    
        }
    }

    public string getPlantID() { return plantID; }
    public void setPlantType(string type) { plantType = type; }
    public string getPlantType() { return plantType; }
    public bool getIsDry() { return isDry; }
    public void setIsDry(bool watered) { isDry = watered; }
    public bool getIsSick() { return isSick; }
    public void setIsSick(bool watered) { isSick = watered; }

    public bool getIsMatured() { return isMature; }
    public void setIsMature(bool matured) { isMature = matured; }

    public void setGameObjectType(GameObject plantGameObject) 
    {
        plant = plantGameObject;
    }
    public GameObject getGameObjectType() { return plant; }

    public void setPosition(Vector3 position) { positionPlant = position; }
    public Vector3 getPosition() { return positionPlant; }

    public void setRotation(Quaternion rotation) { rotationPlant = rotation; }
    public Quaternion getRotation() { return rotationPlant; }


}