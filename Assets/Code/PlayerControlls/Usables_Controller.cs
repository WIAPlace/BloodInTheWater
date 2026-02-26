using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Usable Items Controller
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Usables_Controller : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader.")]
    private InputReader input;

    [SerializeField] [Tooltip("Array for the avalible items of use")]
    private UseableItem_Abstract[] UsableItem;
    // 0 = null
    // 1 = fishing rod
    // 2 = harpoon
    

    public UseableItem_Abstract currentItem; // used to hold what the current item in use is
    public IUsableState currentState; // will just be swaped out for whatever is needed at that point

    // States:
    //public Abs_StateItemIdle Idle; // no contact in between action states.
    //public Abs_StateItemReadying Readying; // The process of setting up to do the thing. //press held down 
    //public Abs_StateItemIsReady IsReady; // finished readying, if let go it will do its thing
    //public Abs_StateItemUse UseItem; // If Ready On release, the object will do its thing

    void Update()
    {
        if(currentState != null)
        {
            currentState = currentItem.DoState(this);
        }
    }
    

    public void ChangeState(IUsableState newState)
    {
        currentState.DoExit(); // leave the prevvious state
        currentState = newState;
        currentState.DoEnter(); // enter the new state
    }
    public void ChangeItem(int num)
    {
        if(num<UsableItem.Length) // bounds checker
        { 
            currentItem = UsableItem[num];
        }
    }






    // On thing happens
    void OnEnable()
    {
        input.UseEvent += HandleUse;
        input.UseCancelledEvent += HandleUseCancelled;
    }

    void OnDisable()
    {
        input.UseEvent -= HandleUse;
        input.UseCancelledEvent -= HandleUseCancelled;
    }
    void OnDestroy()
    {
        input.UseEvent -= HandleUse;
        input.UseCancelledEvent -= HandleUseCancelled;
    }

    private void HandleUse()
    { // on clicking trigger of use
        if(currentItem == null)
        { // Error catcher for if we have nothing in our hands
            return;
        }

        ChangeState(currentItem.Readying);
    }
    private void HandleUseCancelled()
    { // on let go of trigger for use
        if(currentItem == null)
        { // Error catcher for if we have nothing in our hands
            return;
        }

        if(currentState == currentItem.IsReady)
        {   // If the windup has occured and we are ready to inact
            ChangeState(currentItem.UseItem);
        }
        else if(currentState == currentItem.Readying)
        { // if winding up and let go early. 
            // this is mainly used to handle simple clicks not setting off the items
            ChangeState(currentItem.Idle);
        }

    }
}
