using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;
using Unity.VisualScripting.FullSerializer;

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
    Vector3 ramTarget;
    Vector3 targetDirection;
    Quaternion targetRotation;
    bool woundUp = false;
    public override void DoEnter(Fish_Controller FSC)
    {
        ramTarget = FSC.SC.GetRamTarget(FSC);
        woundUp = false;
        FSC.running = FSC.StartCoroutine(WindUp(FSC));   
        // 1. Determine the direction to the target.
        targetDirection = ramTarget - FSC.transform.position;
        targetDirection = new Vector3(targetDirection.x,FSC.transform.position.y,targetDirection.z);
        // 2. Create the target rotation (a Quaternion looking in that direction).
        targetRotation = Quaternion.LookRotation(targetDirection);
        TutorialManager.Instance.TriggerTutorial(1,4);
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        if(!woundUp){
            // do some splashing or something
            if (ramTarget != null && FSC.transform.rotation != targetRotation)
            {
                // 3. Rotate the current transform towards the target rotation at a specified speed.
                //    The speed is in degrees per second, so multiply by Time.deltaTime.
                FSC.transform.rotation = Quaternion.RotateTowards(FSC.transform.rotation, targetRotation, FSC.agent.angularSpeed * Time.deltaTime);
            }
        }
        else
        {
            FSC.transform.Translate(Vector3.forward * FSC.agent.speed * 3 * Time.deltaTime);
        }
        return this;
    }
    public IEnumerator WindUp(Fish_Controller FSC)
    {
        yield return new WaitForSeconds(FSC.wanderTimer);
        woundUp =  true;
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
