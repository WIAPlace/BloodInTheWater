using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;

////////////////////////////////////////////////////////////////// In Idle Range
public class Dunk_StateIdle : Abs_StateIdle
{
    float ChanceToAggro = 0f;
    public override void DoEnter(Fish_Controller FSC)
    {   // start the corutine that will check if it should become aggressive
        FSC.running = FSC.StartCoroutine(Aggro(FSC));
        FSC.agent.isStopped = true; // turn off navmesh shit
        ChanceToAggro = FSC.chanceToHook;
    }

    public override void DoExit(Fish_Controller FSC)
    {   // stop the corutine for it checking if its aggressive
        FSC.StopCo(FSC.running);
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        UpdatePosition(FSC);
        return this;
    }
    private void UpdatePosition(Fish_Controller FSC)
    {
        FSC.SC.IdleMovement(FSC);
    }
    
    public IEnumerator Aggro(Fish_Controller FSC)
    {
        yield return new WaitForSeconds(FSC.catchTimeWindow * FSC.lureMoveVary);
        while (true) // just go forever if allowed
        {
            yield return new WaitForSeconds(FSC.catchTimeWindow); // wait a bit
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
public class Dunk_StateLure : Abs_StateLure
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
public class Dunk_StateBobber : Abs_StateBobber
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
public class Dunk_StateFear : Abs_StateFear
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
        IFishState fish =FSC.SC.MoveBackToIdle(FSC);
        return fish;
    }
}

////////////////////////////////////////////////////////////////// Unique Behavior
public class Dunk_StateUnique : Abs_StateUnique
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
            FSC.transform.Translate(Vector3.forward * FSC.agent.speed * Time.deltaTime);
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
public class Dunk_StateEnter : Abs_StateEnter
{
    public override void DoEnter(Fish_Controller FSC)
    {
       GameManager.Instance.unlocks.SaveMonsterData(1);
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return FSC.SC.Idle;
    }
}


public class Dunk_StateHooked : Abs_StateHooked
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
public class Dunk_StateOnLine : Abs_StateOnLine
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