using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/5/26
/// Purpose: Player Look Controlls (should be attached to a camera)
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class PlayerLook : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input;

    [SerializeField] [Tooltip("Mouse Sensitivity")] [Range(0f, 300f)]
    private float sensitivity;

    private Vector2 mouseInput; // will store the inputs of the mouse

    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        input.LookEvent += HandleLook;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, mouseInput.x * sensitivity * Time.deltaTime); // rotates the objects transform

        pitch -= mouseInput.y * sensitivity * Time.deltaTime; // pitch is for the y axis
        pitch = Mathf.Clamp(pitch, -80f, 80f); // forces range of look for up and down so you dont do a back flip.
        transform.localEulerAngles = new Vector3(pitch, transform.localEulerAngles.y, 0f);
    }


    private void HandleLook(Vector2 mouse)
    {
        mouseInput = mouse;
    }
}
