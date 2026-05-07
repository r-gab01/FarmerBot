using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using BBUnity;

[Action("MyNode/NeedWater")]
public class NeedWater : GOAction
{
    [InParam("plantStats")]
    public PlantStats plantStats;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    public override TaskStatus OnUpdate()
    {
        if (plantStats.getSoilMoisture() <= plantStats.getRequiredMoisture())
        {
            // Riproduciamo il suono tramite l'AgentAudioManager
            if (agentAudioManager != null)
            {
                agentAudioManager.PlayWateringSound();
            }
            return TaskStatus.COMPLETED;
        }
        else
        {
            return TaskStatus.FAILED;
        }
    }

 
}
