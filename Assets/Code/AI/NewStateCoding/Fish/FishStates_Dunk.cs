using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLC.FishStates;

////////////////////////////////////////////////////////////////// In Idle Range
public class Dunk_StateIdle : Abs_StateIdle
{
    
    
    public override void DoEnter(Fish_Controller FSC)
    {   
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        UpdatePosition(FSC);
        return this;
    }
    private void UpdatePosition(Fish_Controller FSC)
    {
        float normalizedTime = FSC.currentLocalOnReel / FSC.reelLength; 

        Vector3 position = FSC.reelSpline.EvaluatePosition(normalizedTime);
        FSC.transform.position = position;
        //Debug.Log(normalizedTime);

        // Get the tangent (forward direction) for rotation
        Vector3 forward = FSC.reelSpline.EvaluateTangent(normalizedTime);
        // Calculate the up vector (adjust as needed for 2D or specific orientations)
        Vector3 up = FSC.reelSpline.EvaluateUpVector(normalizedTime);

        // Set the rotation to align with the spline's direction and up vector
        FSC.transform.rotation = Quaternion.LookRotation(forward, up);
    }
}

////////////////////////////////////////////////////////////////// In Lure Range
public class Dunk_StateLure : Abs_StateLure
{
    public override void DoEnter(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
        return FSC.previousState;
    }
}

////////////////////////////////////////////////////////////////// In Bobber Range
public class Dunk_StateBobber : Abs_StateBobber
{
    public override void DoEnter(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override IFishState DoState(Fish_Controller FSC)
    { 
        return FSC.previousState;
    }
}

////////////////////////////////////////////////////////////////// In Fear Range
public class Dunk_StateFear : Abs_StateFear
{
    public override void DoEnter(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Fish_Controller FSC)
    {
        //throw new System.NotImplementedException();
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return this;
    }
}

////////////////////////////////////////////////////////////////// Unique Behavior
public class Dunk_StateUnique : Abs_StateUnique
{
    Vector3 ramTarget;
    public override void DoEnter(Fish_Controller FSC)
    {
        ramTarget = FSC.SC.GetRamTarget(FSC);
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        
        return this;
    }
}

////////////////////////////////////////////////////////////////// Enter
public class Dunk_StateEnter : Abs_StateEnter
{
    public override void DoEnter(Fish_Controller FSC)
    {
       
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }

    public override IFishState DoState(Fish_Controller FSC)
    {
        return FSC.previousState;
    }
}


public class Dunk_StateHooked : Abs_StateHooked
{
    public override void DoEnter(Fish_Controller FSC)
    {
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }
    public override IFishState DoState(Fish_Controller FSC)
    {
        return FSC.previousState;
    }
}
public class Dunk_StateOnLine : Abs_StateOnLine
{
    public override void DoEnter(Fish_Controller FSC)
    {
        
    }

    public override void DoExit(Fish_Controller FSC)
    {
        
    }
    public override IFishState DoState(Fish_Controller FSC)
    {
        return FSC.previousState;
    }
}