using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 2/24/26
/// Purpose: Scuba man moves twoards target
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Scuba_StateMove : IBoatStomperState
{
    public void DoExit(Scuba_Controller SC)
    {
        
    }

    public void DoEnter(Scuba_Controller SC)
    {
        if (SC.agent.isActiveAndEnabled && SC.agent.isStopped)
        {   // this doesnt need to be going off every frame.
            SC.agent.isStopped = false; // turns the man into an able bodied individual
        }
        SC.SetAnimation(0);
    }

    public IBoatStomperState DoState(Scuba_Controller SC)
    { 
        if(SC.agent.isActiveAndEnabled)SC.agent.SetDestination(SC.GetTarget().transform.position);
        return SC.MoveState;
    }
}
