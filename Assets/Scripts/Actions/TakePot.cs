using BBUnity.Actions;
using Pada1.BBCore;
using System.Collections.Generic;
using System;
using UnityEngine;
using Pada1.BBCore.Tasks;


[Action("MyNode/TakePot")]
public class TakePot : GOAction
{
    
    [InParam("robotHandPosition")]
    [Help("Posizione della mano del robot dove il vaso dovrà essere istanziato.")]
    public Transform robotHandPosition;

    [OutParam("pots")]
    [Help("Riferimento al GameObject del vaso da prendere.")]
    public GameObject pots;

    [InParam("potPrefab")]
    [Help("Prefab del vaso da istanziare.")]
    public GameObject potPrefab;

    public override void OnStart()
    {
        base.OnStart();

       
        if (potPrefab != null)
        {
            
            pots = GameObject.Instantiate(potPrefab, robotHandPosition.position, robotHandPosition.rotation);

            pots.transform.SetParent(robotHandPosition);
            pots.transform.localPosition = new Vector3(0, -0.5f, 0);

            Debug.Log($"Ho preso un vaso");
        }
        else
        {
            Debug.LogError("Impossibile completare l'azione: potPrefab o robotHandPosition non assegnati.");
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (pots != null)
        {
            Debug.Log("Vaso raccolto con successo.");
            return TaskStatus.COMPLETED;
        }

        Debug.LogError("Il vaso non è stato istanziato correttamente.");
        return TaskStatus.FAILED;
    }
}
