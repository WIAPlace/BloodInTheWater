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
        SC.StartCoroutine(StunTimer(SC));
        return SC.StunnedState; // or some kind of pause state
    }
    public IEnumerator StunTimer(Scuba_Controller SC)
    {
        yield return new WaitForSeconds(SC.secondsStunned);
        SC.SetCurrentState(SC.MoveState);
    }
    
}
