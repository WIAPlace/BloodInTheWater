using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishAggro_Dunk : FishAggro_Abstract
{
    private FishStateController fishState;
    private NavMeshAgent agent;
    private bool rotToTarget=false;
    private bool fullAggroOn = false;

    void Start()
    {
        fishState = GetComponent<FishStateController>();
        agent = GetComponent<NavMeshAgent>();
        InitiateAggroState();
    }

    public override void InitiateAggroState()
    {
        rotToTarget = true;
        agent.speed = 1f;
        agent.SetDestination(target.transform.position);
        StartCoroutine(WindUp());
    }
    protected override void FullAggro()
    {
        Debug.Log("FullAgro");
        agent.speed = aggroSpeed;
    }
}
