using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;

////////////////////////////////////////////////////////////////// In Idle Range
public class PS_StateIdle : Abs_StateIdle
{
    float ChanceToAggro = 0f;
    public override void DoEnter(Fish_Controller FSC)
    {
        idleActive = true;
        FSC.running = FSC.StartCoroutine(WanderRoutine(FSC));
        
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
        return FSC.previousState;
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
        //IFishState fish =FSC.SC.MoveBackToIdle(FSC);
        return FSC.SC.Idle;
    }
}

////////////////////////////////////////////////////////////////// Unique Behavior
public class PS_StateUnique : Abs_StateUnique
{   
    Transform orientation;
    Vector3 ramTarget;
    bool woundUp;
    public override void DoEnter(Fish_Controller FSC)
    {
        woundUp = false;
        orientation = FSC.waveHandler.UseableMesh;
        ramTarget = FSC.SC.target.transform.position;
        //woundUp = false;
        //FSC.running = FSC.StartCoroutine(WindUp(FSC));   
        orientation.localRotation = Quaternion.Euler(0, 0, 0);
        FSC.running = FSC.StartCoroutine(Rotate90Degrees(FSC));
    }

    public override void DoExit(Fish_Controller FSC)
    {
        FSC.StopCo(FSC.running);
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        if (woundUp)
        {
            
        }
        return this;
    } 
    IEnumerator Rotate90Degrees(Fish_Controller FSC) {
        // Calculate the exact target rotation relative to current
        
        Quaternion startRotation = orientation.localRotation;
        Quaternion targetRotation = orientation.localRotation * Quaternion.Euler(90, 0, 0);
        
        float progress = 0;
        while (progress < 1f) {
            ramTarget = FSC.SC.target.transform.position;
            // 1. Find direction to target
            Vector3 direction = ramTarget - FSC.transform.position;

            // 2. Create the rotation the object should eventually have
            Quaternion targetRot = Quaternion.LookRotation(direction);
            
            float angle = Quaternion.Angle(FSC.transform.rotation,targetRot);
                // 3. Rotate towards that target over time (Slerp for easing)
            FSC.transform.rotation = Quaternion.RotateTowards(FSC.transform.rotation, targetRot, FSC.agent.angularSpeed/3 * Time.deltaTime);
            

            // Smoothly move towards target based on time
            orientation.localRotation = Quaternion.RotateTowards(orientation.localRotation, targetRotation, FSC.agent.angularSpeed/6 * Time.deltaTime);

            // Check if we are close enough to stop this is the stopper because it will almost certainly happen first.
            if (Quaternion.Angle(orientation.localRotation, targetRotation) < 0.1f && angle<0.1f) {
                Debug.Log("break");
                orientation.localRotation = targetRotation; // Snap to exact value
                woundUp = true;
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
