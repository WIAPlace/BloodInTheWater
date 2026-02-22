using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// 
/// Author: Weston Tollette
/// Created: 2/5/26
/// Purpose: This is a tool to communicate between the GameInput Script and the scripts that call for those inputs.
///     This will allow us to not have to reform the connections from inputsystem and script uses in each and every script used. 
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGamePlayActions, GameInput.IUIActions, GameInput.IQuickTimeActions
{
    private GameInput gameInput;

    void OnEnable()
    { // Enable this When the game begins.
        if (gameInput == null)
        {
            gameInput=new GameInput();
            gameInput.GamePlay.SetCallbacks(this);
            gameInput.UI.SetCallbacks(this);
            gameInput.QuickTime.SetCallbacks(this);

            SetGameplay();
        }
    }
    void OnDisable()
    { // Disable this when the game Stops.
        if(gameInput != null)
        {
           gameInput.Disable(); 
        }
    }

    public void SetGameplay()
    {
        gameInput.GamePlay.Enable();
        gameInput.UI.Disable();
        gameInput.QuickTime.Disable();
        //Debug.Log("Gameplay");
    }
    public void SetUI()
    {
        gameInput.GamePlay.Disable();
        gameInput.UI.Enable();
        gameInput.QuickTime.Disable();
        //Debug.Log("UI");
    }
    public void SetQuickTime()
    {
        gameInput.GamePlay.Disable();
        gameInput.UI.Disable();
        gameInput.QuickTime.Enable();
    }

    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action UseEvent;
    public event Action UseCancelledEvent;
    public event Action InteractEvent;
    public event Action InteractUIEvent;
    public event Action PlaceEvent;
    public event Action PlaceCancelledEvent;

    public event Action UseEventQT; // quick time click

    public event Action PauseEvent;
    public event Action ResumeEvent;


    ///////////////////////////////////////////////////////////////////////////////////////// OnX Methods 
    /// Called when An input occurs. They should Invoke their related events. 
    /// Will likley need to be added to when adding more inputs.
    public void OnUse(InputAction.CallbackContext context)
    { // Use
        if (context.phase == InputActionPhase.Performed)
        {
            UseEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            UseCancelledEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    { // Move (Sends a vector 2 beacuse the values considered are the cardinal directions.)
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPause(InputAction.CallbackContext context)
    { // Pause 
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI(); // Make it so that only UI controls are able to be done.
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    { // Resume
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay(); // Make it so that only gameplay controls are able to be done.
        }
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }
    }
    public void OnInteractUI(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractUIEvent?.Invoke();
        }
    }
    public void OnUseQT(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            UseEventQT?.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnPlace(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PlaceEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            PlaceCancelledEvent?.Invoke();
        }
    }

    /////////////////////////////////////////////////////////////////////////// Unimplemented 

    public void OnNavigate(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    
}
