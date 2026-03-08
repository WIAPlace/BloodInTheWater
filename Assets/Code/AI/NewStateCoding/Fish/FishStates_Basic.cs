using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 3/8/26
/// Purpose: Holder for all the fish Basic states
///     Usualy implies what range they are in
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
////////////////////////////////////////////////////////////////// In Idle Range
public class Basic_StateIdle : Abs_StateIdle
{
    private bool idleActive = false;
    private Coroutine idleRun;
    public override void DoEnter(Fish_Controller FSC)
    {   
        FSC.agent.isStopped = false; // reactivates the agent if its stoped.
        idleActive = true;
        idleRun = FSC.StartCoroutine(WanderRoutine(FSC));
    }

    public override void DoExit(Fish_Controller FSC)
    {
        idleActive = false;
        FSC.agent.isStopped = true; // deactivates the agent if its stoped.
        FSC.StopCo(idleRun);
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return this;
    }
    private IEnumerator WanderRoutine(Fish_Controller FSC)
    {
        while(idleActive)
        {
            Vector3 newPos = RandomNavMeshLocation(FSC.transform.position, FSC.wanderRadius);
            FSC.agent.SetDestination(newPos);
                
            // Wait until agent is close to destination or timer runs out
            yield return new WaitForSeconds(FSC.wanderTimer);
        }
    }

    // Finds a valid NavMesh point near the center point
    private Vector3 RandomNavMeshLocation(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        NavMeshHit hit;
        
        // Sample position to make sure it is on the NavMesh
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        return hit.position;
    }
}

////////////////////////////////////////////////////////////////// In Lure Range
public class Basic_StateLure : Abs_StateLure
{
    public override void DoEnter(Fish_Controller FSC)
    {
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return this;
    }
}

////////////////////////////////////////////////////////////////// In Bobber Range
public class Basic_StateBobber : Abs_StateBobber
{
    public override void DoEnter(Fish_Controller FSC)
    {
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return this;
    }
}

////////////////////////////////////////////////////////////////// In Fear Range
public class Basic_StateFear : Abs_StateFear
{
    public override void DoEnter(Fish_Controller FSC)
    {
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return this;
    }
}

////////////////////////////////////////////////////////////////// Unique Behavior
public class Basic_StateUnique : Abs_StateUnique
{
    public override void DoEnter(Fish_Controller FSC)
    {
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return this;
    }
}

////////////////////////////////////////////////////////////////// Enter
public class Basic_StateEnter : Abs_StateEnter
{
    public override void DoEnter(Fish_Controller FSC)
    {
        base.DoEnter(FSC);
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return this;
    }
}