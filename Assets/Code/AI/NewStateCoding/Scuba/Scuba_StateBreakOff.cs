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
    private Coroutine stun;
    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        if(stun != null)
        { // stops a corunie if its already playing
            SC.StopCoroutine(stun);
        }
        stun = SC.StartCoroutine(StunTimer(SC));
        return SC.StunnedState; // or some kind of pause state
    }
    public IEnumerator StunTimer(Scuba_Controller SC)
    {
        yield return new WaitForSeconds(SC.secondsStunned);
        SC.SetCurrentState(SC.MoveState);
    }
    
}
