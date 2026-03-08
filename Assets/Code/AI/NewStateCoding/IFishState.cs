using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/22/26
/// Purpose: Fish State interface
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public interface IFishState
{
    void DoEnter(Fish_Controller FSC);
    IFishState DoState(Fish_Controller FSC);
    void DoExit(Fish_Controller FSC);
}
