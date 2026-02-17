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


    public void SetPlayerPoint(float position, Image playerPoint, Image maxBar)
    {
        playerPoint.rectTransform.anchoredPosition = maxBar.rectTransform.anchoredPosition + Vector2.right*position;
    }

    public void SetRedArea(float max, Image redBar)
    {
        SetBarLength(0,max,redBar);
        redBar.rectTransform.anchoredPosition = Vector2.left*(max/2) + Vector2.down*Screen.height/4;
    }

    // set the are of the hit zone then move it to be between the min and max.
    public void SetHitZone(float min, float max, Image hitBar, Image maxBar)
    {
        SetBarLength(min,max,hitBar);
        float origin = (min+max)/2;
        hitBar.rectTransform.anchoredPosition = maxBar.rectTransform.anchoredPosition + Vector2.right*min;
    }

    private void SetBarLength(float min, float max, Image bar)
    {
        float length = max-min; // length
        //RectTransform rtform = bar.rectTransform; // make this am instance thing for eiser editing.
        bar.rectTransform.sizeDelta = new Vector2(length,bar.rectTransform.sizeDelta.y); // change size of width only
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
