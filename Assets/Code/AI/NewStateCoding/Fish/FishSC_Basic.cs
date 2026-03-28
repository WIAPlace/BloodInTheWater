using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/8/26
/// Purpose: state controller varriant for a basic fish 
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class FishSC_Basic : FishSC_Abstact
{
    // public Abs_StateIdle Idle; // Outside of lure or bobber range, mainly just hanging out
    // public Abs_StateLure Lure; // Inside of lure range, but outside of bobber range.
    // public Abs_StateBobber Bobber; // inside of bobber range and not in fear range
    // public Abs_StateFear Fear; // bobber was too close at start.
    // public Abs_StateUnique Unique; // Uniqe behavior, probably called in idle
    // public Abs_StateEnter Enter; // on entering the scene.
    
    public void Awake()
    {
        target = GameManager.Instance.lureTarget;
        fishData = GetComponent<QuickTimeData_Abstract>();
        FSC = GetComponent<Fish_Controller>();
        Idle = new Basic_StateIdle();
        Lure = new Basic_StateLure();
        Bobber = new Basic_StateBobber();
        Fear = new Basic_StateFear();
        Unique = new Basic_StateUnique();
        Enter = new Basic_StateEnter();
        Hook = new Basic_StateHooked();
        Line = new Basic_StateOnLine();
    }

    ///////////////////////////////////////////////////////////////////////// Trigger Functions
    void OnTriggerEnter(Collider other)
    { // when the fish enters the lures trigerzone  
        //Debug.Log("entered");     
        if (FSC.SC.Lure!=null && ((1 << other.gameObject.layer) & FSC.targetMask.value) != 0)
        { // if the trigger is the bobber's lure layermask and able to be lured.
            //Debug.Log("entered");
            if(FSC.currentState != FSC.SC.Fear)
            {
                FSC.targetPos = other.transform.position;
                FSC. ChangeState(FSC.SC.Lure);   
            }
            else if(FSC.currentState == FSC.SC.Lure)
            {   
                Debug.Log("entered Bobber");
                FSC.ChangeState(FSC.SC.Bobber);
            }
            else
            {
                FSC.inLureTrigger = true; // held for when they are out of fear
            }
        }
    }
    void OnTriggerExit(Collider other)
    {   
        if(((1 << other.gameObject.layer) & FSC.targetMask.value) != 0){
            if (FSC.inLureTrigger)
            { // if inluretrigger and correct trigger layer
                FSC.inLureTrigger = false; // no longer in lure trigger
            }
            if(FSC.currentState != FSC.SC.Fear || FSC.currentState != FSC.SC.Line)
            { // if fish leaves trigger zone go back to idleing.
                FSC.ChangeState(FSC.SC.Idle);
            }
        }

    }

    ///////////////////////////////////////////////////////////////////////// Collision Functions
    // on contact with bobber.
    
    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & FSC.targetMask.value) != 0 && FSC.currentState!=FSC.SC.Fear && FSC.SC.Hook!=null && FSC.onHook)
        {   // correct layer, not afraid, and able to be on the line
            
            //currentState = SC.Hook;
            FSC.ChangeState(FSC.SC.Hook);
            //fishData.SendData();
            FSC.onHook = false; // might not want this to be here till later.
            //catchWindow = true;
        }
    }

    public override void BobberSpooked(Vector3 lurePosition)
    {
        if(FSC.SC.Fear!=null && FSC.currentState == FSC.SC.Idle) // turns off idle.
        {
            FSC.ChangeState(FSC.SC.Fear);
            FSC.lurePos = lurePosition;
        }
    }

    public override void LureReeledIn()
    {
        //Debug.Log("ReeledIn");
        FSC.inLureTrigger = false;
        if(FSC.currentState == FSC.SC.Lure || FSC.currentState == FSC.SC.Bobber) // making sure this is executing only on the ones who need it
        { 
            FSC.ChangeState(FSC.SC.Idle); // change state to idle only if the current state is lure
        }
        else if(FSC.currentState == FSC.SC.Hook)
        {
            FSC.ChangeState(FSC.SC.Line);
        }
    }

    public override void IdleMovement(Fish_Controller FSC)
    {
        throw new System.NotImplementedException();
    }

    public override IFishState MoveBackToIdle(Fish_Controller FSC)
    {
        throw new System.NotImplementedException();
    }
}
