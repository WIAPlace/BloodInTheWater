
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

    // States:
    public Abs_StateItemIdle Idle; // no contact in between action states.
    public Abs_StateItemReadying Readying; // The process of setting up to do the thing. //press held down 
    public Abs_StateItemIsReady IsReady; // finished readying, if let go it will do its thing
    public Abs_StateItemUse UseItem; // If Ready On release, the object will do its thing

    public abstract IUsableState DoState(Usables_Controller controller);
}
