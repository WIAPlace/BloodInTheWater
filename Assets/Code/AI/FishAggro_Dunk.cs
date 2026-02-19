using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 2/18/26
/// Purpose: The Dunk Enters the Aggro state
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class FishAggro_Dunk : FishAggro_Abstract
{
    //private FishStateController fishState;
    private NavMeshAgent agent;
    private Vector3 dirToTarget;
    private bool rotToTarget=false;

    void Start()
    {
        //fishState = GetComponent<FishStateController>();
        agent = GetComponent<NavMeshAgent>();
        InitiateAggroState();
    }
    void Update()
    {
        if(rotToTarget)
        {
            TurnWithoutMoving();
        }
    }

    public override void InitiateAggroState()
    {
        agent.updateRotation = false;
        agent.isStopped = false; // activates agent
        agent.speed = 0f;
        

        dirToTarget = target.transform.position;
        agent.SetDestination(dirToTarget); // setting this as target for later
        // Calculate direction to the destination
        dirToTarget = (dirToTarget - transform.position).normalized;
        // Project direction onto the XZ plane to ignore Y axis changes
        dirToTarget.y = 0;

        rotToTarget = true;
        //StartCoroutine(WindUp());
    }
    protected override void FullAggro()
    {
        Debug.Log("FullAgro");
        agent.updateRotation = true;
        agent.speed = aggroSpeed;
    }

    private void TurnWithoutMoving()
    {
        // If the direction has magnitude, calculate and apply rotation
        Quaternion lookRotation = Quaternion.LookRotation(dirToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        if(transform.rotation == lookRotation)
        {
            Debug.Log("AgroInitiate");
            rotToTarget = false;
            InitiateWindUp();
        }
    }
    private void InitiateWindUp()
    {
        //indicators will go here.
        StartCoroutine(WindUp());
    }
    public void EndAggro()
    {
        agent.isStopped = true; // turn off agent.
    }
}
