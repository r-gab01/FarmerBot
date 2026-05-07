using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections.Generic;

[Action("MyNode/CheckFreeSlot")]

public class CheckFreeSlot : GOAction
{
    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    private List<string> plantPosition = new List<string> { "PlantPos0", "PlantPos1", "PlantPos2" };
    public override TaskStatus OnUpdate()
    {
       
        if (agentPlantManager == null) { Debug.Log("AgentPlanManager è NULL"); }
        foreach (string str in plantPosition)
        {
            PlantStats stats = agentPlantManager.GetPlantStats(str);
            if (stats == null)
            {
                return TaskStatus.COMPLETED;
            }
        }
        return TaskStatus.FAILED;
    }
}

