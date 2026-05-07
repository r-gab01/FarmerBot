using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections.Generic;
using System;

[Action("MyNode/NextPlant")]
public class NextPlant : GOAction
{
    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [OutParam("nextPlant")]
    public string nextPlantID;

    [OutParam("nextPlantPosition")]
    public string nextPlantPosition;

    public override TaskStatus OnUpdate()
    {
        if (agentPlantManager == null) {
            Debug.Log("AgentPlanManager è NULL"); 
        }

        nextPlantPosition = agentPlantManager.GetNextPlantToWater();
        if (nextPlantPosition == null) {
            Debug.LogWarning("NEXTPLANT POSITION E' NULL");
            return TaskStatus.FAILED;
        }


            nextPlantID = agentPlantManager.GetPlantID(nextPlantPosition);
        return TaskStatus.COMPLETED;

    }
}
