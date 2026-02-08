using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/5/26
/// Purpose: Player Look Controlls (should be attached to a camera)
/// 
/// Edited: 2/7
/// Edited By: Weston Tollette
/// Edit Purpose: the acctual controlls of this were redundent since we have the imput axis controller, and they were causing issues 
/// so that part has been deleted and now only the mouse erase and lock remain. 
/// later on this will likley be used for sensitivity settings to change the gain/pits of the input axis stuff.
/// 
public class PlayerLook : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
