using UnityEngine;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System;
using System.Collections.Generic;

[Action("MyNode/TakePlant")]
[Help("Identifica una pianta nel terreno e la prende.")]
public class IdentifyAndPickPlant : GOAction
{


    [InParam("agentDirection")]
    public Transform direction;

    [InParam("robotHandPosition")]
    [Help("Posizione della mano del robot dove la pianta dovrà essere presa.")]
    public Transform robotHandPosition;

    [OutParam("plant")]
    [Help("Riferimento al GameObject della pianta da prendere.")]
    public GameObject plant;

    [InParam("sickBool")]
    public bool sickBool; //Mi dice se la pianta che sto prendendo è malata (serve per distinguere questo caso da quella in cui è secca o pronta) 

    [InParam("dryBool")]
    public bool dryBool;

    [InParam("matureBool")]
    public bool matureBool;

    [InParam("nextSlotInHouse")]
    public String slotSickPos; 

    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [InParam("pots")]
    public GameObject pots;

    private bool plantPicked = false;

    public override void OnStart()
    {
        base.OnStart();


        FindGameObject();       
        MovePlantToHand();
            
    }

    public override TaskStatus OnUpdate()
    {

        if (plantPicked)
        {
              return TaskStatus.COMPLETED;
        }

        return TaskStatus.RUNNING;
    }

    private void FindGameObject()
    {
        Vector3 rayOrigin = direction.position;
        Vector3 rayDirection = direction.TransformDirection(Vector3.forward); 
        float rayDistance = 20f;

        Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, rayDistance);
        Debug.DrawRay(direction.position, rayDirection * hitInfo.distance, Color.red);
        plant = hitInfo.collider.gameObject;
        Debug.Log($" GameObject: {plant.name}");
         
    }

    private void MovePlantToHand()
    {
        if (plant != null && robotHandPosition != null)
        {
            plant.transform.position = robotHandPosition.position;
            UnityEngine.Object.Destroy(pots);
            plant.transform.SetParent(robotHandPosition);

            PlantStats plantStats = plant.GetComponent<PlantStats>();
            if (sickBool) { //pianta malata
                agentPlantManager.AddSickPlant(slotSickPos, plantStats); 
            }
            if (dryBool) { //pianta secca
            agentPlantManager.AddDryPlant(slotSickPos,plantStats);
            }
            if (matureBool)
            {
               agentPlantManager.AddMaturePlant(slotSickPos, plantStats);
            }

            plantPicked = true;

            Debug.Log($"Il robot ha afferrato la pianta '{plant.name}'");
            //GameObject.Destroy(plant.GetComponent<PlantStats>());
        }
        else
        {
            Debug.LogError("Non è stato possibile spostare la pianta. Controlla che robotHandPosition e plant siano assegnati.");
        }
    }
}
