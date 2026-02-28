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
    [field: Header("Refrences")]
    [field: SerializeField][Tooltip("Layer for Monsters")]
    public LayerMask MonsterMask { get; private set; }


    [field: Header("Variables")]
    [field: SerializeField][Tooltip("Distanse for attack")]
    public float AtkDistance { get; private set; }
    [field: SerializeField][Tooltip("Harpoon's thickness / leniency for hits")]
    public float AtkRadius { get; private set; }



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
