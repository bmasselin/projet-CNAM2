using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public bool ShowDebug = true;
    public float Speed = 3.0f;

    public void FindPathTo(Vector3 dest)
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<NavMeshAgent>().speed = Speed;
        GetComponent<NavMeshAgent>().SetDestination(dest);
    }

    public void Stop()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void OnDrawGizmos()
    {
        if (!ShowDebug)
            return;

        float height = GetComponent<NavMeshAgent>().height;
        if (GetComponent<NavMeshAgent>().hasPath)
        {
            Vector3[] corners = GetComponent<NavMeshAgent>().path.corners;
            if (corners.Length >= 2)
            {
                Gizmos.color = Color.red;
                for (int i = 1; i < corners.Length; i++)
                {
                    Gizmos.DrawLine(corners[i - 1] + Vector3.up * height / 2, corners[i] + Vector3.up * height / 2);
                }
            }
        }
    }
}
