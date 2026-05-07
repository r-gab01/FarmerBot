using System;
using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using Pada1.BBCore.Conditions;

[Condition("MyConditions/IsNotCharged")]
public class IsNotCharged : ConditionBase
{
    [InParam("agentStatistics")]
    public AgentStatistics agentStatistics; // Riferimento al componente AgentStatistics

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    public float batteryThreshold = 30f; // Soglia minima per il livello della batteria
    public override bool Check()
    {
        if (agentStatistics.getBatteryLevel() <= batteryThreshold)
        {

            // Riproduciamo il suono tramite l'AgentAudioManager
            if (agentAudioManager != null)
            {
                agentAudioManager.LowBatterySound();
            }

            return true;    // è scarico
        }
        else
        {
            return false;   // è carico
        }
    }
}

