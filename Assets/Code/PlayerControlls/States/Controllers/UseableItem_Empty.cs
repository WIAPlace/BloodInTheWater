using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/27/26
/// Purpose: Empty hand ItemState;
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class UseableItem_Empty : UseableItem_Abstract
{
    private void Start()
    {
        Idle = new Empty_StateItemIdle();
        Readying = new Empty_StateItemReadying();
        IsReady = new Empty_StateItemIsReady();
        UseItem = new Empty_StateItemUse();
        Place = new Empty_StateItemPlace();
    }

    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentState.DoState(controller);
    }
}
