using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

[Action("MyNode/NextFreeSlot")]
public class NextFreeSlot : GOAction
{
    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [OutParam("nextFreeSlot")]
    public String nextFreeSlot;

    private List<String> plantPosition = new List<String> { "PlantPos0", "PlantPos1", "PlantPos2", "PlantPos3", "PlantPos4", "PlantPos5", "PlantPos6", "PlantPos7", "PlantPos8", "PlantPos9", "PlantPos10", "PlantPos11", };

    public override TaskStatus OnUpdate()
    {
        foreach (string str in plantPosition)
        {
            Dictionary<string, PlantStats> plantList = agentPlantManager.getPlantList();
            if (!plantList.ContainsKey(str))
            {
                Debug.Log($"Slot libero trovato: {str} ");
                nextFreeSlot = str;
                return TaskStatus.COMPLETED;
            }
        }
        return TaskStatus.FAILED;

    }
}
