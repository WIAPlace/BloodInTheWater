using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/1
/// Purpose: interface for allowed progression in the fishing minigame
/// 
/// Edited: 
/// Edited By:
/// Edit Purpose:
///
public class Progress_EndFishing : IProgressFlag
{
    public bool Progress()
    {
        if(GameState.Instance != null) // check for if this exists.
        {
            return GameState.Instance.CheckIfEnoughCaught();
        }
        else
        {
            return true; // if there is no game state for some reason just let them leave
        }
    }
}
