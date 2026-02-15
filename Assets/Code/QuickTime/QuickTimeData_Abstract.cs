using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using QuickTime;
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
    [SerializeField] [Tooltip("Length of the Bar")]
    protected float barLength;
    [SerializeField] [Tooltip("Area within that the player will need to hit, Minimum")]
    protected float[] hitZoneMin;
    [SerializeField] [Tooltip("Area within that the player will need to hit, Maximum")]
    protected float[] hitZoneMax;
    [SerializeField] [Tooltip("How fast the player hit indicator should move")]
    protected float qtSpeed;
    [SerializeField] [Tooltip("How long should the allowable time be for this")]
    protected float qtLength ;
    [SerializeField] [Tooltip("Type of Game")]
    protected QuickTimeType_Enum type;
 
    public QuickTimeData_Abstract(QuickTimeData_Abstract other)
    {
        this.barLength=other.barLength;
        this.hitZoneMin = other.hitZoneMin;
        this.hitZoneMax = other.hitZoneMax;
        this.qtSpeed = other.qtSpeed;
        this.qtLength = other.qtLength;
        this.qtcPlayer = other.qtcPlayer;
    }

    public void SendType() // Sends the Enum to the Rod Script for use in switch statement;
    {
        qtcPlayer.SetType(type); // set it to the current type
    }
    public void SendData() // sends the data to the Rod scipt.
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
    public float GetHitZoneMin(int i)
    {
        return hitZoneMin[i];
    }
    public float GetHitZoneMax(int i)
    {
        return hitZoneMax[i];
    }

}
