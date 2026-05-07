using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;
using System.Collections.Generic;
using System;
using System.Drawing;

[Action("MyNode/NextSlotInSick")]
public class NextSlotInSick : GOAction
{
    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [OutParam("nextFreeSickSlot")]
    public String nextFreeSickSlot;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    private List<String> plantSickPosition = new List<String> { "TablePlantPos0", "TablePlantPos1", "TablePlantPos2", "TablePlantPos3", "TablePlantPos4", "TablePlantPos5", "TablePlantPos6", "TablePlantPos7" };

    public override TaskStatus OnUpdate()
    {
        foreach (string str in plantSickPosition)
        {
            PlantStats stats = agentPlantManager.GetSickSlot(str);
            if (stats == null)
            {
                Debug.Log($"Slot libero trovato: {str} ");
                nextFreeSickSlot = str;

                // Riproduciamo il suono tramite l'AgentAudioManager
                if (agentAudioManager != null)
                {
                    agentAudioManager.PlaceToSlotSound();
                }

                return TaskStatus.COMPLETED;
            }
        }
        Debug.LogError($" Non ci sono posti liberi nel casotto.");
        return TaskStatus.FAILED;

    }
}
