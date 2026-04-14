using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(Latch());    
            // set it to inactive
            //FSC.gameObject.SetActive(false);
            FSC.agent.isStopped = true;
            FSC.waveHandler.UseableMesh.gameObject.SetActive(false);
        }
    }
    IEnumerator Latch()
    {   // after caught latch on and play the much more dangerous game.
        yield return new WaitForSeconds(secondsTillLatchOn);
        QTD.SendData();
        
    }
}
