using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/23/26
/// Purpose: WinStateThing
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class GameState : MonoBehaviour
{
    public static GameState Instance {get;set;} // singleton

    [SerializeField][Tooltip("How many fish to catch till win")]
    private float totalLbs = 20f;
    private float lbsCaught=0f;
    private float lbsPercent;
    [SerializeField] private GameOver endLoose;
    [SerializeField] private GameObject keeperToSpawn;
    private int visitors = 0;
    /*
    [SerializeField,Tooltip("Events will occur when the element in the array has reached its indexed range (percentage based)"),Range(0,100)]
    private int[] weightEventPercentages;
    */
    public static event Action<float> OnCatch;
    public static event Action<float> WeightEvent; 

    private void Awake()
    { // makes sure this is the only instance of this system.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        if(keeperToSpawn!=null){
            keeperToSpawn.SetActive(false);
        }
        visitors = 0;
    }
    

    public bool CheckIfEnoughCaught()
    {
        return lbsCaught >= totalLbs;
    }

    // setter
    public void CaughtFish(float lbs)
    {
        lbsCaught += Mathf.Round(lbs * 10)/10; // 1 decimal
        lbsPercent = lbsCaught/totalLbs * 100;
        OnCatch?.Invoke(.6f);
        WeightEvent?.Invoke(lbsPercent); // if subscriber is attached to this and weight is met it will go off.
    }

    public void CheckCaughtFish()
    {
        OnCatch?.Invoke(0f); // run this without updating fish to just show the thing
    }

    /// getters
    public float CheckLbs()
    {
        if(lbsCaught >= totalLbs)
        {
            if(keeperToSpawn!=null)
            {
                keeperToSpawn.SetActive(true);
            }
        }
        return lbsCaught;
    }
    public float CheckLbsNeeded()
    {
        return totalLbs;
    }
    
    public void LooseState(string scene)
    {
        Time.timeScale=1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endLoose.GameEnd(scene);
    }

    public void OnBoard(bool isOn)
    {   
        if (isOn)
        {
            visitors +=1;
        }
        else visitors -= 1;

        if(visitors < 0) visitors = 0;
    }
    public bool CheckOnBoard()
    {   // if there is something on board return true;
        if(visitors > 0)
        {
            return false;
        }
        else return true;
    }
}
