using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using System.Security.Cryptography.X509Certificates;
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
/// Edited: 2/23/26
/// Edited By: Weston Tollette
/// Edit Purpose: updating this to be able to modify the sensitivity.
/// 
public class PlayerLook : MonoBehaviour
{
    [SerializeField][Tooltip("Cinamachine Camera")]
    private CinemachineInputAxisController inputController;
    [SerializeField][Tooltip("X sensitivity")]
    private float mouseSensitivityX = 1.0f;
    [SerializeField][Tooltip("Y sensitivity")]
    private float mouseSensitivityY = -1.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void UpdateSensitivity()
    {
        // Iterate through the driven axes in the controller
        foreach (var axis in inputController.Controllers)
        {
            // Names usually match "Look X (Pan)" and "Look Y (Tilt)" 
            // Check your component's "Driven Axis" labels in the Inspector
            if (axis.Name == "Look X (Pan)")
            {
                axis.Input.Gain = mouseSensitivityX;
            }
            else if (axis.Name == "Look Y (Tilt)")
            {
                axis.Input.Gain = mouseSensitivityY;
            }
        }
    }

    public void UpdateSensitivity(bool isX, float value)
    {
        if (isX)
        {
            mouseSensitivityX = value;
        }
        else
        {
            mouseSensitivityY = -value;
        }
        UpdateSensitivity();
    }
}
