using System.Collections;
using System.Collections.Generic;
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

    public IUsableState currentState;

    // States:
    // Idle // no contact in between action states.
    // Readying // The process of setting up to do the thing. //press held down 
    // UnReadying // the reverse of readying, will happen if let go before is ready
    // IsReady // finished readying, if let go it will do its thing
    public Abs_StateUseItem UseItem; // If Ready On release, the object will do its thing
    // EndUse // the action of what occurs after using. will lead back into idle ?

    void Update()
    {
        if(currentState != null)
        {
            
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
    {
        
    }
    private void HandleUseCancelled()
    {
        
    }
}
