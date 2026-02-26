
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Abstract for scripts of usable Items.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public abstract class UseableItem_Abstract
{
    [Tooltip("Game Object of the thing being used")]
    public GameObject usableMesh;

    public Abs_StateUseItem UseItem;

    
}
