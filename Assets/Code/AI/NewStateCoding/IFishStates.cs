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
public interface IFishStates 
{
    IFishStates DoState(FishStateController FSC);
}
