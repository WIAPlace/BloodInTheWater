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
    private void OnEnable()
    {
        UseItem = new Harp_StateUseItem();
    }
    public override IUseableState DoState(Useable_Controller controller)
    {
        return null;
    }
}
