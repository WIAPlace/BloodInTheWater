using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/24/26
/// Purpose: State interface for monsters on the boat
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public interface IBoatStomperState
{
    void DoEnter(Scuba_Controller SC);
    IBoatStomperState DoState(Scuba_Controller SC);
    void DoExit(Scuba_Controller SC);
}
