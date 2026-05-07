using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;
using System.Collections.Generic;
using System;
using System.Drawing;

[Action("MyNode/NextSlotInMature")]
public class NextSlotInMature : GOAction
{
    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [OutParam("nextFreeMatureSlot")]
    public String nextFreeMatureSlot;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    private List<String> plantMaturePosition = new List<String> { "MaturePlantPos0", "MaturePlantPos1", "MaturePlantPos2", "MaturePlantPos3", "MaturePlantPos4", "MaturePlantPos5", "MaturePlantPos6", "MaturePlantPos7", "MaturePlantPos8", "MaturePlantPos9", "MaturePlantPos10", "MaturePlantPos11", "MaturePlantPos12", "MaturePlantPos13", "MaturePlantPos14", "MaturePlantPos15" };

    public override TaskStatus OnUpdate()
    {
        foreach (string str in plantMaturePosition)
        {
            PlantStats stats = agentPlantManager.GetMatureSlot(str);
            if (stats == null)
            {
                Debug.Log($"Slot libero trovato: {str} ");
                nextFreeMatureSlot = str;

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
