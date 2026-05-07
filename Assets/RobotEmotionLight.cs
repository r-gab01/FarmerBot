using UnityEngine;

public class RobotEmotionLED : MonoBehaviour
{
    public Renderer ledRenderer;  
    public Color happyColor = Color.green;    // Felice
    public Color warningColor = Color.yellow; // Attenzione
    public Color dangerColor = Color.red;     // Preoccupato

    private Material ledRobot;

    void Start()
    {
        if (ledRenderer != null)
            ledRobot = ledRenderer.material;
    }

    public void SetEmotion(string emotion)
    {
        if (ledRobot == null) return;

        switch (emotion)
        {
            case "happy":
                ledRobot.SetColor("_EmissionColor", happyColor);
                break;
            case "warning":
                ledRobot.SetColor("_EmissionColor", warningColor);
                break;
            case "danger":
                ledRobot.SetColor("_EmissionColor", dangerColor);
                break;
            default:
                break;
        }
    }
}
