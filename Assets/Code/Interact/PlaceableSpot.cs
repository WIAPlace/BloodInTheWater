using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/22/26
/// Purpose: Script for the spots that hold the items themselves.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class PlaceableSpot : MonoBehaviour, IInteractable
{
    [SerializeField][Tooltip("Refrence To Player Holding Script")]
    private PlayerHolding holding;

    [SerializeField][Tooltip("Filled Spot")]
    private GameObject filledSpot;
    [SerializeField][Tooltip("Empty Spot")]
    private GameObject emptySpot;

    [SerializeField][Tooltip("Place in the Placeable Area typeIndex this is. Must be Greater than 0")]
    private int typeIndex = 1;

    [SerializeField][Tooltip("Bool to check if this item is placed (Make sure there is only ever one placed)")]
    private bool itemPlaced = false; // if the item is located here.

    void OnValidate()
    { // value of this can not be less than or equal zero because zero is nothing and less is out of range 
        if (typeIndex <= 0)
        {
            Debug.LogWarning("Index must be greater than 0! Clamping to 1.");
            typeIndex = 1; 
        }
    }
    

    private void Start()
    {
        // fire these two off just to make sure its all going as planned;
        OffPlaced();
        SetPlacedTag();
    }

    private void SetPlaced(bool isPlaced) // setter for if the item has been placed.
    {
        itemPlaced = isPlaced;
        OffPlaced(); // changes either on or off based on bool of if it is or isnt
        SetPlacedTag(); // changes tag
        ChangeHoldingState(); // changes the state of whats in your hand
    }

    // Scripts for changing acording to their status of itemPlaced.
    private void OffPlaced()
    { // weather the item is place will determin if it is on or off
        filledSpot.SetActive(itemPlaced);
        emptySpot.SetActive(!itemPlaced);
    } 
    private void SetPlacedTag() // change the tag to what it should be
    { // used in place of get component.
        if (!itemPlaced)
        { // tag empty
            gameObject.tag = "PlaceEmpty";
        }
        else
        { // tag full
            gameObject.tag = "PlaceFull";
        }
    }

    private void ChangeHoldingState()
    {// change if the player is holding a certain object
        int indexHolder = 0;
        if (!itemPlaced)
        {
            indexHolder = typeIndex;
        }
        holding.ChangeInHand(indexHolder);
    }

    // interface implementation
    public void Interact()
    {
        if(holding.CheckInHand() == typeIndex || holding.CheckInHand() == 0)
        {
            SetPlaced(!itemPlaced);

            if (!holding.CheckIfChecking())
            { //if we arnt checking dont keep this game object around
                gameObject.SetActive(false);
            }
        }
    }
}
