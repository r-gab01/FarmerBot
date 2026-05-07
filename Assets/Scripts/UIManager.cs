using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public AgentStatistics statistics;
    [SerializeField] private TextMeshProUGUI batteryTMPText;
    [SerializeField] private TextMeshProUGUI waterTMPText;

    [SerializeField] private Image batteryIcon;
    [SerializeField] private Image waterIcon;

    [SerializeField] private Sprite BatteryLogoEmpty;
    [SerializeField] private Sprite BatteryLogoMedium;
    [SerializeField] private Sprite BatteryLogoFull;

    private float currentBattery;
    private float currentWater;

    void Start()
    {

    }

    void Update()
    {
        currentBattery = statistics.getBatteryLevel();
        currentWater = statistics.getWaterLevel();

        if (batteryTMPText != null)
        {
            batteryTMPText.text = $"{currentBattery:F1}%";
        }

        if (waterTMPText != null)
        {
            waterTMPText.text = $"{currentWater:F1}%";
        }

        UpdateIcons();
    }

    private void UpdateIcons()
    {
        if (batteryIcon != null)
        {
            if (currentBattery <= 30.0f)
            {
                batteryIcon.sprite = BatteryLogoEmpty;
            }
            else if (currentBattery >30.0f  && currentBattery<=70.0f)
            {
                batteryIcon.sprite = BatteryLogoMedium;
            }
            else
            {
                batteryIcon.sprite = BatteryLogoFull;
            }
        }


    }
}
