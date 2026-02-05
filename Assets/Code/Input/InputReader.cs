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

public class InputReader : ScriptableObject, GameInput.IGamePlayActions, GameInput.IUIActions
{
    private GameInput gameInput;

    void OnEnable()
    { // Enable this When the game begins.
        if (gameInput == null)
        {
            gameInput=new GameInput();
            gameInput.GamePlay.SetCallbacks(this);
            gameInput.UI.SetCallbacks(this);
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
    }
    public void SetUI()
    {
        gameInput.GamePlay.Disable();
        gameInput.UI.Enable();
    }

    public event Action<Vector2> MoveEvent;
    public event Action InteractEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;



    public void OnInteract(InputAction.CallbackContext context)
    { // Interact
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
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
}
