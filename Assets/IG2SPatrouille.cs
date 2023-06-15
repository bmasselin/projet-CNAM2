/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IG2SPatrouille : FSMState<IG2StateInfo>
{
    bool hasPatrolDestination = false;
    private Vector3 randomPatrolPosition;
    private float patrolDistance = 10;
    private IG2EnemyController controller;
    private float DistPointReached = 1;
    private float MaxTimeNotMoving = 3;
    private float TimeNotMoving = 0;
    private float SpeedLimiteNotMoving = 0.2f;
    private float PatrolSpeed = 1.0f;
    private Vector3 lastPosition = Vector3.zero;


    public override void doState(ref IG2StateInfo infos)
    {
        controller = infos.Controller;

        if ((lastPosition - controller.transform.position).magnitude / Time.deltaTime < SpeedLimiteNotMoving)
            TimeNotMoving += Time.deltaTime;
        else
            TimeNotMoving = 0;

        this.controller = infos.Controller;
        if (!hasPatrolDestination || TimeNotMoving > MaxTimeNotMoving)
        {
            if (createValidPatrolPoint(ref randomPatrolPosition))
            {
                hasPatrolDestination = true;
                controller.Speed = PatrolSpeed;
                controller.FindPathTo(randomPatrolPosition);
            }
        }
        else
        {
            if ((controller.transform.position - randomPatrolPosition).sqrMagnitude < DistPointReached * DistPointReached)
            {
                hasPatrolDestination = false;
            }
        }

        KeepMeAlive = true;
        lastPosition = controller.transform.position;
    }

    public bool createValidPatrolPoint(ref Vector3 dest)
    {
        for (int i = 0; i < 10; i++)
        {

            if (sampleRandomDestination(controller.transform.position, patrolDistance, ref dest))
            {
                if (canBeReached(dest))
                {
                    log("Point added");
                    return true;
                }
            }
        }

        return false;
    }

    public bool sampleRandomDestination(Vector3 curentPosition, float distance, ref Vector3 destination)
    {
        Vector3 offset = Random.onUnitSphere;
        offset.y = 0;
        offset = offset.normalized * distance;
        offset.y = 1000;
        RaycastHit hitInfo;
        if (Physics.Raycast(curentPosition + offset, Vector3.down, out hitInfo))
        {
            destination = hitInfo.point;
            return true;
        }

        return false;
    }

    public bool canBeReached(Vector3 destination)
    {
        NavMeshAgent agent = controller.transform.GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);
        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            return true;
        }
        return false;
    }
}



*/