using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;
using UnityEngine.UIElements;

////////////////////////////////////////////////////////////////// In Idle Range
public class PS_StateIdle : Abs_StateIdle
{
    Transform orientation;
    float ChanceToAggro = 0f;
    public override void DoEnter(Fish_Controller FSC)
    {
        
        orientation = FSC.waveHandler.UseableMesh;
        idleActive = true;
        FSC.agent.enabled = true; // turn that mans on
        FSC.agent.isStopped = false;

        FSC.running = FSC.StartCoroutine(Rotate90Degrees(FSC));
        /*
        Quaternion targetRotation = orientation.localRotation * Quaternion.Euler(0, 0, 0);
        if(Quaternion.Angle(orientation.localRotation, targetRotation) < 0.1f)
        {
            FSC.running = FSC.StartCoroutine(WanderRoutine(FSC));
        }
        else
        {
            FSC.StartCoroutine(Rotate90Degrees(FSC));
        }
        */
        ChanceToAggro = FSC.chanceToHook;
    }

    public override void DoExit(Fish_Controller FSC)
    {
        FSC.StopCo(FSC.running);
        idleActive = false;
    }
    protected override IEnumerator WanderRoutine(Fish_Controller FSC)
    {
        while(idleActive)
        {
            Vector3 newPos = FSC.SC.RandomNavMeshLocation(FSC.transform.position, FSC.wanderRadius);
            FSC.agent.SetDestination(newPos);
            //FSC.agent.Warp(newPos);
                
            // Wait until agent is close to destination or timer runs out
            yield return new WaitForSeconds(FSC.wanderTimer);
            float randy = Random.Range(0,101);
            if(randy < ChanceToAggro) 
            {   // if random number is in range go aggro.
                //Debug.Log("Changed To Unique");
                FSC.ChangeState(FSC.SC.Unique);
            }
            else
            {
                ChanceToAggro += FSC.SC.ChaceIncrease;
            }
        }
    }
    IEnumerator Rotate90Degrees(Fish_Controller FSC) {
        // Calculate the exact target rotation relative to current
        Quaternion targetRotation = Quaternion.identity;
        
        float progress = 0;
        while (progress < 1f) {      
            //Debug.Log(orientation.localRotation);
            // Smoothly move towards target based on time
            orientation.localRotation = Quaternion.RotateTowards(orientation.localRotation, targetRotation, FSC.agent.angularSpeed/6 * Time.deltaTime);
            float angle = Quaternion.Angle(orientation.localRotation, targetRotation);
            // Check if we are close enough to stop this is the stopper because it will almost certainly happen first.
            //Debug.Log(angle);
            if (angle < 0.1f) {
                
                //Debug.Log("break");
                orientation.localRotation = targetRotation; // Snap to exact value
                FSC.running = FSC.StartCoroutine(WanderRoutine(FSC));
                break;
            }
            yield return null; // Wait for the next frame
        }
    }
}

////////////////////////////////////////////////////////////////// In Lure Range
public class PS_StateLure : Abs_StateLure
{
    public override void DoEnter(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
        return FSC.previousState;
    }
}

////////////////////////////////////////////////////////////////// In Bobber Range
public class PS_StateBobber : Abs_StateBobber
{
    bool jetting = false;
    public override void DoEnter(Fish_Controller FSC)
    {
        jetting = false;
        FSC.running = FSC.StartCoroutine(KeepTime(FSC));

        //throw new System.NotImplementedException();
    }

    public override void DoExit(Fish_Controller FSC)
    {
        FSC.StopCo(FSC.running);
        //throw new System.NotImplementedException();
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        if (jetting)
        {
            FSC.transform.Translate(Vector3.forward * FSC.agent.speed * 4 * Time.deltaTime);
        }
        return this;
    }
    IEnumerator KeepTime(Fish_Controller FSC)
    {
        yield return new WaitForSeconds(FSC.fleeDistance); // take a quick pause
        jetting = true; // start moving
        FSC.agent.enabled = false;// turn this back off so that the mans can move
        yield return new WaitForSeconds(1f);
        FSC.agent.enabled = true; // turn it back on so that he doesnt leave the stage
        yield return new WaitForSeconds(FSC.fleeTimer);
        FSC.ChangeState(FSC.SC.Idle);
    }
}

////////////////////////////////////////////////////////////////// In Fear Range
public class PS_StateFear : Abs_StateFear
{
    
    public override void DoEnter(Fish_Controller FSC)
    {
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        
        return FSC.SC.Idle;
    }
    
}

////////////////////////////////////////////////////////////////// Unique Behavior
public class PS_StateUnique : Abs_StateUnique
{   
    Transform orientation;
    Vector3 ramTarget;
    public override void DoEnter(Fish_Controller FSC)
    {
        FSC.agent.isStopped = true;
        
        orientation = FSC.waveHandler.UseableMesh;
        ramTarget = FSC.SC.target.transform.position;
        //woundUp = false; 
        orientation.localRotation = Quaternion.Euler(0, 0, 0);
        FSC.running = FSC.StartCoroutine(Rotate90Degrees(FSC));
    }

    public override void DoExit(Fish_Controller FSC)
    {
        FSC.StopCo(FSC.running);
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return this;
    } 
    IEnumerator Rotate90Degrees(Fish_Controller FSC) {
        // Calculate the exact target rotation relative to current
        Quaternion targetRotation = orientation.localRotation * Quaternion.Euler(90, 0, 0);
        
        float progress = 0;
        while (progress < 1f) {
            ramTarget = FSC.SC.target.transform.position;
            // 1. Find direction to target
            Vector3 direction = (ramTarget - FSC.transform.position).normalized;

            // 2. Create the rotation the object should eventually have
            Quaternion targetRot = Quaternion.LookRotation(direction);
            
            // 3. Rotate towards that target over time (Slerp for easing)
            FSC.transform.rotation = Quaternion.RotateTowards(FSC.transform.rotation, targetRot, FSC.agent.angularSpeed/3 * Time.deltaTime);
            
            // Smoothly move towards target based on time
            orientation.localRotation = Quaternion.RotateTowards(orientation.localRotation, targetRotation, FSC.agent.angularSpeed/6 * Time.deltaTime);

            float angleBase = Quaternion.Angle(orientation.localRotation, targetRotation);
            
            float dot = Vector3.Dot(FSC.transform.forward, direction);   
            //Debug.Log(dot);
            // Check if we are close enough to stop this is the stopper because it will almost certainly happen first.
            if (angleBase < 0.1f && dot >0.99f) {
                //Debug.Log("break");
                orientation.localRotation = targetRotation; // Snap to exact value
                //woundUp = true;
                FSC.ChangeState(FSC.SC.Bobber);
                break;
            }
            yield return null; // Wait for the next frame
        }
    } 
}

////////////////////////////////////////////////////////////////// Enter
public class PS_StateEnter : Abs_StateEnter
{
    public override void DoEnter(Fish_Controller FSC)
    {
       //GameManager.Instance.unlocks.SaveMonsterData(1);
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return FSC.SC.Idle;
    }
}


public class PS_StateHooked : Abs_StateHooked
{
    public override void DoEnter(Fish_Controller FSC)
    {
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }
    public override IFishState DoState(Fish_Controller FSC)
    {
        return FSC.previousState;
    }
}
public class PS_StateOnLine : Abs_StateOnLine
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
