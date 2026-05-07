using UnityEngine;
using BBUnity.Actions;
using BBUnity;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System;

[Action("MyNode/NextPosition")]
public class NextPosition : GOAction
{
    [InParam("agentPlantManager")]
    public AgentPlantManager agentPlantManager;

    [OutParam("nextSearchPos")]
    public string nextSearchPos;


    public override TaskStatus OnUpdate()
    {
        nextSearchPos = agentPlantManager.SearchQueueDequeue();
        return TaskStatus.COMPLETED;

    }
}
