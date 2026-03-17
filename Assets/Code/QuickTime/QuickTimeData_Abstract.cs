using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickTime;
using Unity.VisualScripting;
/// 
/// Author: Weston Tollette
/// Created: 2/12/26
/// Purpose: This is an abstract for all quick time minigames things.
/// 
/// Edited: 2/13/26
/// Edited By: 
/// Edit Purpose:
/// 
public abstract class QuickTimeData_Abstract : MonoBehaviour
{

    [SerializeField] [Tooltip("Refrence to Controller")]
    protected QuickTimeController_Player qtcPlayer; // QuickTimeController_Player 

    [Header("Mini Game Stats")]
    [SerializeField] [Tooltip("Area within that the player will need to hit, Maximum")]
    protected float[] hitZoneLength;
    [SerializeField] [Tooltip("How fast the player hit indicator should move. Pixels per Second.")]
    protected float qtSpeed = 360;
    [SerializeField] [Tooltip("How long should the allowable time be for this")]
    protected float qtLength;
    [SerializeField] [Tooltip("Type of Game")]
    protected QuickTimeType_Enum type;
 
    public QuickTimeData_Abstract(QuickTimeData_Abstract other)
    {
        this.hitZoneLength = other.hitZoneLength;
        this.qtSpeed = other.qtSpeed;
        this.qtLength = other.qtLength;
        this.qtcPlayer = other.qtcPlayer;
    }

    public abstract void OnHit(); // when you win the minigame this occurs.

    public abstract void OnMiss(); // when you miss 

    public void SendType() // Sends the Enum to the Rod Script for use in switch statement;
    {
        qtcPlayer.SetType(type); // set it to the current type
    }
    public virtual void SendData() // sends the data to the Rod scipt.
    {
        qtcPlayer.SetData(this);
    }
    public float GetQTSpeed()
    {
        return qtSpeed;
    }
    public float GetQTLength()
    {
        return qtLength;
    }
    public float GetHitZoneLength(int i)
    {
        return hitZoneLength[i];
    }    
}
