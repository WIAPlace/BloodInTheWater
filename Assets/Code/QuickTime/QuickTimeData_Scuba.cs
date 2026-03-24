using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeData_Scuba : QuickTimeData_Abstract
{
    private Scuba_Controller controller;
    void Start()
    {
        controller = GetComponent<Scuba_Controller>();
    }
    public QuickTimeData_Scuba(QuickTimeData_Abstract other) : base(other)
    {
    }
    public override void SendData()
    {
        base.SendData();
    }

    public override  void OnHit()
    {
        //controller.SetCurrentState(controller.HitState);
        
    }
    public override void OnMiss()
    {
        //Debug.Log("Hit");
        //GameState.Instance.LooseState();
    }

    public override void EnterQTEvent()
    {
        //throw new System.NotImplementedException();
    }

    public override void ExitQuickTimeEvent(bool status)
    {
        if (!status)
        {
            GameState.Instance.LooseState();
        }
        else
        {
            controller.MonsterHit(controller.transform.forward - Vector3.up/2);
        }
    }

    public override void QTStatus(float amnt)
    {
        //throw new System.NotImplementedException();
    }
}
