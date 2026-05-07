using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("MyNode/IsMature")]
public class IsMature : GOAction
{
    [InParam("plantStats")]
    public PlantStats plantStats;

    [OutParam("matureBool")]
    public bool matureBool;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    public override TaskStatus OnUpdate()
    {
        if (plantStats.getIsMatured())
        {
            matureBool = true;

            // Riproduciamo il suono tramite l'AgentAudioManager
            if (agentAudioManager != null)
            {
                agentAudioManager.PlayMatureSound();
            }

            return TaskStatus.COMPLETED; //la piantina è matura --> eseguire Task
        }
        else
        {
            matureBool = false; 
            return TaskStatus.FAILED; //la piantina non è matura (stato di default)
        }
    }

}
