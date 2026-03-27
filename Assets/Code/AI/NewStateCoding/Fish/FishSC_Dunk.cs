using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSC_Dunk : FishSC_Abstact
{
    public LayerMask BoatMask;
    public Vector3 BoatObj;
    public void Awake()
    {
        //target = GameManager.Instance.lureTarget;
        FSC = GetComponent<Fish_Controller>();
        Idle = new Dunk_StateIdle();
        Lure = new Dunk_StateLure();
        Bobber = new Dunk_StateBobber();
        Fear = new Dunk_StateFear();
        Unique = new Dunk_StateUnique();
        Enter = new Dunk_StateEnter();
        Hook = new Dunk_StateHooked();
        Line = new Dunk_StateOnLine();
    }

    public override void BobberSpooked(Vector3 lurePosition)
    {
        if(FSC.SC.Fear!=null && FSC.currentState == FSC.SC.Unique) // turns off idle.
        {
            FSC.ChangeState(FSC.SC.Fear);
            FSC.lurePos = lurePosition;
        }
    }

    public override void LureReeledIn()
    {
        
    }

    ///////////////////////////////////////////////////////////////////////// Trigger Functions
    void OnTriggerEnter(Collider other)
    { // when the fish enters the lures trigerzone  
        //Debug.Log("entered");     
        if (FSC.SC.Lure!=null && ((1 << other.gameObject.layer) & FSC.targetMask.value) != 0)
        { // if the trigger is the bobber's lure layermask and able to be lured.
            //Debug.Log("entered");
            if(FSC.currentState == FSC.SC.Unique)
            {
                
            }
        }
    }

    public override Vector3 GetRamTarget(Fish_Controller FSC)
    {
        return BoatObj;
    }
}
