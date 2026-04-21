using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private bool limitTime = true;
    [SerializeField] [Tooltip("Allocated Time To Fish")]
    float secondsAllocated;

    [SerializeField] [Tooltip("Time Between Each checkign of the timer")]
    float waitInterval=5f; // we are doing it like this because we dont need to have this update every frame.
    [SerializeField] FlickerLights flickerLights;
    [SerializeField,Tooltip("lights will start flickering at: secondsAllocated - secondsAllocated/flickerPercent")]
    private float flickerPercent = 0;

    private float sceneTime = 0;
    private float timePassed=0;
    private float penaltyTime=0;
    

    void Start()
    {
        //GameState.Instance.ChangeTime(GetTimeLeft());
        if (limitTime)
        {
            StartCoroutine(TickTock());
            //GameManager.BoatHit += AddPenaltyTime;
            
        }
        GameManager.BoatHit += AddPenaltyTime;
    }
    void OnDestroy()
    {
        GameManager.BoatHit -= AddPenaltyTime;
    }
    void Update()
    {
        sceneTime += Time.deltaTime;
        
    }

    IEnumerator TickTock()
    {
        bool lightChecked = false;
        float flickerSec = secondsAllocated - secondsAllocated/flickerPercent;
        while(timePassed<secondsAllocated){
            yield return new WaitForSeconds(waitInterval);
            timePassed = sceneTime + penaltyTime;
            if(timePassed > flickerSec && !lightChecked)
            {
                lightChecked=true;
                flickerLights.BeginFlickering();
            }
            //GameState.Instance.ChangeTime(GetTimeLeft());
            //Debug.Log(timePassed);
        }
       GameState.Instance.LooseState();
    }

    public void AddPenaltyTime(float seconds)
    {
        penaltyTime += seconds;
        //Debug.Log(penaltyTime);
    }

    public string GetTimeLeft()
    {
        float timeLeft = secondsAllocated-timePassed;
        int minutes = (int)(timeLeft/60); 
        int seconds = (int)(timeLeft%60);
        
        string sec = "";
        if(seconds < 10)
        {
            sec = "0";
        }
        sec += seconds;

        string min = "";
        if (minutes < 10)
        {
            min="0";
        }
        min+=minutes;
        
       string timHasLeft = min + " : " + sec;

        return timHasLeft;
    }
    public float GetMaxTime()
    {
        return secondsAllocated;
    }
    public float GetCurrentTime()
    {
        return secondsAllocated-timePassed;
    }
}
