using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System;

[Action("MyNode/BatteryCharge")]
public class BatteryCharge : GOAction
{
    [InParam("agentStatistics")]
    public AgentStatistics agentStatistics;

    private float currentBattery;

    public override TaskStatus OnUpdate()
    {
        currentBattery = agentStatistics.getBatteryLevel();
        if (currentBattery < 99.9f)
        {
            return TaskStatus.RUNNING;
        }
        else 
        {
            Debug.Log($"Batteria ricaricata al {currentBattery}");
            return TaskStatus.COMPLETED;
        }
    }
}
