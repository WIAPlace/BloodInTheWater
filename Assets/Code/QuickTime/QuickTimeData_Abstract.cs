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
    [SerializeField] [Tooltip("How fast the marker indicator should move. Pixels per Second.")]
    protected float qtSpeed = 360;
    [SerializeField] 
    protected float qtSmoothMotion = 1f;    

    [SerializeField] [Tooltip("How long should the allowable time be for this")]
    protected float qtLength;
    [SerializeField] [Tooltip("Type of Game")]
    protected QuickTimeType_Enum type;
 
    protected float timeKeeper;
    [SerializeField][Tooltip("max amount of time between behavior changes")]
    protected float timerMultiplyer=3f;

    protected float qtDestin;

    [Header("Completion Rates")]
    [SerializeField] [Tooltip("If win and loose rate should be two diffrent values")]
    protected bool diffRate = false;
    [SerializeField] [Tooltip("How quickly the Completion Bar should fill")]
    protected float winRate=3f;
    [SerializeField] [Tooltip("How quickly the Completion Bar should empty. Will be made negative in code")]
    protected float looseRate=3f;

    public QuickTimeData_Abstract(QuickTimeData_Abstract other)
    {
        this.hitZoneLength = other.hitZoneLength;
        this.qtSpeed = other.qtSpeed;
        this.qtLength = other.qtLength;
        this.qtcPlayer = other.qtcPlayer;
    }
    private void OnValidate()
    { 
        if (!diffRate) // if they should not be diffrent they are made the same.
        {
            looseRate = winRate;
        }
    }
    public virtual float GetCompletionRate(bool rate)
    {
        float setter=0;
        if (rate)
        {
            setter = winRate;
        }
        else
        {
            setter = -looseRate;
        }
        return setter;
    } 

    public abstract void OnHit(); // when you win the minigame this occurs.

    public abstract void OnMiss(); // when you miss 

    public virtual void SendData() // sends the data to the Rod scipt.
    {
        qtcPlayer.SetData(this);
        EnterQTEvent();
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

    public virtual float GetQTMove(float location)
    {
        timeKeeper -= location;
        if(timeKeeper < 0f)
        {
            timeKeeper = UnityEngine.Random.value * timerMultiplyer;
            // (0-1) * time Multiplyer

            qtDestin = (UnityEngine.Random.value * 360)-180;

        }
        float newlocal;
        newlocal = Mathf.SmoothDamp(location,qtDestin,ref qtSpeed, qtSmoothMotion);

        return newlocal;
    }  

    /// Status
    public abstract void QTStatus(float amnt);
    public abstract void EnterQTEvent(); // enter the quick time event and change any things you need to while in it
    public abstract void ExitQuickTimeEvent(bool status); // status for if they have won or lost
}
