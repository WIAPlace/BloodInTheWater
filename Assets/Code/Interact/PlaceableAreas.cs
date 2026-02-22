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
    private int placeableTypes=3; // nothing, rod, and spear; 
    [SerializeField][Tooltip("Array for Fishing Rod spots")]
    private GameObject[] rodSpots;

    [SerializeField][Tooltip("Array for spear spots")]
    private GameObject[] harpoonSpots;

    private GameObject[][] placeableSpots; 
    // an array of arrays for holding spots. will be used to cycle through enabling them.

    
    void Start()
    {
        placeableSpots = new GameObject[placeableTypes][]; // a the moment 3

        // instantiate the spots
        placeableSpots[0] = null;
        placeableSpots[1] = rodSpots;
        placeableSpots[2] = harpoonSpots;

        TurnOffAllIndicators(); // turn off all indicators cause we dont want them to be there before we press the button
    }

    public void TurnOffAllIndicators() // disable all game objects
    {
        for(int i = 1; i < placeableSpots.Length; i++)
        { // starting i at 1 because 0 is null and doesnt need to be turned off.
            foreach(GameObject spot in placeableSpots[i])
            {
                if (spot != null && spot.activeSelf && spot.CompareTag("PlaceEmpty"))
                { // making sure it exist and is able to be active
                    spot.SetActive(false);
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
        foreach(GameObject spot in placeableSpots[x])
        {
            if (spot != null && !spot.activeSelf)
            { // check to make sure it isnt already active.
                spot.SetActive(true);
            }
        }
    }

}
