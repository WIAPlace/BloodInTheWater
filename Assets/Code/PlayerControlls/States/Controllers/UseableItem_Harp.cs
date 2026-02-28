using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/26/26
/// Purpose: The Harpoon Usable Item
///  
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class UseableItem_Harp : UseableItem_Abstract
{
    private void Start()
    {
        Idle = new Harp_StateItemIdle();
        Readying = new Harp_StateItemReadying();
        IsReady = new Harp_StateItemIsReady();
        UseItem = new Harp_StateItemUse();
        Place = new Harp_StateItemPlace();
    }
    
    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentState.DoState(controller);
    }
}
