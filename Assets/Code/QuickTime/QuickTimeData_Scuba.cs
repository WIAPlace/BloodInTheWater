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
        qtcPlayer.SetData(this); // send the data of this mans
    }

    public override  void OnHit()
    {
        //controller.SetCurrentState(controller.HitState);
        controller.MonsterHit(controller.transform.forward - Vector3.up/2);
    }
    public override void OnMiss()
    {
        //Debug.Log("Hit");
        GameState.Instance.LooseState();
    }
    
}
