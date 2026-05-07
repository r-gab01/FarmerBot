using System;
using UnityEngine;

public class AgentStatistics : MonoBehaviour
{
    private float batteryLevel = 100.0f;
    private float waterLevel = 45.0f;
    private bool isMoving = false;

    public void Update()
    {
        dischargeBattery();
    }

    private void dischargeBattery()
    {
        float batteryLevel = getBatteryLevel();
        if (getIsMoving() == false)
        {
            batteryLevel -= Time.deltaTime * 0.05f;
        }
        else
        {
            batteryLevel -= Time.deltaTime * 0.25f;
        }
        setBatteryLevel(batteryLevel);

    }

    public float getBatteryLevel()
    {
        return batteryLevel;
    }
    public float getWaterLevel()
    {
        return waterLevel;
    }

    public bool getIsMoving()
    {
        return isMoving;
    }
    public void setBatteryLevel(float level)
    {
        if (level <= 100f && level >= 0) 
        {
            batteryLevel = level;
        }
        else if(level > 100.0f)
        {
            batteryLevel = 100f;
        }
    }

    public void setWaterLevel(float level)
    {
        if (level <= 100f && level >= 0)
        {
            waterLevel = level;
        }
        else if (level > 100.0f)
        {
            waterLevel = 100f;
        }
    }

    public void setIsMoving(bool value)
    {
        isMoving = value;
    }

    public void changeIsMoving()
    {
        if (isMoving)
        {
            Debug.Log("Agente fermo");
            isMoving = false;
        }
        else
        {
            Debug.Log("In movimento");
            isMoving = true;
        }
    }
}
