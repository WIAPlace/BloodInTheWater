using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scuba_StateMove : IBoatStomperState
{
    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        SC.agent.isStopped = false; // turns the man into an able bodied individual
        SC.agent.SetDestination(SC.GetTarget().transform.position);
        return SC.MoveState;
    }
}
