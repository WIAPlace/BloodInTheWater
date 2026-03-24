using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/8/26
/// Purpose: state controller varriant for a basic fish 
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class FishSC_Basic : FishSC_Abstact
{
    // public Abs_StateIdle Idle; // Outside of lure or bobber range, mainly just hanging out
    // public Abs_StateLure Lure; // Inside of lure range, but outside of bobber range.
    // public Abs_StateBobber Bobber; // inside of bobber range and not in fear range
    // public Abs_StateFear Fear; // bobber was too close at start.
    // public Abs_StateUnique Unique; // Uniqe behavior, probably called in idle
    // public Abs_StateEnter Enter; // on entering the scene.

    public void Awake()
    {
        target = GameManager.Instance.lureTarget;
        fishData = GetComponent<QuickTimeData_Abstract>();
        Idle = new Basic_StateIdle();
        Lure = new Basic_StateLure();
        Bobber = new Basic_StateBobber();
        Fear = new Basic_StateFear();
        Unique = new Basic_StateUnique();
        Enter = new Basic_StateEnter();
        Hook = new Basic_StateHooked();
        Line = new Basic_StateOnLine();
    }

}
