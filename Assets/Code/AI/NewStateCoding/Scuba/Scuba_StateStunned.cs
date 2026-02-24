using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/24/26
/// Purpose: Scuba Stunned and can't make contact with the player
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Scuba_StateStunned : IBoatStomperState
{
    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        // play some animation for being stunned;
        return SC.StunnedState; 
    }

}
