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
    [SerializeField][Tooltip("Fishing Rod Game Object")]
    private GameObject fishingRod;

    private List<GameObject> heldItems = new List<GameObject>();
    private int holdingsLength=0;


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
        return index;
    }

    public void ChangeInHand(int index) // change what is being held.
    { 
        EmptyHand();
        if(heldItems[index]!=null)
        {
            heldItems[index].SetActive(true); // if the object is not null.
        }
    } 

    private void EmptyHand() // disable any hand items if they are on.
    {
        foreach(GameObject obj in heldItems)
        { // ? : if item is null do nothing else do the set active thing.
            if (obj != null && obj.activeSelf)
            {
                obj.SetActive(false);
            }
        }
    }
}
