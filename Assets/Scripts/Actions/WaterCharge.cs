using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System;

[Action("MyNode/WaterCharge")]
public class WaterCharge : GOAction
{
    [InParam("agentStatistics")]
    public AgentStatistics agentStatistics;

    private float currentWater;

    public override TaskStatus OnUpdate()
    {
        currentWater = agentStatistics.getWaterLevel();
        if (currentWater < 99.9f)
        {
            return TaskStatus.RUNNING;
        }
        else 
        {
            Debug.Log($"Acqua ricaricata al {currentWater}%");
            return TaskStatus.COMPLETED;
        }
    }
}
