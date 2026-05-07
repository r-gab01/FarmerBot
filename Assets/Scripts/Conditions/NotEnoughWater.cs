using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

[Action("MyNode/NotEnoughWater")]

public class NotEnoughWater : GOAction
{
    [InParam("agentStatistics")]
    public AgentStatistics agentStatistics; // Riferimento al componente AgentStatistics

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    public float waterThreshold = 20f; // Soglia minima per il livello della batteria
    public override TaskStatus OnUpdate()
    {
        if (agentStatistics.getWaterLevel() <= waterThreshold)
        {
            // Riproduciamo il suono tramite l'AgentAudioManager
            if (agentAudioManager != null)
            {
                agentAudioManager.LowWaterSound();
            }

            return TaskStatus.COMPLETED;
        }
        else
        {
            return TaskStatus.FAILED;
        }
    }
}

