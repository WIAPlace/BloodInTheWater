/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Abstract for all UseItem States
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class UsableItem_Rod : UseableItem_Abstract
{
    private void OnEnable()
    {
        UseItem = new Rod_StateUseItem();
    }
    public override IUsableState DoState(Usables_Controller controller)
    {
        return controller.currentState.DoState(controller);
    }
}
