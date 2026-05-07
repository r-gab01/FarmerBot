using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    public AgentStatistics agentStatistics;
    private float waterLevel;

    private void OnTriggerStay(Collider other)
    {
        waterLevel = agentStatistics.getWaterLevel();
        waterLevel += Time.deltaTime * 10f;
        agentStatistics.setWaterLevel(waterLevel);
    }
}
