using System.Collections;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/24/26
/// Purpose: Scuba man spawns into the game
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Scuba_StateBreakOff : IBoatStomperState
{
    
    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        //Debug.Log("begun");
        if(SC.stun != null)
        { // stops a corunie if its already playing
            SC.StopCoroutine(SC.stun);
        }
        SC.stun = SC.StartCoroutine(StunTimer(SC));
        return SC.StunnedState; // or some kind of pause state
    }
    public IEnumerator StunTimer(Scuba_Controller SC)
    {
        SC.agent.enabled = false;
        yield return new WaitForSeconds(SC.secondsStunned);
        SC.agent.enabled = true;
        SC.rb.isKinematic = true;
        
        SC.SetCurrentState(SC.MoveState);
    }
    
}
