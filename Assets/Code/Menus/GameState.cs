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
    public static GameState Instance {get;set;}

    [SerializeField][Tooltip("How many fish to catch till win")]
    private float totalFishCount=3f;
    private float fishCaught=0f;
    [SerializeField][Tooltip("WinScreen")]
    private GameObject winScreen;
    [SerializeField][Tooltip("LooseScreen")]
    private GameObject looseScreen;

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
    private void Start()
    { // we want this to start out as 0
        fishCaught = 0f;
        winScreen.SetActive(false);
        looseScreen.SetActive(false);
    }

    public void CaughtAFish(float weight)
    {
        fishCaught += weight; // fish based on the pounds of fish
    }

    public void CheckWin()
    {
        if(fishCaught >= totalFishCount) // if they are return true if not false
        {
            winScreen.SetActive(true);
        }
    }
    public void LooseState()
    {
        looseScreen.SetActive(true);
    }
    public void ChangeTime(float time)
    {
        if (timeText != null)
        {
            int Tim = (int)time;
            // Change the text value
            timeText.text = "Seconds Left: " + Tim.ToString();
        }
    }



}
