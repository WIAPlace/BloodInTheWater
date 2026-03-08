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
    
}

////////////////////////////////////////////////////////////////// In Lure Range
public class Basic_StateLure : Abs_StateLure
{
    // base
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
    //base
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