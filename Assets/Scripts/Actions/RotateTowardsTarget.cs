using UnityEngine;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

[Action("MyNode/RotateTowardsTarget")]
public class RotateTowardsTarget : GOAction
{
    [InParam("direction")]
    public Transform direction;

    [InParam("agentDirection")]
    public Transform agentDirection;

    public float rotationSpeed = 1.5f;

    public override void OnStart()
    {
        if (direction == null)
        {
            Debug.LogError("Direzione non specificata!");
        }
    }

    public override TaskStatus OnUpdate()
    {

        agentDirection.rotation = Quaternion.Slerp(agentDirection.rotation, direction.rotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(agentDirection.rotation, direction.rotation) == 0.0f)
        {
            return TaskStatus.COMPLETED;
        }

        return TaskStatus.RUNNING;
    }
}
