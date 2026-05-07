using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AgentMap : MonoBehaviour
    // classe per la gestione della rappresentazione interna della mappa nell'agente
    // contiene metodi per aggiungere una nuova posizione nella rappresentazione o per trovare un punto noto
{
    private Dictionary<string, MapPoint> internalMap = new Dictionary<string, MapPoint>();

    public void AddPoint(MapPoint point)
    {
        if (!internalMap.ContainsKey(point.pointName))
        {
            internalMap.Add(point.pointName, point);
            //Debug.Log($"Punto aggiunto manualmente: {point.pointName} - Posizione: {point.Position} - Direzione: {point.direction.forward}");
        }
        else
        {
            Debug.LogWarning($"Il punto {point.pointName} esiste già nella mappa.");
        }
    }

    public Vector3? GetPosition(string name)
    {
        if (internalMap.TryGetValue(name, out var point))
        {
            return point.Position;
        }
        return null;
    }

    public Transform GetDirection(string name)
    {
        if (internalMap.TryGetValue(name, out var point))
        {
            return point.direction;
        }
        return null;
    }
}
