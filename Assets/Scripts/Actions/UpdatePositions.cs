using UnityEngine;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

[Action("MyNode/UpdatePositions")]

public class UpdatePositions : GOAction
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
                // Controlla se l'ID corrisponde
                if (idPlant == plantStats.getPlantID())
                {
                    agentPlantManager.PlantFound(pointName, plantStats);
                    // Riproduciamo il suono tramite l'AgentAudioManager
                    if (agentAudioManager != null)
                    {
                        agentAudioManager.FindPlantSound();
                    }
                    Debug.Log("Pianta trovata, nuova posizione salvata nella base di conoscenza");
                    return TaskStatus.COMPLETED;
                }
                else
                {
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
        { 
            Debug.DrawRay(direction.position, direction.TransformDirection(Vector3.forward) * 20f, Color.green);
            return TaskStatus.FAILED;
        }

    }

}
