using UnityEngine;
using UnityEngine.UI;
/// 
/// Author: Weston Tollette
/// Created: 2/14/26
/// Purpose: Scriptable object for basic quick time event
/// 
/// Edited: 
/// Edited By:
/// Edit Purpose:
/// 
[CreateAssetMenu(menuName = "QTE_Basic")]
public class QuickTimeEvent_Basic : ScriptableObject
{
    // will need to figure out how to keep this working with screen size.


    public void SetPlayerPoint(float position, Image playerPoint) 
    { // player point is now the fish's marker on the radial
        Vector3 rot = new Vector3(0,0,position);
        playerPoint.transform.rotation = Quaternion.Euler(rot);
    }

    // set the are of the hit zone then move it to be between the min and max.
    public void SetHitZone(float length, float origin, Image hitBar)
    {
        hitBar.fillAmount = length/360; // set size of the bar to be between 0-1 on a scale of 360;
        Vector3 rot = new Vector3(0,0,origin+length);
        hitBar.transform.rotation = Quaternion.Euler(rot);
    }


    /* // i think it be more usefull to have this in the main code so that we dont have to return false.
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
    */
}
