using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickTime;
using UnityEngine.UI;
using Unity.VisualScripting;
/// 
/// Author: Weston Tollette
/// Created: 2/14/26
/// Purpose: The thing in the player that will actualy play the minigame.
/// 
/// Edited: 2/15/26
/// Edited By: Weston Tollette
/// Edit Purpose: Moving the UI to this instead of the Scriptable Object.
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
    private Image playerPointUI;

    float origin;


    // Time Variables //
    private float timeKeeper = 0; // current time.
    private bool timeStarted = false; // should time be going.
    private float playerPoint; // used for determining where the player's  point is. 
    private float currentQTSpeed;
    private float currentQTLength; // how long the game will last before force quiting
    
    
    private bool inHit=false;

    // Quick Time Stuff // Will be initiated by the fish hooked //
    private QuickTimeData_Abstract currentQTData; // the given data at that time.
    private QuickTimeType_Enum currentType; // holder of what the current type of game is playing.
    void Awake()
    {
        if (useControl == null)
        {
            useControl = GetComponent<Useable_Controller>();
        }
        
        /*
        qtUI = Instantiate(qtUI); // Instantiate and alter the instantiated version in scene
        Transform hitZoneT = qtUI.transform.Find("HitBar");
        Transform playerPointT = qtUI.transform.Find("HitMarker");
        //Debug.Log(redArea); 
        hitZone = hitZoneT.GetComponent<Image>();
        playerPointUI = playerPointT.GetComponent<Image>();
        qtUI.SetActive(false); // dont have it on screen imedietly.
        */
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
    }

    void Update()
    {
        if (timeStarted)
        {
            KeepingTime();
            qte.SetPlayerPoint(timeKeeper,playerPointUI);

            
        }
    }

    // Quick Time Keeping;
    private void KeepingTime() // This is basicly the Player point position during the minigame. 
    {                           // playerpoint variable only used for when the player hits the button.
        timeKeeper = (Time.time * currentQTSpeed) % 360;
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
        //currentQTBarLength = currentQTData.GetHitZoneLength(0);

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
    public void SetData(QuickTimeData_Scuba data)
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
        // retrive lure as a way to disable the other fish from being interested and interupting.
        // should be switched out for something more elegent later
        useControl.rod.LurePrefab.SetActive(false);
        useControl.rod.RetrieveLure(useControl.rod.LurePrefab.transform.position, useControl.rod.LureRadius); 


        inHit=false; // set this to false for the moment, just in case.
        // (set off some animation for the rod).

        // Set controls to Minigame State
        input.SetQuickTime();

        timeStarted = true; // keep track of time.
        
        // set up minigame bar
        qtUI.SetActive(true);
        origin = Random.Range(0,360);
        // set the point at which the spinner will be to within the circle

        qte.SetHitZone(currentQTData.GetHitZoneLength(0), origin, hitZone);

        //qte.SetRedArea(currentQTData.GetBarLength(),redArea); // sets the red area to bar length of the fish data 
        //qte.SetHitZone(currentQTData.GetHitZoneMin(0),currentQTData.GetHitZoneMax(0),hitZone,redArea);
        //qte.SetHitZone(20,100,hitZone,redArea); used for debugging
        
        

        // begin minigame

        //Start corutine for the time elapsed


    }

    private void HandelUseQT()
    {
        timeStarted = false; // turn of time keeping for the moment
        playerPoint = timeKeeper; // this will be used to check if its in range.
        CheckInRange(0);
        if (inHit)
        {
            // shit, would be nice if we had the fish object.
            // maybe do a event thing?
            currentQTData.OnHit();
            
        }
        else
        {
            currentQTData.OnMiss();
        } 

        StartCoroutine(EndQTE(.4f));
    }
    private void HandleUseQTCancelled()
    {
        
    }

    private void CheckInRange(int i) // i will be used for array indexing later on
    {
        //float min = currentQTData.GetHitZoneMin(i);
        //float max = currentQTData.GetHitZoneMax(i);
        float hitBarArea = currentQTData.GetHitZoneLength(0) + origin;
        
        /*
        Debug.Log(hitBarArea);
        Debug.Log(origin);
        Debug.Log(playerPoint);
        */

        if(playerPoint > origin && playerPoint < hitBarArea)
        {
            inHit = true;
            //GameState.Instance.CheckWin();
        }
        else if(playerPoint + 360 > origin && playerPoint + 360 < hitBarArea)
        {
            inHit = true;
        }
        // im not doing an else false cause it will be easier to just check all then see if its true. maybe?
    }

    private IEnumerator EndQTE(float x) // turns off the qte after a number of seconds.
    {
        //useControl.ChangeState(useControl.currentItem.Readying);
        yield return new WaitForSeconds(x);
        qtUI.SetActive(false);
        input.SetGameplay();
        
    }



    //inHit = qte.CheckInRange(currentQTData.GetHitZoneMin(0),currentQTData.GetHitZoneMax(0),playerPoint);
}   
