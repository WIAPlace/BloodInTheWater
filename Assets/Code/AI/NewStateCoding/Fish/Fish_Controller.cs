using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;
using UnityEngine.AI;
using UnityEditorInternal;

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
    public Vector3 targetPos;

    [HideInInspector]
    public FishSC_Abstact stateController;

    [HideInInspector]
    public NavMeshAgent agent;

    public IFishState currentState;
    public IFishState previousState;

    public Coroutine running;

    
    public float wanderRadius = 20f;
    public float wanderTimer = 5f;

    public float fleeDistance = 20f;
    public float fleeTimer = 5f;

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
            IFishState holder = stateController.DoState(this);
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
        stateController = GetComponent<FishSC_Abstact>();
    }

    ///////////////////////////////////////////////////////////////////////// Starting Functions
    private void OnEnable()
    { // called when enabled to do a bit of set up
        //Debug.Log("Enabled");
        StartCoroutine(slowStart());
    }
    private IEnumerator slowStart()
    {
        yield return new WaitForSeconds(.2f);
        stateController.Enabled(this);
        ChangeState(stateController.Enter); // at start enter idleing
    } 

    ///////////////////////////////////////////////////////////////////////// Trigger Functions
    void OnTriggerEnter(Collider other)
    { // when the fish enters the lures trigerzone  
        //Debug.Log("entered");     
        if (stateController.Lure!=null && ((1 << other.gameObject.layer) & targetMask.value) != 0)
        { // if the trigger is the bobber's lure layermask and able to be lured.
            //Debug.Log("entered");
            if(currentState != stateController.Fear)
            {
                targetPos = other.transform.position;
                ChangeState(stateController.Lure);   
            }
            else
            {
                inLureTrigger = true; // held for when they are out of fear
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (inLureTrigger && ((1 << other.gameObject.layer) & targetMask.value) != 0)
        { // if inluretrigger and correct trigger layer
            inLureTrigger = false; // no longer in lure trigger
        }
    }

    ///////////////////////////////////////////////////////////////////////// Collision Functions
    // on contact with bobber.
    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & targetMask.value) != 0 && currentState!=stateController.Fear && stateController.Bobber!=null)
        {   // correct layer, not afraid, and able to be on the line
            currentState = stateController.Bobber;
            fishData.SendData();
        }
    }

    ///////////////////////////////////////////////////////////////////////// Lure Reeled In
    public void LureReeledIn() // called when the lure has been reeled in.
    {
        inLureTrigger = false;
        if(currentState == stateController.Lure) // making sure this is executing only on the ones who need it
        { 
            ChangeState(stateController.Idle); // change state to idle only if the current state is lure
        }
    }
    ///////////////////////////////////////////////////////////////////////// Bobber Startles fish
    public void BobberSpooked(Vector3 lurePosition)
    {
        if(stateController.Fear!=null && currentState == stateController.Idle) // turns off idle.
        {
            ChangeState(stateController.Fear);
            lurePos = lurePosition;
        }
    }


}
