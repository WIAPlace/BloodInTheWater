using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickTime;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.ProBuilder;
//using System;
/// 
/// Author: Weston Tollette
/// Created: 2/14/26
/// Purpose: The thing in the player that will actualy play the minigame.
/// 
/// Edited: 2/15/26
/// Edited By: Weston Tollette
/// Edit Purpose: Moving the UI to this instead of the Scriptable Object.
/// 
/// Edited: 3/21/26
/// Edited By: Weston Tollette
/// Edit Purpose: changing minigame to be more traditional fishing hold spot in reel zone
/// 
public class QuickTimeController_Player : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input;
    [SerializeField] [Tooltip("Refrence to the Fishing Rod Script")]
    private Useable_Controller useControl;
    [SerializeField] [Tooltip("Quick Time Event")]
    QuickTimeEvent_Basic qte;
    [SerializeField] [Tooltip("The quick time mini game bar")]
    private GameObject qtUI;

    // Quick Time UI
    [SerializeField]
    private Image hitZone;
    [SerializeField]
    private Image qtHitMarker;
    [SerializeField]
    private Image qtCompletion;

    [Header("QT Variables")]
    [SerializeField] // size of hitzone
    private float hitArea;
    
    [SerializeField] [Tooltip("How fast should it's max be on either side of spinning")]
    private float maxHitSpeed;
    [SerializeField] [Tooltip("Rat of change speed for changing directions")]
    private float changeSmooth;
    private float currentHitSpeed=0;
    private float _hitVelocity = 0f;

    private float currentHitSpot = 0; // will be used to check where the hit zone is at any given time
    private float hitMarker =0; // will be used to check hit marker position at any time
    
    //float origin;

    private bool counterClockwise = false;
    

    // Time Variables //
    //private float timeKeeper = 0; // current time.
    private bool inProgress = false; // should time be going.

    private Coroutine grace; 
    
    private bool inHit=false;
    private float completionAmnt = 0; // will help determin the amount needed for completion or amount to loose

    // Quick Time Stuff // Will be initiated by the fish hooked //
    private QuickTimeData_Abstract currentQTData; // the given data at that time.
    private QuickTimeType_Enum currentType; // holder of what the current type of game is playing.

    void Awake()
    {
        if (useControl == null)
        {
            useControl = GetComponent<Useable_Controller>();
        }
    }
    void Start()
    {
        input.UseEventQT += HandelUseQT;
        input.UseEventQTCanelled += HandleUseQTCancelled;
        
    }
    void OnDestroy()
    {
        input.UseEventQT -= HandelUseQT;
        input.UseEventQTCanelled -= HandleUseQTCancelled;
        input.InteractEventQT -= NextHint;
        //input.InteractEventQT -= TurnTimeBackOn;
    }

    // Update //////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (inProgress)
        {
            //Set QT Point
            float location = currentQTData.GetQTMove(Time.deltaTime);
            hitMarker = (hitMarker + location + 360) % 360;
            
            qtHitMarker.transform.rotation = Quaternion.Euler(Vector3.forward * hitMarker);
            
            //Set Player Hit Zone
            RotateHitZone();

            //Debug.Log("currentHitSpot: " + currentHitSpot);
            //Debug.Log("ExtendedHitSpot: " + (currentHitSpot+hitArea));
            //Debug.Log("HitMarker: "+hitMarker);

            // check if the hit marker is in the hit range
            inHit = CheckInRange(hitMarker);

            // if true will run on hit, if false will run on miss;
            if (inHit)
            {
                currentQTData.OnHit(); 
            }
            else
            {
                currentQTData.OnMiss();
            }
            
            // completion amount and change of grace time
            completionAmnt += currentQTData.GetCompletionRate(inHit)*Time.deltaTime; // will add the rate of change overtime to completion amount
            completionAmnt = Mathf.Clamp(completionAmnt,0,1);

            // change reticle to know how complete it is.
            qtCompletion.fillAmount = completionAmnt;
            currentQTData.QTStatus(completionAmnt);

            if(completionAmnt >= 1) // win
            {   
                EndQTEAll(true);
            }
            else if(completionAmnt <= 0) //lose
            {
                if(currentQTData.type == QuickTimeType_Enum.BasicFish) EndQTEAll(false);
            }
            
        }
    }

    // Rotate Hit Zone //////////////////////////////////////////////////////////////////////////////////////
    private void RotateHitZone()
    { // rotates the players hit zone. wether the key is down the spinner will move clockwise or counter
        float chosenHitSpeed;
        if (counterClockwise)
        { // if QT use key is down
            chosenHitSpeed = maxHitSpeed;
        }
        else
        { // if QT use Key is not down
            chosenHitSpeed = -maxHitSpeed;
        }
        // Smoothly damp the current speed towards the target speed
        currentHitSpeed = Mathf.SmoothDamp(currentHitSpeed, chosenHitSpeed, ref _hitVelocity, changeSmooth);

        currentHitSpot = (currentHitSpot + (currentHitSpeed * Time.deltaTime)+360) % 360 ;//% 360; // move the hit spot with the current speed over time.
        

        hitZone.transform.rotation = Quaternion.Euler(Vector3.forward* currentHitSpot);
    }

    // Check if in Range //////////////////////////////////////////////////////////////////////////////////////
    private bool CheckInRange(float marker)
    { // every update it will check if the hit marker is within the hit zone
        bool setter = false; // will be returned as to say if its in range

        if(marker>currentHitSpot && marker < currentHitSpot + hitArea)
        { // in Hit Zone
            if(qtHitMarker.color != Color.green) qtHitMarker.color = Color.green; // visual Debugging
            setter = true;
        }
        else if(marker + 360 > currentHitSpot && marker + 360 < currentHitSpot+hitArea)
        { // in Hit Zone
            if(qtHitMarker.color != Color.green) qtHitMarker.color = Color.green; // visual Debugging
            setter = true;
        }

        else
        { // outside of Hit Zone
            if(qtHitMarker.color != Color.red) qtHitMarker.color = Color.red; // visual debugging
            setter = false;
        }
        return setter;
    }
    
    // Set Data //////////////////////////////////////////////////////////////////////////////////////
    public void SetData(QuickTimeData_Abstract data) // Abstract
    {
        currentQTData = data; 
        HookTutor();
    } 
    public void SetData(QuickTimeData_BasicFish data)// Basic Fish Type
    {
        currentQTData = data; 
        HookTutor();
    }
    public void SetData(QuickTimeData_Scuba data)
    {
        currentQTData = data; 
        HookTutor();
    }


    private void HookTutor()
    {
        if (GameManager.Instance.hintsEnabled)
        { // if hints are enabled play the hint
            //Time.timeScale = 0f;
            TutorialManager.Instance.TriggerTutorial(2,0); // Hold Release
        }
        Hooked();
    }
    // On Hook ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Hooked() // activated when the fish is hooked.
    {
        // retrive lure as a way to disable the other fish from being interested and interupting.
        // should be switched out for something more elegent later

        useControl.rod.LurePrefab.SetActive(false);
        useControl.rod.RetrieveLure(useControl.rod.LurePrefab.transform.position, useControl.rod.LureRadius); 


        currentHitSpeed = 0; // change hit speed to 0;
        inHit=false; // set this to false for the moment, just in case.
        completionAmnt = .5f;
        // (set off some animation for the rod).
        if(useControl.currentItem == useControl.rod)
        {
            useControl.rod.RodTriggerAnimator(useControl,3);
        }
        // Set controls to Minigame State
        input.SetQuickTime();

        inProgress = true; // keep track of time.
        
        // set up minigame bar
        qtUI.SetActive(true);
        // set the point at which the spinner will be to within the circle

        hitZone.fillAmount = hitArea/360;      

        // begin minigame

        //Start corutine for the time elapsed


    }

    private void HandelUseQT()
    {
        counterClockwise = true;
    }
    private void HandleUseQTCancelled()
    {
        counterClockwise = false;
    }

    private void EndQTEAll(bool status)
    {
        if(useControl.currentItem == useControl.rod)
        {
            useControl.rod.RodTriggerAnimator(useControl,0);
        }
        input.InteractEventQT -= NextHint;
        inProgress = false;
        currentQTData.ExitQuickTimeEvent(status);
        StartCoroutine(EndQTE(.4f));
    }
    private IEnumerator EndQTE(float x) // turns off the qte after a number of seconds.
    {
        //useControl.ChangeState(useControl.currentItem.Readying);
        yield return new WaitForSeconds(x);
        qtUI.SetActive(false);
        input.SetGameplay();
        
    }

    public void PlayFakeFish(GameObject fish, int rot, float normalRandy)
    {
        //Debug.Log("Hit");
        useControl.ReelInFakeFish(fish,rot,normalRandy);
    }
    
    
    private void NextHint()
    {
        Hooked();
        input.InteractEvent -= NextHint;
    }
    /*
    private IEnumerator HintForCast()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(.01f);
        Time.timeScale = 0f;
        GameManager.Instance.GiveHint(2,1);
        input.InteractEventQT -=NextHint;
        input.InteractEventQT += TurnTimeBackOn;
    }
    private void TurnTimeBackOn()
    {
        Time.timeScale = 1f;
        input.InteractEventQT -= TurnTimeBackOn;
        Hooked();
    }
    */

    ////////////////////////////////////////////////////////////////// Arcade Mode Settings Changes
    /// Getters
    public float GetHitArea()
    {
       return hitArea; 
    }
    public float GetHitSpeed()
    {
        return maxHitSpeed;
    }
    public float GetHitSmooth()
    {
        return changeSmooth;
    }

    /// Setters
    public void ChangeHitArea(float newArea)
    {
       hitArea = newArea;
    }
    public void ChangeHitSpeed(float newSpeed)
    {
        maxHitSpeed = newSpeed;
    }
    public void changeHitSmooth(float newSmooth)
    {
        changeSmooth = newSmooth;
    }
    
}   
