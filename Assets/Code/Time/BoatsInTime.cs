using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/17/26
/// Purpose: The Boat's Hull and it relation to time.
/// 
/// Edited: 
/// Edited By:
/// Edit Purpose:
/// 
public class BoatsInTime : MonoBehaviour
{
    [SerializeField] [Tooltip("Boat Object")]
    GameObject boat;
    [SerializeField] [Tooltip("Refrence to the Time Keeper")]
    TimeKeeper timeKeeper;
    
    
    

    // Callable Functions ///////
    public void HitHull(float timePenalty)
    { // throw that over to the time keeper. 
        timeKeeper.AddPenaltyTime(timePenalty);
        // likely play some audio here.
        // Shake the camera
    }
}
