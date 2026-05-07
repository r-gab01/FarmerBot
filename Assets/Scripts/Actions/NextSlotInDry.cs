using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;
using System.Collections.Generic;
using System;
using System.Drawing;

[Action("MyNode/NextSlotInDry")]
public class NextSlotInDry : GOAction
{
    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [OutParam("nextFreeDrySlot")]
    public String nextFreeDrySlot;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    private List<String> plantDryPosition = new List<String> { "DryPlantPos0", "DryPlantPos1", "DryPlantPos2", "DryPlantPos3" };

    public override TaskStatus OnUpdate()
    {
        foreach (string str in plantDryPosition)
        {
            PlantStats stats = agentPlantManager.GetDrySlot(str);
            if (stats == null)
            {
                Debug.Log($"Slot libero trovato: {str} ");
                nextFreeDrySlot = str;

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
