using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/12/26
/// Purpose: Basic 1 zone hit thing.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class QuickTimeData_BasicFish : QuickTimeData_Abstract
{
    //[HideInInspector]
    public Fish_Controller FSC;
    private float fishLength;
    protected float fishLbs;
    [SerializeField] [Tooltip("When caught how much should they rotate to show their good side")]
    protected int rot;
    [SerializeField][Tooltip("Fish lbs min")]
    private float minLbs=5;
    [SerializeField][Tooltip("Fish lbs max")]
    private float maxLbs=15;

    [SerializeField][Tooltip("Fish lbs min")]
    private float minMeshSize=.5f;
    [SerializeField][Tooltip("Fish lbs max")]
    private float maxMeshSize=1.5f;
    [SerializeField][Tooltip("Refrence the unlocks Fish array")]
    protected int key;
    
    protected float normalRandy;
    public QuickTimeData_BasicFish(QuickTimeData_BasicFish other) : base(other)
    {
        this.fishLength = other.fishLength; 
        this.fishLbs = other.fishLbs;
    }

    void Start()
    {   // needed to activate stuff within the fish itself
        FSC = GetComponent<Fish_Controller>();
    }

    private void OnEnable()
    {
        
        normalRandy = Random.Range(0f,1f)+Random.Range(0f,1f);
        fishLbs = normalRandy *.5f *(maxLbs-minLbs)+minLbs; // trigangle distrubutin
        //Debug.Log(fishLbs);
        normalRandy = Mathf.Lerp(minMeshSize,maxMeshSize,normalRandy/2);
        
        transform.localScale*=normalRandy; // change their scaling relatively
        //Debug.Log(normalRandy);
    }

    public override void SendData() // sends the data to the Rod scipt.
    {
        base.SendData();
    }

    /// Hit ////////////////////////////////////////////////////
    public override  void OnHit()
    {   // change the active state and declare that fish have been caught
        
        //FSC.MoveAlongSpline(50f);
        FSC.ReelRate = winRate;
        
        //gameObject.SetActive(false);
       
    }
    /// Miss ////////////////////////////////////////////////////
    public override void OnMiss()
    {
        //FSC.MoveAlongSpline(50f);
        FSC.ReelRate = -looseRate;
    }
    
    /// Move ////////////////////////////////////////////////////
    public override float GetQTMove(float location)
    {
        return base.GetQTMove(location);
    }


    public override void EnterQTEvent()
    {
        FSC.waveHandler.SetOnWaves(false);
        FSC.reelLength = 0;
       
        
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

            GameManager.Instance.PresentFish(key,fishLbs,type); // set fish stat screen
            GameManager.Instance.unlocks.SaveFishData(key,fishLbs); // send the fish data to unlocks so it can be saved as a player pref
            

            // set it to inactive
            FSC.gameObject.SetActive(false);
        }
    }

    public override void QTStatus(float amnt)
    {   //move along the spines
        FSC.MoveAlongSpline(amnt);
    }
}
