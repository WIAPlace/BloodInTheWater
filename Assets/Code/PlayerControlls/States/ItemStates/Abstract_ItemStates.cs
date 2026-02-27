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

    private Coroutine running; // if not null the corutine is running


    public virtual IUseableState DoState(Useable_Controller controller)
    { // much like idle this is a buffer state.
        return this; // will return the concrete version of this class
    }

    public virtual void DoEnter(Useable_Controller controller)
    { // on enter play this to start the corutine to be ready
        running = controller.StartCoroutine(PrepReady(controller));
    }

    public virtual void DoExit(Useable_Controller controller)
    { // if the corutine is happening stop it.
        if(running != null)
        {
            controller.StopCoroutine(running);
            running = null;
        }
    }

    public virtual IEnumerator PrepReady(Useable_Controller controller)
    { // wait a number of seconds till the item is ready
        yield return new WaitForSeconds(controller.readyTime);
        controller.ChangeState(controller.currentItem.IsReady);
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