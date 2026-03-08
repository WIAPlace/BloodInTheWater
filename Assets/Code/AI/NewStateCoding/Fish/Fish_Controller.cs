using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;
using UnityEngine.AI;

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
    [HideInInspector]
    public LayerMask targetMask; 

    public FishSC_Abstact stateController;
    public NavMeshAgent agent;

    public IFishState currentState;
    public IFishState previousState;

    public Coroutine running;

    
    public float wanderRadius = 20f;
    public float wanderTimer = 5f;


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

    ///////////////////////////////////////////////////////////////////////// Starting Scripts
    private void OnEnable()
    { // called when enabled to do a bit of set up
        //Debug.Log("Enabled");
        
        ChangeState(stateController.Enter); // at start enter idleing
        stateController.Enabled(this);
    }
}
