using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public string pointName; // Nome del punto sulla mappa
    public Vector3 Position => transform.position; // Posizione del punto
    public Transform direction => transform;
}
