using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/28/26
/// Purpose: Scuba hit
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Scuba_StateHit : IBoatStomperState
{
    public IBoatStomperState DoState(Scuba_Controller SC)
    {
        //Debug.Log("Hit State Entered");
        if (SC.gameObject.TryGetComponent(out Rigidbody rb)){
            //Debug.Log("force added");
            
            rb.AddForce(SC.hitDir*SC.hitForce);
        }
        return SC.BreakOffState;
    }
}
