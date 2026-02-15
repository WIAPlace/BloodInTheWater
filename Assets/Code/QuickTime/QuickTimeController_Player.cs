using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickTime;
/// 
/// Author: Weston Tollette
/// Created: 2/14/26
/// Purpose: The thing in the player that will actualy play the minigame.
/// 
/// Edited: 
/// Edited By:
/// Edit Purpose:
/// 
public class QuickTimeController_Player : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input;
    [SerializeField] [Tooltip("Refrence to the Fishing Rod Script")]
    private TestFishLure lure;
    [SerializeField] [Tooltip("Quick Time Event")]
    QuickTimeEvent_Basic qte;


    // Time Variables //
    private float timeKeeper = 0; // current time.
    private bool timeStarted = false; // should time be going.
    private float playerPoint; // used for determining where the player's  point is. 
    private float currentQTSpeed;
    private float currentQTLength;
    
    
    private bool inHit=false;

    // Quick Time Stuff // Will be initiated by the fish hooked //
    private QuickTimeData_Abstract currentQTData; // the given data at that time.
    private QuickTimeType_Enum currentType; // holder of what the current type of game is playing.
    void Awake()
    {
        if (lure == null)
        {
            lure = GetComponent<TestFishLure>();
        }
    }  

    void Update()
    {
        if (timeStarted)
        {
            KeepingTime();
        }
    }

    // Quick Time Keeping;
    private void KeepingTime() // This is basicly the Player point position during the minigame. 
    {                           // playerpoint variable only used for when the player hits the button.
        timeKeeper = Mathf.PingPong(Time.time * currentQTSpeed, currentQTLength);
    }

    public void SetType(QuickTimeType_Enum qtType) // determins what type this is
    { // might not need this
        currentType = qtType;
    }

    // Set Data //////////////////////////////////////////////////////////////////////////////////////
    private void SetNeededData() // basic stuff that will be needed every frame is better to have as an in class variable
    {   
        currentQTSpeed = currentQTData.GetQTSpeed();
        currentQTLength = currentQTData.GetQTLength();
    }
    public void SetData(QuickTimeData_Abstract data) // Abstract
    {
        currentQTData = data; 
        SetNeededData();
        Hooked();
    } 
    public void SetData(QuickTimeData_BasicFish data)// Basic Fish Type
    {
        currentQTData = data; 
        SetNeededData();
        Hooked();
    }


    // On LuredIn /////////////////////////////////////////////////////////////////////////////////////////////////
    /* // will set up later
    public void LuredIn() // when the fish is contemplating going for the lure. with tentative taps.
    {
        // Have the lure be set so that it is no longer luring in new fish, basicly have it so the lure is reeled in.
    }
    */
    // On Hook ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Hooked() // activated when the fish is hooked.
    {
        // (set off some animation for the rod).

        // Set controls to Minigame State

        timeStarted = true; // keep track of time.
        
        // set up minigame bar
        qte.InstanceUI(); // makes ui seeable on screen
        qte.SetRedArea(currentQTData.GetBarLength()); // sets the red area to bar length of the fish data 
        qte.SetHitZone(currentQTData.GetHitZoneMin(0),currentQTData.GetHitZoneMax(0));
        

        // begin minigame

        //Start corutine for the time elapsed


    }




    //inHit = qte.CheckInRange(currentQTData.GetHitZoneMin(0),currentQTData.GetHitZoneMax(0),playerPoint);
}   
