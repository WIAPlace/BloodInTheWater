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
    private float sqrTapRange;
    public override void DoEnter(Fish_Controller FSC)
    {
        base.DoEnter(FSC);
        sqrTapRange = FSC.tapEnterRange * FSC.tapEnterRange;
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        if (FSC.targetPos != null)
        {
            // vector between two points
            Vector3 heading = FSC.targetPos - FSC.transform.position;
            //sq mgnitude
            float sqrDist = heading.sqrMagnitude;
            
            if(sqrDist < sqrTapRange)
            {   // enter bobber
                return FSC.SC.Bobber;
            }
        }
        return this;
    }
}

////////////////////////////////////////////////////////////////// In Bobber Range
public class Basic_StateBobber : Abs_StateBobber
{
    float timer = 0f;
    Vector3 origin;
    //Coroutine moving = null;
    Vector3 velocity = Vector3.zero;
    public override void DoEnter(Fish_Controller FSC)
    {
        origin = FSC.transform.position;
        timer = 0f;
        FSC.agent.isStopped = true;
        //FSC.transform.LookAt(FSC.targetPos);
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    { 
        RotToTarget(FSC);
        TapPonging(FSC);
        return this;
    }
    IEnumerator Tapping(Fish_Controller FSC)
    {
        while(FSC.currentState == FSC.SC.Bobber)
        {
            
            yield return new WaitForSeconds(5f);
            
        }
    }
    private void TapPonging(Fish_Controller FSC)
    {
        // 1. Calculate raw target movement
        timer += Time.deltaTime;

        float numInRange = Mathf.PingPong(timer * FSC.tapSpeed, FSC.tapVary);
        
        // Original calculation - consider reviewing if this is meant to be a simple offset
        float tapValue = -FSC.tapVary / (1 + numInRange); 

        Vector3 forward3D = FSC.transform.forward;
        Vector3 forward2D = new Vector3(forward3D.x, 0f, forward3D.z).normalized;

        // 2. Define the target position
        Vector3 targetPosition = origin + forward2D * tapValue;

        // 3. Smoothly move towards the target
        FSC.transform.position = Vector3.SmoothDamp(
            FSC.transform.position, 
            targetPosition, 
            ref velocity, 
            FSC.tapSmooth
        );
    }
    private void RotToTarget(Fish_Controller FSC)
    {
        Vector3 dir = FSC.SC.target.transform.position - FSC.transform.position; // get the vector between the 2 objects
        Quaternion targetRotation = Quaternion.LookRotation(dir); // creat the direction to look
        //if(FSC.transform.rotation != )
        FSC.transform.rotation = Quaternion.RotateTowards( // rotate twoards target
                FSC.transform.rotation, 
                targetRotation, 
                FSC.tapSmoothRot * Time.deltaTime
            );
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