using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("MyNode/IsSick")]
public class IsSick : GOAction
{
    [InParam("plantStats")]
    public PlantStats plantStats;

    [OutParam("sickBool")]
    public bool sickBool;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    public float sensorAccuracy = 0.95f;      // Probabilità likelihood di rilevare correttamente una pianta malata (P(E | H)) H: ipotesi (stato di malattia della pianta); E: evidenza (rilevazione di malattia del sensore)
    public float falsePositiveRate = 0.03f;  // Probabilità di rilevare erroneamente malattia su una pianta sana (P(E | ¬H))

    public override TaskStatus OnUpdate()
    {
        sickBool = DetectPlantHealth(plantStats.getIsSick());
        if (sickBool)
        {
            if (agentAudioManager != null)
            {
                agentAudioManager.PlaySickSound();
            }
            return TaskStatus.COMPLETED; //la piantina è malata  --> eseguire Task
        }
        else
        {
            return TaskStatus.FAILED; //la piantina non è malata (stato di default)
        }
    }

    private bool DetectPlantHealth(bool health)
    {
        if (health)
        {
            if (Random.value <= sensorAccuracy)
            {
                return true;    //pianta rilevata malata: TP
            }
            else
            {
                return false;   //pianta non rilevata malata: FN
            }
        }
        else
        {
            if (Random.value <= falsePositiveRate)
            {
                return true;    // pianta rilevata malata ma non lo è: FP
            }
            else
            {
                return false;   // pianta non rilevata malata  correttamente: TN
            }

        }
    }

}







