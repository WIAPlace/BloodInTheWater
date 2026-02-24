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
    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        SC.agent.isStopped = false; // turns the man into an able bodied individual
        SC.agent.SetDestination(SC.GetTarget().transform.position);
        return SC.MoveState;
    }
}
