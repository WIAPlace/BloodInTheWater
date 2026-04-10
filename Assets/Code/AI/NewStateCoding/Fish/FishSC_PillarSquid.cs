using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FishSC_PillarSquid : FishSC_Abstact
{
    public void Awake()
    {
        target = GameManager.Instance.lureTarget;
        fishData = GetComponent<QuickTimeData_Abstract>();
        FSC = GetComponent<Fish_Controller>();
        Idle = new PS_StateIdle();
        Lure = new PS_StateLure();
        Bobber = new PS_StateBobber();
        Fear = new PS_StateFear();
        Unique = new PS_StateUnique();
        Enter = new PS_StateEnter();
        Hook = new PS_StateHooked();
        Line = new PS_StateOnLine();
    }

    public override void BobberSpooked(Vector3 lurePosition)
    {
        //throw new System.NotImplementedException();
    }

    public override void IdleMovement(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override void LureReeledIn()
    {
        if(FSC.currentState != FSC.SC.Idle)
        {   // if current state is not idle make it idle.
            FSC.ChangeState(FSC.SC.Idle); 
        }
    }

    public override IFishState MoveBackToIdle(Fish_Controller FSC)
    {
        return FSC.SC.Idle;
        //throw new System.NotImplementedException();
    }
}
