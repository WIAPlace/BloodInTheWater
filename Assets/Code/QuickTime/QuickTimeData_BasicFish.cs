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

    public override void SendData() // sends the data to the Rod scipt.
    {
        qtcPlayer.SetData(this);
    }

    public override  void OnHit()
    {
        gameObject.SetActive(false);
        //GameState.Instance.CaughtAFish(1); // send over the caught state to the gamestate thing.
    }
}
