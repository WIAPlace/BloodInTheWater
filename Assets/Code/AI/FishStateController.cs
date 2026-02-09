using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/8/26
/// Purpose: Script to swich between behaviors of fish ai
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class FishStateController : MonoBehaviour
{
    [SerializeField] [Tooltip("The target to move twoards")]
    private Transform target; // the bobber most likeley.

    [SerializeField][Tooltip("LayerMask for Lure")]
    private LayerMask lureMask;

    private FishNavToRod rodNav; // Script for navigation to rod.
    // we could just add it via script if it this becomes a hassle. 
    // though that could likely change if the script gets more complicated.
    // so this will probably work better as a prefab.

    private FishIdleMove idle; // used for fish just wandering around
    private ScaredFish scare; // used for the spooked state of fish.

    private Collider otherHolder; // will be used to store other in on trigger enter if the scared fish is activated.

    private enum FishState
    { // helps in knowing what state the fish ought to be in.
        Idle, // Just hanging out doing its thing
        LureNav, // On the move going after the lure.
        Spooked
    }

    private FishState currentState = FishState.Idle; // starts off in idle.

    void Start()
    {
        rodNav = GetComponent<FishNavToRod>();
        idle = GetComponent<FishIdleMove>();
        scare = GetComponent<ScaredFish>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    { // when the fish enters the lures trigerzone       
        if (((1 << other.gameObject.layer) & lureMask.value) != 0)
        { // if the trigger is the bobber's lure layermask
            if(currentState!= FishState.Spooked)
            {
                LureNavState(); // keeping this seprate cause i expect i might use it outside of this. wes 2/8
            }
            else // if current state is = spooked
            {
                otherHolder = other;
                //Debug.Log("Entered");
            }
        }
    }
    void OnTriggerExit(Collider other)
    { // basicly only used to see if they should continue going after the bobber or if they have left the area by now.
        if (currentState == FishState.Spooked && ((1 << other.gameObject.layer) & lureMask.value) != 0)
        {
            otherHolder = null; // set this to nothing if they leave the holder.
            //Debug.Log("Exited");
        }
    }

    private void LureNavState() // State in which the fish will navigate twoards the lure
    {
        if (target != null)
        {
            if(currentState == FishState.Idle) // turns off idle.
            {
                idle.IdleStateInactive();
            }
            currentState = FishState.LureNav;
            rodNav.InLureZone(target); // activates the FishNavToRod script that sets the agents destination
        }
    }
    
    public void LureReeledIn()
    {
        otherHolder = null; // on reeled in otherholder will be emptied.
        if(currentState == FishState.LureNav) // making sure this is executing only on the ones who need it
        { 
            rodNav.LureReeledIn(); // force nav agent to turn of for the moment.
        }
        currentState = FishState.Idle; // change the state back to idle;
        idle.IdleStateActive();
        
    }

    public void BobberSpooked(Vector3 lurePosition)
    {
        if(currentState == FishState.Idle) // turns off idle.
        {
            idle.IdleStateInactive();
        }
        currentState = FishState.Spooked;
        StartCoroutine(scare.FearTheBobber(lurePosition));
    }
    public void EndSpook()
    {
        if(otherHolder != null)
        { // send it to after the lure if its still around.
            LureNavState();
        }
        else
        {
            LureReeledIn(); // lure is gone or the fish has left its range, either way forget about him.
        }
    }
}
