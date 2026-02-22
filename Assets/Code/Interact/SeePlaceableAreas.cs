using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/22/26
/// Purpose: This will allow the player to see where they can place the thing in their hand.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class SeePlaceableAreas : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input;
    [SerializeField] [Tooltip("PlayerHolding Script")]
    private PlayerHolding holdings;
    [SerializeField] [Tooltip("PlaceableArea Script")]
    private PlaceableAreas areas;



    private void Start()
    {
        input.PlaceEvent += HandlePlace;
        input.PlaceCancelledEvent += HandlePlaceCancelled;
    }
    private void OnDestroy()
    {
        input.PlaceEvent -= HandlePlace;
        input.PlaceCancelledEvent -= HandlePlaceCancelled;
    }




    private void HandlePlace()
    {// on holding down place button allow them to see
        //Debug.Log("Showing Areas");
        int currentHolding = 0; // check what the player is currently holding.
        currentHolding = holdings.CheckInHand(); 

        areas.TurnOffAllIndicators(); // just in case
        areas.TurnOnIndicator(currentHolding); // turns on the indicators related to what is currently being held

        holdings.SetChecking(true); // communication for placeable spots
    }
    private void HandlePlaceCancelled()
    {// on letting go of place button turn off ability to see.
        areas.TurnOffAllIndicators(); // we only want to see indicators if the button is down.
        holdings.SetChecking(false); // communication for placeable spots
    }
}
