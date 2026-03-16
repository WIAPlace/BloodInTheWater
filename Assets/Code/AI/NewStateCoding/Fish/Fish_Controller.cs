using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;
using UnityEngine.AI;
using UnityEditorInternal;
using UnityEngine.Splines;

/// 
/// Author: Weston Tollette
/// Created: 3/8/26
/// Purpose: Controller for fish states
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Fish_Controller : MonoBehaviour
{   
    [HideInInspector]
    public QuickTimeData_Abstract fishData; 
    public LayerMask targetMask; 
    [HideInInspector]
    public Vector3 targetPos; // used for fear state
    [HideInInspector]
    public MeshWithWaves waveHandler;

    [field:SerializeField]
    public SplineContainer reelSpline;
    [HideInInspector]
    public float distOnReel = 0; // distance to move on the reel spline
    [HideInInspector]
    public float reelLength; // length of the spline.
    [HideInInspector]
    public float currentLocalOnReel; // location on the reel when called;

    [HideInInspector]
    public FishSC_Abstact SC; // state controller
    // diffrent controllers can be added for diffrent behaviors

    [HideInInspector]
    public NavMeshAgent agent;

    public IFishState currentState;
    public IFishState previousState;

    public Coroutine running;

    [field:Header("Idle")]
    public float wanderRadius = 20f;
    public float wanderTimer = 5f;

    [field:Header("Fear")]
    public float fleeDistance = 20f;
    public float fleeTimer = 5f;

    [field:Header("Lured")]
    [Tooltip("Varriance in how close it will move with each check of new destination. +-value")]
    public float lureMoveVary = 2f;

    [field:Header("Bobber")]
    [Tooltip("Range at which the fish will enter the tapping behavior")]
    public float tapEnterRange = 4f; // range how far fish can enter the tapping.
    [Tooltip("Range at which the fish will Exit the tapping behavior. Multiplied against sqr tap range")]
    public float tapExitRange = 30f; // multiplied against sqr tap range
    [Tooltip("How quickly it will go back and forth between the rod")]
    public float tapSpeed = 4f;
    [Tooltip("Range of its tapping varriance")]
    public float tapVary = 5f;
    public float tapSmooth = .1f;
    public float tapSmoothRot = 50f;
    

    [field:Header("Hook")]
    [Tooltip("value% chance the fish will actully go for the hook while tapping.")]
    public float chanceToHook = 20f;
    [Tooltip("window of opprotunity to press click to start the reel in game")]
    public float catchTimeWindow = 3f;
    [HideInInspector]
    public bool onHook = false; // will be used for collision detection and allowance with the hooked stuff.
    

    [HideInInspector]
    public bool inLureTrigger = false; //checks if in the lure trigger after fear is done

    public Vector3 lurePos; // used for holding the lure position in fear state 

    // used for debuging
    [SerializeField] [Tooltip("used to see what state we are in")]
    private string debugCurrentStateName = "";
    [SerializeField] [Tooltip("used to see what state we are in")]
    private string debugPreviousStateName = "";

    //////////////////////////////////////////////////////////////////////// Update 
    void Update() // called every frame
    {
        
        if(currentState != null)
        {
            //Debug.Log("Updating"); 
            IFishState holder = SC.DoState(this);
            if(currentState != holder) 
            { // using this as a of being able to utilize change state instead of just changing current state dirrectly
                ChangeState(holder);

                debugCurrentStateName = currentState.GetType().Name; //used for debuging to see name
                debugPreviousStateName = previousState?.GetType().Name; //used for debuging to see name
            }
        }
        
    }

    /////////////////////////////////////////////////////////////////////////// Change State
    public void ChangeState(IFishState newState)
    {
        //Debug.Log(newState);
        previousState = currentState;
        currentState?.DoExit(this); // leave the prevvious state
        currentState = newState;
        currentState?.DoEnter(this); // enter the new state   
    }

    /////////////////////////////////////////////////////////////////////////// wait to change
    /// start running a corutine then once its done change to the new state
    public void StartWaitToChange(IFishState newState, float time)
    { // will return the corutine so that later on it can be stopped
        running = StartCoroutine(WaitToChange(newState,time));
    }
    private IEnumerator WaitToChange(IFishState newState, float time)
    { // wait a number of seconds till the item is ready
        yield return new WaitForSeconds(time);
        ChangeState(newState);
    }

    ///////////////////////////////////////////////////////////////////////// Stop a corutine from running.
    public void StopCo(Coroutine activeCo)
    {
        if(activeCo != null)
        {
            //Debug.Log("stoped corutine");
            StopCoroutine(activeCo);
            activeCo = null;
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SC = GetComponent<FishSC_Abstact>();
        waveHandler = GetComponent<MeshWithWaves>();
    }

    ///////////////////////////////////////////////////////////////////////// Starting Functions
    private void OnEnable()
    { // called when enabled to do a bit of set up
        //Debug.Log("Enabled");
        StartCoroutine(slowStart());
    }
    private IEnumerator slowStart()
    {
        yield return new WaitForSeconds(.1f);
        SC.Enabled(this);
        ChangeState(SC.Enter); // at start enter idleing
    } 

    ///////////////////////////////////////////////////////////////////////// Trigger Functions
    void OnTriggerEnter(Collider other)
    { // when the fish enters the lures trigerzone  
        //Debug.Log("entered");     
        if (SC.Lure!=null && ((1 << other.gameObject.layer) & targetMask.value) != 0)
        { // if the trigger is the bobber's lure layermask and able to be lured.
            //Debug.Log("entered");
            if(currentState != SC.Fear)
            {
                targetPos = other.transform.position;
                ChangeState(SC.Lure);   
            }
            else if(currentState == SC.Lure)
            {   
                Debug.Log("entered Bobber");
                ChangeState(SC.Bobber);
            }
            else
            {
                inLureTrigger = true; // held for when they are out of fear
            }
        }
    }
    void OnTriggerExit(Collider other)
    {   
        if(((1 << other.gameObject.layer) & targetMask.value) != 0){
            if (inLureTrigger)
            { // if inluretrigger and correct trigger layer
                inLureTrigger = false; // no longer in lure trigger
            }
            if(currentState != SC.Fear || currentState != SC.Line)
            { // if fish leaves trigger zone go back to idleing.
                ChangeState(SC.Idle);
            }
        }

    }

    ///////////////////////////////////////////////////////////////////////// Collision Functions
    // on contact with bobber.
    
    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & targetMask.value) != 0 && currentState!=SC.Fear && SC.Hook!=null && onHook)
        {   // correct layer, not afraid, and able to be on the line
            
            //currentState = SC.Hook;
            ChangeState(SC.Hook);
            //fishData.SendData();
            onHook = false; // might not want this to be here till later.
            //catchWindow = true;
        }
    }
    

    ///////////////////////////////////////////////////////////////////////// Lure Reeled In
    public void LureReeledIn() // called when the lure has been reeled in.
    {
        //Debug.Log("ReeledIn");
        inLureTrigger = false;
        if(currentState == SC.Lure || currentState == SC.Bobber) // making sure this is executing only on the ones who need it
        { 
            ChangeState(SC.Idle); // change state to idle only if the current state is lure
        }
        else if(currentState == SC.Hook)
        {
            ChangeState(SC.Line);
        }
    }
    ///////////////////////////////////////////////////////////////////////// Bobber Startles fish
    public void BobberSpooked(Vector3 lurePosition)
    {
        if(SC.Fear!=null && currentState == SC.Idle) // turns off idle.
        {
            ChangeState(SC.Fear);
            lurePos = lurePosition;
        }
    }
    public void MoveAlongSpline(float dist)
    {
        reelLength = SplineUtility.CalculateLength(reelSpline.Spline,reelSpline.transform.localToWorldMatrix);
        //Vector3 localPositionOnReel = reelSpline.EvaluatePosition(1f);
        /*
        // get local position to spline
        Vector3 endTangent = reelSpline.EvaluateTangent(1f);
        Vector3 endUp = reelSpline.EvaluateUpVector(1f);
        Quaternion endRotation = Quaternion.LookRotation(endTangent, endUp);

        transform.rotation = endRotation;
        transform.position = localPositionOnReel;
        */
        
        currentLocalOnReel = 0f;
        distOnReel = dist;
        //Debug.Log(distOnReel);
    }


}
