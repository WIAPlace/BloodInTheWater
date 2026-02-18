using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/17/26
/// Purpose: The allocated Time per level.
/// 
/// Edited: 
/// Edited By:
/// Edit Purpose:
/// 
public class TimeKeeper : MonoBehaviour
{
    [SerializeField] [Tooltip("Allocated Time To Fish")]
    float secondsAllocated;

    [SerializeField] [Tooltip("Time Between Each checkign of the timer")]
    float waitInterval=5f; // we are doing it like this because we dont need to have this update every frame.

    private float timePassed=0;
    private float penaltyTime=0;

    void Start()
    {
        StartCoroutine(TickTock());
    }

    IEnumerator TickTock()
    {
        while(timePassed<secondsAllocated){
            yield return new WaitForSeconds(waitInterval);
            timePassed = Time.time + penaltyTime;
            //Debug.Log(timePassed);
        }
    }
    public void AddPenaltyTime(float seconds)
    {
        penaltyTime += seconds;
    }
    public float GetTimeLeft()
    {
        float timeLeft = secondsAllocated-timePassed;
        return timeLeft;
    }
}
