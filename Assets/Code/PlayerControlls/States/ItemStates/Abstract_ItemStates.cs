using System.Collections;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/26/26
/// Purpose: Abstract for all item states
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///

////////////////////////////////////////////////////// Idle ////////////////////////////////////////////
public abstract class Abs_StateItemIdle : IUseableState
{
    public abstract void DoEnter(Useable_Controller controller);

    public abstract void DoExit(Useable_Controller controller);

    public virtual IUseableState DoState(Useable_Controller controller)
    { // idle is a buffer so it can just return idle
        return this; // will return an instance of the concrete class / x Idlestate
    }
}

////////////////////////////////////////////////////// Is Ready ////////////////////////////////////////////
public abstract class Abs_StateItemIsReady : IUseableState
{
    public abstract void DoEnter(Useable_Controller controller);

    public abstract void DoExit(Useable_Controller controller);

    public virtual IUseableState DoState(Useable_Controller controller)
    { // much like idle this is a buffer state.
        return this; // will return the concrete version of this class
    }
}

////////////////////////////////////////////////////// Readying /////////////////////////////////////////////
public abstract class Abs_StateItemReadying : IUseableState
{ // a simple wait before you can use the item, a QOL so that clicking doesnt imediatly fire of the item. 

    public virtual IUseableState DoState(Useable_Controller controller)
    { // much like idle this is a buffer state.
        return this; // will return the concrete version of this class
    }

    public virtual void DoEnter(Useable_Controller controller)
    { // on enter play this to start the corutine to be ready
        controller.StartWaitToChange(controller.currentItem.IsReady, controller.readyTime);
    }

    public virtual void DoExit(Useable_Controller controller)
    { // if the corutine is happening stop it.
        controller.StopRunning(); 
    }
}

////////////////////////////////////////////////////// Use Item /////////////////////////////////////////////
public abstract class Abs_StateItemUse : IUseableState
{
    protected UseableItem_Abstract useable;
    public abstract void DoEnter(Useable_Controller controller);

    public abstract void DoExit(Useable_Controller controller);

    abstract public IUseableState DoState(Useable_Controller controller); // will be implemented in the States themselves.
}

////////////////////////////////////////////////////// Place Item /////////////////////////////////////////////
public abstract class Abs_StateItemPlace : IUseableState
{
    public virtual void DoEnter(Useable_Controller controller)
    {
        // start animation to place or pickup something
        controller.DisableControlls(); // turn the controlls off 
        controller.ChangeItem(controller.currentItemIndex); // changes current item to whatever player holding changed it to
        controller.StartWaitToChange(controller.currentItem.Idle, controller.placeTime);

    }

    public virtual void DoExit(Useable_Controller controller)
    {
        controller.StopRunning(); // stop running the corutine if its playing
        controller.EnableControlls(); // turn the controlls back on
    }

    public virtual IUseableState DoState(Useable_Controller controller)
    {
        return this; // just keep letting them know were in place.
    }
}