using TMPro;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] private GameObject PlantInfoUI;
    [SerializeField] private TextMeshProUGUI textNamePrefab;
    [SerializeField] private TextMeshProUGUI textNameStatus;
    [SerializeField] private TextMeshProUGUI textSoilLevel1;
    [SerializeField] private TextMeshProUGUI textSoilLevel2;
    private float soilMoistureLevel;


    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 5f, layerMask))
        {
            //Debug.Log("Piantina Colpita");
            Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hitInfo.distance, Color.red);
            PlantStats plantStats = hitInfo.transform.GetComponent<PlantStats>();

            if (plantStats != null)
            {
                // Stampa l'ID della piantina nella console
                //Debug.Log($"Piantina con ID: {plantStat.getPlantID()} colpita ");

                // Attiva l'UI all'inizio
                PlantInfoUI.SetActive(true);

                // Aggiorna il nome del prefab della piantina
                textNamePrefab.text = $"{plantStats.getPlantType()}";

                //Controllo sullo stato della pianta
                if (plantStats.getIsSick() == false && plantStats.getIsDry() == false && plantStats.getIsMatured() == false)
                {
                    textNameStatus.text = "Pianta Sana";
                }
                else if (plantStats.getIsSick())
                {
                    textNameStatus.text = "Pianta Malata";
                }
                else if (plantStats.getIsDry())
                {
                    textNameStatus.text = "Pianta Secca";
                }
                else if (plantStats.getIsMatured())
                {
                    textNameStatus.text = "Pianta Matura";
                }
                
                soilMoistureLevel = plantStats.getSoilMoisture();
                textSoilLevel1.text = "umidita' suolo:";
                textSoilLevel2.text = $"{soilMoistureLevel:F1}%";
            }
            else
            {
                //Debug.Log("L'Oggetto colpito non è una piantina");
            }
        }
        else
        {
            //Debug.Log("Non ho colpito nulla");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
            // Disattiva l'UI all'inizio
            PlantInfoUI.SetActive(false);
        }
        
    }
}
