using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/21/26
/// Purpose: This script is a chedker for what the player is currently holding
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class PlayerHolding : MonoBehaviour
{
    [Header("Game Object Refrences")]
    [SerializeField][Tooltip("Fishing Rod Game Object")]
    private GameObject fishingRod;

    [Header("Script Refrences")]
    [SerializeField][Tooltip("Useable Item Refrences. Make sure these are in the same order as the objects they represent. 0 is nothing")]
    private UseableItem_Abstract[] usableItems;

    private List<GameObject> heldItems = new List<GameObject>();
    private int holdingsLength=0;
    private bool checkingActive = false; // used for comunicating between placeAble Spots and See Placeable Areas.


    //private int currentIndex=0; // possibly used for checking what the current index should be at any given time.
    
    
    private void Start()
    {
        // Add in the Objects to the held Items List;
        heldItems.Add(null); // index 0 = nothing
        heldItems.Add(fishingRod); // index 1 = fishing rod;

        holdingsLength = heldItems.Count;
    }

    


    public int CheckInHand() // chedk what the index is in hand
    {
        int index = 0; // 0 signifies null
        for(int i = 0; i < holdingsLength; i++)
        {
            if(heldItems[i] != null && heldItems[i].activeSelf)
            { // if one of them is active it will return that.
                index = i;
            }
        }
        //Debug.Log(index); shows what is active
        return index;
    }

    public void ChangeInHand(int index) // change what is being held.
    { 
        EmptyHand();
        if(heldItems[index]!=null)
        {
            heldItems[index].SetActive(true); // if the object is not null.
            //usableItems[index].enabled = true;
        }
    } 

    private void EmptyHand() // disable any hand items if they are on.
    {
        for(int i = 0; i < holdingsLength; i++)
        { // checking null cause 0 is nothing.
            if(heldItems[i] != null && heldItems[i].activeSelf)
            {
                heldItems[i].SetActive(false);
                //usableItems[i].enabled = false;
            }
        }
    }

    // Communicators between placeable Spot and See Placeable Areas Scritps
    // this is needed because if you press on an active indicator it should disapear if no longer holding down Q 
    public bool CheckIfChecking()
    { // check if Q(/other equivilent) is down
        return checkingActive;
    }
    public void SetChecking(bool checking)
    { // set checking active in see placeable Areas.
        checkingActive = checking;
    }

}
