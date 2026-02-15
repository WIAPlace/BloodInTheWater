using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QTE_Basic")]
public class QuickTimeEvent_Basic : ScriptableObject
{
    // Checks if the the player pointer is in the ranger
    public bool CheckInRange(float min, float max, float value)
    { 
        if(value>=min && value <= max) // if the value is between the max and min, its true
        {
            return true;
        }
        else // else it is false
        {
            return false;
        }
    }
}
