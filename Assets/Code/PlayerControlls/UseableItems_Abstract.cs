using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/22/26
/// Purpose: Abstract for scripts with usable Items
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public abstract class UseableItems_Abstract : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader.")]
    protected InputReader input;


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

    protected abstract void HandleUse();
    protected abstract void HandleUseCancelled();
}
