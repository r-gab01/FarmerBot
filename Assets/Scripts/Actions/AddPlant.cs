using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections.Generic;
using System;
using Unity.AI.Navigation;

[Action("MyNode/AddPlant")]
public class AddPlant : GOAction
{

    [InParam("prefabPomodoro")]
    public GameObject prefabPomodoro;
    [InParam("prefabZucchina")]
    public GameObject prefabZucchina;
    [InParam("prefabMais")]
    public GameObject prefabMais;

    [InParam("plantParent")]
    public Transform plantParent;

    [InParam("agent")]
    public NavMeshSurface agent;

    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [InParam("nextPlantPosition")]
    public String nextPlantPosition;

    [InParam("agentDirection")]
    public Transform agentDirection;

    [OutParam("plantType")]
    public string plantType;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    public override TaskStatus OnUpdate()
    {
        if (prefabPomodoro == null || prefabZucchina == null)
        {
            Debug.Log("prefab piantina è NULL");
            return TaskStatus.FAILED;
        }
        if (agentPlantManager == null)
        {
            Debug.Log("AgentPlanManager è NULL");
            return TaskStatus.FAILED;
        }
        if (plantParent == null)
        {
            Debug.Log("plantParent è NULL");
            return TaskStatus.FAILED;
        }
        if (agent == null)
        {
            Debug.Log("navMeshSurface è NULL");
            return TaskStatus.FAILED;
        }
        if (nextPlantPosition == null)
        {
            Debug.Log("nextPlantPosition è NULL");
            return TaskStatus.FAILED;
        }

        IstantiatePlant();
        return TaskStatus.COMPLETED;

    }

    private void IstantiatePlant()
    {
        Vector3 plantPosition = agentDirection.position + agentDirection.forward * 2f;
        plantPosition.y -= 0.5f;

        string plantType = agentPlantManager.GetSlotRestriction(nextPlantPosition);
        GameObject prefab = null;

        if (plantType == "Tomato")
        {
            prefab = prefabPomodoro;
        }
        else if (plantType == "Zucchina")
        {
            prefab = prefabZucchina;
        }else if(plantType == "Mais")
        {
            prefab = prefabMais;
            plantPosition.y += 0.37f;
        }

        GameObject newPlant = GameObject.Instantiate(prefab, plantPosition, Quaternion.identity) as GameObject;
        newPlant.transform.parent = plantParent;
    
        PlantStats plant = newPlant.GetComponent<PlantStats>();

        if (plant != null)
        {
            plant.setPlantID();
            plant.setPlantType(plantType);
            plant.setGameObjectType(newPlant);
            //tengo il riferimento della posizione e rotazione della pianta perchè mi servirà successivamente
            Vector3 position = newPlant.transform.position;
            Quaternion rotation = newPlant.transform.rotation;

            plant.setPosition(position);
            plant.setRotation(rotation);


            agentPlantManager.AddPlant(nextPlantPosition, plant);

            //aggiorniamo la navmesh
            if (agent != null)
            {
                agent.BuildNavMesh();
            }

            if (agentAudioManager != null)
            {
                agentAudioManager.PlayPlantingSound();
            }

        }
        else
        {
            Debug.Log("plant stat è null");
        }
    }
}
