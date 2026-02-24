using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/24/26
/// Purpose: Scuba makes contact with player
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Scuba_StateContact : IBoatStomperState
{
    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        Debug.Log("Hit");
        SC.scubaData.SendData();
        return null;
    }

}
