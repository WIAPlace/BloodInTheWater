using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;
using UnityEngine.AI;
using UnityEngine.Splines;
//using System.Numerics;
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
        FSC.waveHandler.SetOnWaves(true); 
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
        if (FSC.SC.target != null)
        {
            // vector between two points
            Vector3 heading = FSC.SC.target.transform.position - FSC.transform.position;
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
    float sqrDist; // used for checking how far away the bobber is.
    private float sqrTapRange;
    float timer = 0f;
    Vector3 origin;
    //Coroutine moving = null;
    Vector3 velocity = Vector3.zero;
    float hookChance = 0f;
    private bool inRange = false;

    public override void DoEnter(Fish_Controller FSC)
    {
        hookChance = FSC.chanceToHook; // sets base chance to grab bobber.
        FSC.onHook = false; // making sure this is false at the start
        origin = FSC.transform.position;
        sqrTapRange = FSC.tapEnterRange * FSC.tapEnterRange;
        inRange = true;
        timer = 0f;
        FSC.agent.isStopped = true;
        //FSC.transform.LookAt(FSC.targetPos);
        FSC.running = FSC.StartCoroutine(Tapping(FSC)); // start the tapp timmer;
    }

    public override void DoExit(Fish_Controller FSC)
    {
        hookChance = 0f;
        FSC.agent.isStopped = true; // stop agent from continuing to move.
        FSC.StopCo(FSC.running); // stop this from continuing
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    { 
        if(!FSC.onHook)
        {
            RotToTarget(FSC);
            TapPonging(FSC); // the tappin thing. 
            
            Vector3 heading = FSC.SC.target.transform.position - origin;
            //sq mgnitude
            sqrDist = heading.sqrMagnitude;

            if(sqrDist > sqrTapRange * FSC.tapExitRange)
            {   // enter bobber
                return FSC.SC.Lure;
            }
        }
        return this;
    }

    IEnumerator Tapping(Fish_Controller FSC)
    { // will every so seconds decide if its hooked or not.
        while(FSC.currentState == FSC.SC.Bobber)
        {
            float randyHit = Random.Range(0,101);
            //Debug.Log(randyHit);
            if (randyHit <= hookChance) 
            { // when in hook chance range start moving twoards the bobber.
            // then on collision it will be in the positioon thats ready to be hooked/
                //Debug.Log("Hooked Fish");
                //FSC.ChangeState(FSC.SC.Hook);
                FSC.onHook = true; // stop the update and allow for collision with bobber to occur.
                FSC.agent.isStopped = false; // let him move
                FSC.agent.SetDestination(FSC.SC.target.transform.position); // move twoards lure;
                FSC.StopCo(FSC.running); // stop running this corutine
            }
            hookChance += 5;
            yield return new WaitForSeconds(3f);
            
        }
    }
    private void TapPonging(Fish_Controller FSC)
    {
        if(sqrDist > sqrTapRange || !inRange)
        { // this allows for the fish to still be attrackted while the bobber is moving. the range is determined by the tap exit range
            if(inRange) inRange = false; // change this to out of range so it will at least run once after its in range
            
            // move forward
            FSC.transform.Translate(Vector3.forward * FSC.agent.speed * Time.deltaTime);
            origin = FSC.transform.position; // set origin point to current position for calculationg the new sqr distance.

            if (sqrDist <= sqrTapRange)
            {   // basicly do a mini reset for the bobber state.
                inRange = true;
                timer = 0;
            }
        }
        else // doo the bobbing back and forth.
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
        FSC.onHook = false; // in case i forget to do this in an exit.
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

////////////////////////////////////////////////////////////////// Hooked
public class Basic_StateHooked : Abs_StateHooked
{
    public override void DoEnter(Fish_Controller FSC)
    {
        FSC.running = FSC.StartCoroutine(AboutToCatch(FSC)); 
        GameManager.Instance.Hooked(true);
    }

    public override void DoExit(Fish_Controller FSC)
    {
       FSC.StopCo(FSC.running);
       GameManager.Instance.Hooked(false); // maybe, might need to check what state is being thrown
    }
    IEnumerator AboutToCatch(Fish_Controller FSC)
    {
        
        yield return new WaitForSeconds(FSC.catchTimeWindow);
        //Debug.Log("Hit");
        FSC.ChangeState(FSC.SC.Fear);
    }
}
////////////////////////////////////////////////////////////////// On Line
public class Basic_StateOnLine : Abs_StateOnLine
{
    public override void DoEnter(Fish_Controller FSC)
    {
        FSC.currentLocalOnReel = 0f;
        FSC.fishData.SendData();
    }

    public override void DoExit(Fish_Controller FSC)
    {
       //Debug.Log("OutOfLine");
    }
    public override IFishState DoState(Fish_Controller FSC)
    {
        /*
        if (FSC.currentLocalOnReel < FSC.distOnReel)
        {
            float moveDist = 5f * Time.deltaTime;
            FSC.currentLocalOnReel += moveDist;

            if (FSC.currentLocalOnReel >= FSC.reelLength)
            {
                FSC.currentLocalOnReel = FSC.distOnReel;
                if(FSC.currentLocalOnReel >= FSC.reelLength){
                    FSC.gameObject.SetActive(false);
                }
            }
            UpdatePosition(FSC);
        }
        */
        float dist = FSC.distOnReel;
        if (dist < 1)
        {        
            Vector3 position = FSC.reelSpline.EvaluatePosition(0,dist);
            FSC.transform.position = position;
            // Get the tangent (forward direction) for rotation
            Vector3 forward = FSC.reelSpline.EvaluateTangent(0,dist);
            // Calculate the up vector (adjust as needed for 2D or specific orientations)
            Vector3 up = FSC.reelSpline.EvaluateUpVector(0,dist);

            // Set the rotation to align with the spline's direction and up vector
            FSC.transform.rotation = Quaternion.LookRotation(forward, up);
        }
        return this;
    }
    private void UpdatePosition(Fish_Controller FSC)
    {
        float normalizedTime = FSC.currentLocalOnReel / FSC.reelLength; 

        Vector3 position = FSC.reelSpline.EvaluatePosition(normalizedTime);
        FSC.transform.position = position;
        //Debug.Log(normalizedTime);

        // Get the tangent (forward direction) for rotation
        Vector3 forward = FSC.reelSpline.EvaluateTangent(normalizedTime);
        // Calculate the up vector (adjust as needed for 2D or specific orientations)
        Vector3 up = FSC.reelSpline.EvaluateUpVector(normalizedTime);

        // Set the rotation to align with the spline's direction and up vector
        FSC.transform.rotation = Quaternion.LookRotation(forward, up);
    }
}