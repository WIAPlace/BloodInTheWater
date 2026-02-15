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
    [SerializeField] [Tooltip("Quick Time User Interface Prefab")]
    GameObject qtUI;

    public Image redArea;
    private Image hitZone;
    private Image playerPoint;

    void OnEnable()
    { // the odd way of instantiateing these when its in a prefab.
        Transform redAreaT = qtUI.transform.Find("RedArea");
        Transform hitZoneT = qtUI.transform.Find("HitZone");
        Transform playerPointT = qtUI.transform.Find("PlayerPoint");
        //Debug.Log(redArea);
        redArea = redAreaT.GetComponent<Image>();
        hitZone = hitZoneT.GetComponent<Image>();
        playerPoint = playerPointT.GetComponent<Image>();
    }
    public void InstanceUI()
    {
        Instantiate(qtUI);
    }

    public void SetRedArea(float max)
    {
        SetBarLength(0,max,redArea);
    }

    // set the are of the hit zone then move it to be between the min and max.
    public void SetHitZone(float min, float max)
    {
        SetBarLength(min,max,hitZone);
        float origin = (min+max)/2;
        hitZone.transform.position = new Vector3(origin,hitZone.transform.position.y,hitZone.transform.position.z);
    }


    private void SetBarLength(float min, float max, Image bar)
    {
        float length = max-min; // length
        //RectTransform rtform = bar.rectTransform; // make this am instance thing for eiser editing.
        bar.rectTransform.sizeDelta = new Vector2(length,bar.rectTransform.sizeDelta.y); // change size of width only
    }



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
