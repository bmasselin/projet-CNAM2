using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerClicController : ClicAgentController
{
    private float baseSpeed = 3.0f;

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (Input.GetAxis("Vertical") > 0.5f && GetComponent<NavMeshAgent>().velocity.sqrMagnitude > 0.0f)
        {
            Speed = baseSpeed * 1.5f;
            GetComponent<NoiseStatus>().NoiseLevel = 1;
        }
        else
        {
            Speed = baseSpeed;
            GetComponent<NoiseStatus>().NoiseLevel = 0;
        }
    }
}

