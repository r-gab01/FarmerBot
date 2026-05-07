using UnityEngine;

public class MapInitializer : MonoBehaviour
    //classe per inizializzare la mappa con tutte le informazioni note all'agente sull'ambiente iniziale
{
    public AgentMap agentMap;

    private void Start()
    {
        // Usa FindObjectsByType per trovare tutti i MapPoint nella scena
        MapPoint[] mapPoints = FindObjectsByType<MapPoint>(FindObjectsSortMode.None);

        foreach (var point in mapPoints)
        {
            agentMap.AddPoint(point);
        }

        Debug.Log("Mappa interna popolata.");
    }
}
