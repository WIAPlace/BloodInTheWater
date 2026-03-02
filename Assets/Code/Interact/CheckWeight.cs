using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/1
/// Purpose: interact to check the measure of lbs of fish.
/// 
/// Edited: 
/// Edited By:
/// Edit Purpose:
/// 
public class CheckWeight : MonoBehaviour, IInteractable
{ // on interact check how much fish lbs you have caught
    private string lbsText;

    public void Interact()
    {
        if(GameState.Instance != null)
        {
            lbsText = GameState.Instance.CheckLbs().ToString() + "/" + GameState.Instance.CheckLbsNeeded().ToString() + " lbs";
            
        }
        else
        {
            lbsText = "A scale.";
        }
        StartCoroutine(GameManager.Instance.ShowFishLbs(lbsText)); // well need to figure out how to cancle this if another corutine starts up
    }   
}
