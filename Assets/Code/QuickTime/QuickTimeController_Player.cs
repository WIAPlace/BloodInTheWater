using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickTime;
using UnityEngine.UI;
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
    private TestFishLure lure;
    [SerializeField] [Tooltip("Quick Time Event")]
    QuickTimeEvent_Basic qte;
    [SerializeField] [Tooltip("The quick time mini game bar")]
    private GameObject qtUI;

    // Quick Time UI
    private Image redArea;
    private Image hitZone;
    private Image playerPointUI;


    // Time Variables //
    private float timeKeeper = 0; // current time.
    private bool timeStarted = false; // should time be going.
    private float playerPoint; // used for determining where the player's  point is. 
    private float currentQTSpeed;
    private float currentQTLength; // how long the game will last before force quiting
    private float currentQTBarLength;
    
    
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
        qtUI = Instantiate(qtUI); // Instantiate and alter the instantiated version in scene
        Transform redAreaT = qtUI.transform.Find("RedArea");
        Transform hitZoneT = qtUI.transform.Find("HitZone");
        Transform playerPointT = qtUI.transform.Find("PlayerPoint");
        //Debug.Log(redArea);
        redArea = redAreaT.GetComponent<Image>();
        hitZone = hitZoneT.GetComponent<Image>();
        playerPointUI = playerPointT.GetComponent<Image>();
        qtUI.SetActive(false); // dont have it on screen imedietly.
    }
    void Start()
    {
        input.UseEventQT += HandelUseQT;
    }
    void OnDestroy()
    {
        input.UseEventQT -= HandelUseQT;
    }

    void Update()
    {
        if (timeStarted)
        {
            KeepingTime();
            qte.SetPlayerPoint(timeKeeper,playerPointUI,redArea);
        }
    }

    // Quick Time Keeping;
    private void KeepingTime() // This is basicly the Player point position during the minigame. 
    {                           // playerpoint variable only used for when the player hits the button.
        timeKeeper = Mathf.PingPong(Time.time * currentQTSpeed, currentQTBarLength);
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
        currentQTBarLength = currentQTData.GetBarLength();

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
        inHit=false; // set this to false for the moment, just in case.
        // (set off some animation for the rod).

        // Set controls to Minigame State
        input.SetQuickTime();

        timeStarted = true; // keep track of time.
        
        // set up minigame bar
        qtUI.SetActive(true);
        qte.SetRedArea(currentQTData.GetBarLength(),redArea); // sets the red area to bar length of the fish data 
        qte.SetHitZone(currentQTData.GetHitZoneMin(0),currentQTData.GetHitZoneMax(0),hitZone,redArea);
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

        StartCoroutine(EndQTE(.4f));
    }

    private void CheckInRange(int i) // i will be used for array indexing later on
    {
        float min = currentQTData.GetHitZoneMin(i);
        float max = currentQTData.GetHitZoneMax(i);

        if(playerPoint > min && playerPoint < max)
        {
            inHit = true;
            GameState.Instance.CheckWin();
        }
        // im not doing an else false cause it will be easier to just check all then see if its true. maybe?
    }

    private IEnumerator EndQTE(float x) // turns off the qte after a number of seconds.
    {
        lure.AutoHandled();
        yield return new WaitForSeconds(x);
        qtUI.SetActive(false);
        input.SetGameplay();
        
    }



    //inHit = qte.CheckInRange(currentQTData.GetHitZoneMin(0),currentQTData.GetHitZoneMax(0),playerPoint);
}   
