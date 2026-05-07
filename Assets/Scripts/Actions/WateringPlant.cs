using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using TMPro;
using UnityEngine;

[Action("MyNode/WateringPlant")]
public class WateringPlant : GOAction
{

    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [InParam("agentStatistics")]
    public AgentStatistics agentStatistics;

    [InParam("plantStats")]
    public PlantStats plantStats;

    private float soilMoistureLevel; //umidità della pianta
    private float waterLevel; //acqua dell'agente

    public override TaskStatus OnUpdate()
    {
        waterLevel = agentStatistics.getWaterLevel();
        soilMoistureLevel = plantStats.getSoilMoisture();
        if (soilMoistureLevel >= plantStats.getRequiredMoisture())
        {
            Debug.Log("Innaffiamento completato");
            agentPlantManager.SetMonitored(true);
            return TaskStatus.COMPLETED;
        }
        else
        {
            soilMoistureLevel += Time.deltaTime * 12f;
            plantStats.setSoilMoisture(soilMoistureLevel);
            waterLevel -= Time.deltaTime * 1.8f;
            agentStatistics.setWaterLevel(waterLevel);
            
            return TaskStatus.RUNNING;
        }
        
    }
}
