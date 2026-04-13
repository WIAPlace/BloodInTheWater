using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class FishSC_PillarSquid : FishSC_Abstact
{
    [SerializeField] private Transform orientation;
    [SerializeField] private LayerMask BoatMask;
    [SerializeField] [Tooltip("Sound of hitting the boat")]
    private SoundEffectSO BoatHit;
    public AudioSource soundMaker;
    [SerializeField]
    CinemachineImpulseSource impSour;

    [SerializeField] private float damage;

    public void Awake()
    {
        orientation.rotation = Quaternion.Euler(Vector3.zero);
        target = GameManager.Instance.lureTarget;
        fishData = GetComponent<QuickTimeData_Abstract>();
        FSC = GetComponent<Fish_Controller>();
        Idle = new PS_StateIdle();
        Lure = new PS_StateLure();
        Bobber = new PS_StateBobber();
        Fear = new PS_StateFear();
        Unique = new PS_StateUnique();
        Enter = new PS_StateEnter();
        Hook = new PS_StateHooked();
        Line = new PS_StateOnLine();
    }

    public override void BobberSpooked(Vector3 lurePosition)
    {
        //throw new System.NotImplementedException();
    }

    public override void IdleMovement(Fish_Controller FSC)
    {
        
    }
    

    public override void LureReeledIn()
    {
        if(FSC.currentState != FSC.SC.Idle)
        {   // if current state is not idle make it idle.
            FSC.ChangeState(FSC.SC.Idle); 
        }
    }

    public override IFishState MoveBackToIdle(Fish_Controller FSC)
    {
        return FSC.SC.Idle;
        //throw new System.NotImplementedException();
    }

    public override void Collision(Collision collision)
    {
        if(((1 << collision.gameObject.layer) & BoatMask.value) != 0 && FSC.currentState == FSC.SC.Bobber)
        { // bobber state is when it is jetting twoards thing.
            GameManager.Instance.OnBoatHit(damage);
            BoatHit.Play(soundMaker);
            impSour.GenerateImpulse();
            FSC.ChangeState(Idle);
        }
        else if(((1 << collision.gameObject.layer) & BoatMask.value) != 0 && FSC.currentState == FSC.SC.Unique)
        {
            // maybe some noice for like a little bonk.
            FSC.ChangeState(Idle);
        }
    }
}
