using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private float totalLbsCount=3f;
    private float lbsCaught=0f;

    public TMP_Text timeText;


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
    
}
