using System.Collections;
using System.Collections.Generic;
using TLC.FishStates;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/8/26
/// Purpose: Abstract for fish states and their interfaces, so that the states can be easily changed for diffrenct fish
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public abstract class FishSC_Abstact : MonoBehaviour
{
    //[SerializeField][Tooltip("This will be a dadded to the controller's fishdata")]
    protected QuickTimeData_Abstract fishData;

    public GameObject target;

    public Abs_StateIdle Idle; // Outside of lure or bobber range, mainly just hanging out
    public Abs_StateLure Lure; // Inside of lure range, but outside of bobber range.
    public Abs_StateBobber Bobber; // inside of bobber range and not in fear range
    public Abs_StateFear Fear; // bobber was too close at start.
    public Abs_StateUnique Unique; // Uniqe behavior, probably called in idle
    public Abs_StateEnter Enter; // on entering the scene.

    public virtual IFishState DoState(Fish_Controller FSC)
    {
        return FSC.currentState.DoState(FSC);
    }

    public virtual void Enabled(Fish_Controller FSC)
    {
        FSC.fishData = fishData; // put the new data as this data.
    }
}
