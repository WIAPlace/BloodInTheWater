using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/28/26
/// Purpose: Scuba hit
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Scuba_StateHit : IBoatStomperState
{
    public void DoEnter(Scuba_Controller SC)
    {
        if(SC.stun != null)
        { // stops a corunie if its already playing
            SC.StopCoroutine(SC.stun);
        }
        
        
        //SC.rb.AddForce(SC.rb.mass * SC.hitForce * SC.hitDir);
    }

    public void DoExit(Scuba_Controller SC)
    {
        //SC.StopCoroutine(SC.stun);
    }

    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        SC.rb.isKinematic = false;
        SC.rb.AddForce(SC.rb.mass * SC.hitForce * SC.hitDir);
        return SC.BreakOffState;
    }
}
