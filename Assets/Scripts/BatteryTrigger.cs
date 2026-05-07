using UnityEngine;

public class BatteryTrigger : MonoBehaviour
{
    public AgentStatistics agentStatistics;
    private float batteryLevel;

    private void OnTriggerStay(Collider other)
    {
        batteryLevel = agentStatistics.getBatteryLevel();
        batteryLevel += Time.deltaTime * 10f;
        agentStatistics.setBatteryLevel(batteryLevel);
    }
}
