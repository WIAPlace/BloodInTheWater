using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/21/26
/// Purpose: This script is a chedker for what the player is currently holding
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class PlayerHolding : MonoBehaviour
{
    private Useable_Controller useControl;
    private void Start()
    {
        useControl = GetComponent<Useable_Controller>();   
    }

    private bool checkingActive = false; // used for comunicating between placeAble Spots and See Placeable Areas.


    public int CheckInHand() // chedk what the index is in hand
    {
        return useControl.currentItemIndex;
    }

    public void ChangeInHand(int index) // change what is being held.
    { 
        useControl.currentItemIndex = index;
        useControl.ChangeState(useControl.currentItem.Place);
    } 

    // Communicators between placeable Spot and See Placeable Areas Scritps
    // this is needed because if you press on an active indicator it should disapear if no longer holding down Q 
    public bool CheckIfChecking()
    { // check if Q(/other equivilent) is down
        return checkingActive;
    }

    public void SetChecking(bool checking)
    { // set checking active in see placeable Areas.
        checkingActive = checking;
    }

}
