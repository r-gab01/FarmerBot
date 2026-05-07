using UnityEngine;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System;
using static CurveExtended.CurveExtension;

[Action("MyNode/PlacePlant")]
[Help("Posiziona una pianta in un punto specifico una volta che è nella mano del robot.")]
public class PlacePlantAtTarget : GOAction
{
    [InParam("plant")]
    [Help("Il GameObject della pianta da posizionare.")]
    public GameObject plant;

    [InParam("robotHandPosition")]
    [Help("La posizione della mano del robot che tiene la pianta.")]
    public Transform robotHandPosition;

    [InParam("agentDirection")]
    public Transform agentDirection;

    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [InParam("nextPlantPos")]
    public String plantPosition;//Posizione della pianta. 

    [InParam("nextPlant")]
    public String PlantID;

    [InParam("matureBool")]
    public bool matureBool;

    [InParam("PlantInfoUI")]
    [SerializeField] private GameObject PlantInfoUI;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;


    private bool plantPlaced = false;

    public override void OnStart()
    {

        base.OnStart();


        if (plant != null && robotHandPosition != null && plant.transform.IsChildOf(robotHandPosition))
        {
            Debug.Log($"La pianta '{plant.name}' è pronta per essere spostata.");
        }
        else
        {
            Debug.LogWarning("La pianta non è nella mano del robot o i riferimenti non sono corretti.");
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (plant != null && !plantPlaced)
        {
            PlacePlant();

            if (plantPlaced)
            {
                //PlantInfoUI.SetActive(false);
                return TaskStatus.COMPLETED;
            }
        }

        return TaskStatus.RUNNING;
    }

    private void PlacePlant()
    {
        if (plant != null)
        {
            PlantStats plantStats = plant.GetComponent<PlantStats>();

            if (matureBool)
            {
                agentPlantManager.SetMonitored(false);
                agentPlantManager.RemovePlant(plantPosition);
            }

            //Una volta che la pianta è stata posata, la rimuoviamo dalla lista di piante che devono essere monitorate, e che sono piantate.
            //Inoltre setto il booleano a True
            else
            {
                agentPlantManager.SetMonitored(false);
                agentPlantManager.RemovePlant(plantPosition);
            }
            
            
            plant.transform.SetParent(null);
            Vector3 plantTablePosition = agentDirection.position + agentDirection.forward * 3 + Vector3.up * 1; 
            if (plantStats.getPlantType() == "mais" && matureBool==true)
            {
              plantTablePosition = agentDirection.position + agentDirection.forward * 3 + Vector3.up * -1;
            }

            plant.transform.position = plantTablePosition;
            plantPlaced = true;
            //GameObject.Destroy(plant.GetComponent<PlantStats>());

            // Riproduciamo il suono tramite l'AgentAudioManager
            if (agentAudioManager != null)
            {
                agentAudioManager.PlacePlantSound();
            }
            Debug.Log($"La pianta '{plant.name}' è stata posizionata con successo a {plantTablePosition}.");
        }
        else
        {
            Debug.LogError("Impossibile posizionare la pianta. Il riferimento al GameObject è nullo.");
        }
    }
}
