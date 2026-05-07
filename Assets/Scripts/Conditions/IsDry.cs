using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

[Action("MyNode/IsDry")]
public class IsDry : GOAction
{
    [InParam("plantStats")]
    public PlantStats plantStats;

    [OutParam("dryBool")]
    public bool dryBool;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;
    public override TaskStatus OnUpdate()
    {
        if (plantStats.getIsDry()) {
            dryBool = true;

            // Riproduciamo il suono tramite l'AgentAudioManager
            if (agentAudioManager != null)
            {
                agentAudioManager.PlayNotDrySound();
            }

            return TaskStatus.COMPLETED; //la piantina è secca --> eseguire Task
        }
        else
        {

            dryBool = false; 
            return TaskStatus.FAILED; //la piantina non è secca (stato di default)
        }
    }

 
}
