using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class QuickTimeData_DeepAngel_Reel : QuickTimeData_BasicFish
{
    [SerializeField] private QuickTimeData_Abstract QTD;
    [SerializeField] private float secondsTillLatchOn;


    public QuickTimeData_DeepAngel_Reel(QuickTimeData_BasicFish other) : base(other)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ExitQuickTimeEvent(bool status)
    {
        if (!status) // if the player failed
        {
            FSC.ChangeState(FSC.SC.Fear); // scare the fish off if you loose the mini game
        } 
        else // if player won
        {
            transform.localScale/=normalRandy; // change their scaling back to what the prefab is.
            GameState.Instance.CaughtFish(fishLbs); // send over the caught state to the gamestate thing.
            GameObject fakeFish = FSC.waveHandler.GetFishMesh(); // get the correct mesh at its original position
            GameManager.Instance.qtcPlayer.PlayFakeFish(fakeFish, rot, normalRandy); // pretend to catch the fish

            GameManager.Instance.unlocks.SaveFishData(key,fishLbs); // send the fish data to unlocks so it can be saved as a player pref

            // set it to inactive
            FSC.gameObject.SetActive(false);
        }
    }
    IEnumerator Latch()
    {   // after caught latch on and play the much more dangerous game.
        yield return new WaitForSeconds(secondsTillLatchOn);
        QTD.SendData();
    }
}
