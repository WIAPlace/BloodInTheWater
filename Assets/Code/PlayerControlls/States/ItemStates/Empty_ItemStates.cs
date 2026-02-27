using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/27/26
/// Purpose: Holder for Empty Item States
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///

////////////////////////////////////////////////////// Idle /////////////////////////////////////////////
public class Empty_StateItemIdle : Abs_StateItemIdle
{
    public override void DoEnter(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }
}

////////////////////////////////////////////////////// Readying /////////////////////////////////////////////
public class Empty_StateItemReadying : Abs_StateItemReadying
{
    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentItem.Idle;
    }
}

////////////////////////////////////////////////////// Is Ready //////////////////////////////////////////////
public class Empty_StateItemIsReady : Abs_StateItemIsReady
{
    public override void DoEnter(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }
    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentItem.Idle;
    }
}

////////////////////////////////////////////////////// Use Item /////////////////////////////////////////////
public class Empty_StateItemUse : Abs_StateItemUse
{
    public override void DoEnter(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }

    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentItem.Idle;
    }
}

////////////////////////////////////////////////////// Place Item /////////////////////////////////////////////
public class Empty_StateItemPlace : Abs_StateItemPlace // place is pickup for an empty hand.
{
    public override void DoEnter(Useable_Controller controller)
    {
        // do some kind of animation to pick up
        base.DoEnter(controller);
        controller.currentItem.useableMesh.SetActive(true); // turn on the game object 
    }

    public override void DoExit(Useable_Controller controller)
    {
        base.DoExit(controller);
        
    }
}