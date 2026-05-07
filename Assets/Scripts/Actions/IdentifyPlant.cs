using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

[Action("MyNode/identifyPlant")]

public class IdentifyPlant : GOAction
{    
    [InParam("idPlant")]
    public string idPlant;

    [InParam("pointName")]
    public string pointName;

    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [InParam("layerMask")]
    [SerializeField] LayerMask layerMask;

    [InParam("agentDirection")]
    public Transform direction; //direzione dell'agente

    [OutParam("plantStats")]
    public PlantStats plantStats;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;


    public override TaskStatus OnUpdate()
    { 

        if (Physics.Raycast(direction.position, direction.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 5f, layerMask))
        {

            Debug.DrawRay(direction.position, direction.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);
            plantStats = hitInfo.transform.GetComponent<PlantStats>();

            if (plantStats != null)
            {
                // Stampa l'ID della piantina nella console
                Debug.Log($"Piantina con ID: {plantStats.getPlantID()} identificata ");

                // Controlla se l'ID corrisponde
                if (idPlant == plantStats.getPlantID())
                {
                    Debug.Log("ID corrisponde!");
                    return TaskStatus.COMPLETED;
                }
                else
                {
                    agentPlantManager.PopulateSearchQueue();
                    if (agentAudioManager != null)
                    {
                        agentAudioManager.MovePlantSound();
                    }
                    Debug.Log("Non ho trovato la pianta che cercavo, ma un'altra: aggiorno la mia base di conoscenza");
                    return TaskStatus.FAILED;
                }
            }
            else
            {
                Debug.Log("L'Oggetto colpito non è una piantina");
                return TaskStatus.FAILED;
            }
        }
        else
        {   // non trova piantine nella posizione
            agentPlantManager.PopulateSearchQueue();
            agentPlantManager.RemovePlant(pointName);
            if (agentAudioManager != null)
            {
                agentAudioManager.SearchPlantSound();
            }
            Debug.Log("Non ho trovato la pianta che cercavo: aggiorno la mia base di conoscenza");
            Debug.DrawRay(direction.position, direction.TransformDirection(Vector3.forward) * 20f, Color.green);
            return TaskStatus.FAILED;
        }

    }

}
