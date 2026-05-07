using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

[Condition("MyConditions/IsWaterEmpty")]
public class IsWaterEmpty : ConditionBase
{
    [InParam("agentStatistics")]
    public AgentStatistics agentStatistics; // Riferimento al componente AgentStatistics

    public float waterThreshold = 49.5f; // Soglia minima per il livello della acqua
    public override bool Check()
    {
        if (agentStatistics.getWaterLevel() <= waterThreshold)
        {
            return true; //è pieno
        }
        else
        {
            return false; //è vuoto
        }
    }
}
