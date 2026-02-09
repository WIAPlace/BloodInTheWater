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

    private enum FishState
    { // helps in knowing what state the fish ought to be in.
        Idle, // Just hanging out doing its thing
        LureNav // On the move going after the lure.
    }

    private FishState currentState = FishState.Idle; // starts off in idle.

    void Start()
    {
        rodNav = GetComponent<FishNavToRod>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    { // when the fish enters the lures trigerzone       
        if (((1 << other.gameObject.layer) & lureMask.value) != 0)
        { // if the trigger is the bobber's lure layermask
            LureNavState(); // keeping this seprate cause i expect i might use it outside of this. wes 2/8
        }
    }

    private void LureNavState() // State in which the fish will navigate twoards the lure
    {
        if (target != null)
        {
            currentState = FishState.LureNav;
            rodNav.InLureZone(target); // activates the FishNavToRod script that sets the agents destination
        }
    }
    
    public void LureReeledIn()
    {
        if(currentState == FishState.LureNav) // making sure this is executing only on the ones who need it
        { 
            currentState = FishState.Idle; // change the state back to idle;
            rodNav.LureReeledIn(); // force nav agent to turn of for the moment.
        }
    }
}
