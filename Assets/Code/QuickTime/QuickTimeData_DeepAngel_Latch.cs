using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
// Deep Angel Latched on to the Face.
public class QuickTimeData_DeepAngel_Latch : QuickTimeData_Abstract
{
    public QuickTimeData_DeepAngel_Latch(QuickTimeData_Abstract other) : base(other)
    {
        
    }

    public override void EnterQTEvent()
    {
        GameManager.Instance.OnLatchActive(true); // begin lattching onto the face
    }

    public override void ExitQuickTimeEvent(bool status)
    {
        GameManager.Instance.OnLatchActive(false); // end latch onto face
        if (status) // win
        {
            
        }
        else  // lose
        {
            
        }
    }

    public override GameObject GetLookLocation()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHit()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnMiss()
    {
        //throw new System.NotImplementedException();
    }

    public override void QTStatus(float amnt)
    {
        //throw new System.NotImplementedException();
    }
}
