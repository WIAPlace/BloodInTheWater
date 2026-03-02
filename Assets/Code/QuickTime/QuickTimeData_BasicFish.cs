using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/12/26
/// Purpose: Basic 1 zone hit thing.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class QuickTimeData_BasicFish : QuickTimeData_Abstract
{
    private float fishLength;
    private float fishLbs;
    public QuickTimeData_BasicFish(QuickTimeData_BasicFish other) : base(other)
    {
        this.fishLength = other.fishLength; 
        this.fishLbs = other.fishLbs;
    }

    private void OnEnable()
    {
        fishLbs = Random.Range(5f,15f);
    }

    public override void SendData() // sends the data to the Rod scipt.
    {
        qtcPlayer.SetData(this);
    }

    public override  void OnHit()
    {   // change the active state and declare that fish have been caught
        gameObject.SetActive(false);
        GameState.Instance.CaughtFish(fishLbs); // send over the caught state to the gamestate thing.
    }
}
