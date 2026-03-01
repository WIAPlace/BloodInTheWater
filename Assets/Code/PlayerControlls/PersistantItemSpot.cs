using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/28/26
/// Purpose: holder for where the items will be placed, to be persistatnt accross scenes.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
[CreateAssetMenu]
public class PersistantItemSpot : ScriptableObject
{

    // if either of these are negative on they will be in the player's hand
    [Tooltip("-1 is player holding 0-x is the index of item in placeableAreas. Element 1 = rod, 2 = harpoon, dont worry about 0.")]
    public int[] spots = {0,0,0}; 
    
}
