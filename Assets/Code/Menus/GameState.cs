using System.Collections;
using System.Collections.Generic;
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
    }

    public bool CheckIfEnoughCaught()
    {
        return lbsCaught >= totalLbs;
    }

    // setter
    public void CaughtFish(float lbs)
    {
        lbsCaught += Mathf.Round(lbs * 10)/10; // 1 decimal
    }

    /// getters
    public float CheckLbs()
    {
        return lbsCaught;
    }
    public float CheckLbsNeeded()
    {
        return totalLbs;
    }
    
}
