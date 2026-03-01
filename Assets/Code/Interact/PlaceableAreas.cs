using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/21/26
/// Purpose: This script is for making areas where you can place things visible
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class PlaceableAreas : MonoBehaviour
{
    [SerializeField][Tooltip("Persistant Item Spot Scriptable Object")]
    private PersistantItemSpot perSpot;

    private int placeableTypes=3; // nothing, rod, and spear; 
    [SerializeField][Tooltip("Array for Fishing Rod spots")]
    private PlaceableSpot[] rodSpots;

    [SerializeField][Tooltip("Array for spear spots")]
    private PlaceableSpot[] harpoonSpots;

    private PlaceableSpot[][] placeableSpots; 
    // an array of arrays for holding spots. will be used to cycle through enabling them. 

    
    void Start()
    {
        placeableSpots = new PlaceableSpot[placeableTypes][]; // a the moment 3

        // instantiate the spots
        placeableSpots[0] = null;
        placeableSpots[1] = rodSpots;
        placeableSpots[2] = harpoonSpots;

        SetPersistantOn(); // set certain ones as the persistants
        TurnOffAllIndicators(); // turn off all indicators cause we dont want them to be there before we press the button
    }

    private void SetPersistantOn() // at start put them here
    {
        for(int i = 1; i < perSpot.spots.Length; i++)
        { // start at one cause hands dont matter
            if(perSpot.spots[i] >= placeableSpots.Length) // Out of bounds catch
            {
                perSpot.spots[i] = 0;
            }

            if (perSpot.spots[i] != -1) // -1 being in hand
            { // if the item is not in hand turn it on
                placeableSpots[i][perSpot.spots[i]].SetPlaced(true);
            }
            else if(perSpot.spots[i] == -1)
            { // place items of index -1 into the player's hand.
                PlayerHolding.Instance.ChangeInHand(i);
                //Debug.Log(perSpot.spots[i]);
            }
        }
    }

    public void TurnOffAllIndicators() // disable all game objects
    {
        for(int i = 1; i < placeableSpots.Length; i++)
        { // starting i at 1 because 0 is null and doesnt need to be turned off.
            perSpot.spots[i] = -1;
            for(int x = 0; x < placeableSpots[i].Length; x++)
            {
                if (placeableSpots[i][x] != null && placeableSpots[i][x].gameObject.activeSelf && placeableSpots[i][x].gameObject.CompareTag("PlaceEmpty"))
                { // making sure it exist and is able to be active
                    //Debug.Log("deactivated: " + x );
                    placeableSpots[i][x].gameObject.SetActive(false);
                }
                else if (placeableSpots[i][x] != null && placeableSpots[i][x].gameObject.activeSelf && placeableSpots[i][x].gameObject.CompareTag("PlaceFull"))
                {
                    //Debug.Log("activated: " + x );
                    perSpot.spots[i] = x;
                }
            }
        }
    }

    public void TurnOnIndicator(int x) // turn on certain spots
    {
        if (x <= 0 || x >= placeableTypes) // if x is 0 or out of bounds it will not be considered
        {
            //Debug.Log("Out of Range:" + x);
            return;
        }
        foreach(PlaceableSpot spot in placeableSpots[x])
        {
            if (spot != null && !spot.gameObject.activeSelf)
            { // check to make sure it isnt already active.
                spot.gameObject.SetActive(true);
            }
        }
    }

}
