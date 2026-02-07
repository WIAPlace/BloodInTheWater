using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
/// 
/// Author: Weston Tollette
/// Created: 2/5/26
/// Purpose: Movement of the Player
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input;

    [SerializeField] [Tooltip("How fast the player moves")]
    private float speed;

    [SerializeField] [Tooltip("First Person Camera attached to this so the body moves with the camera")]
    private Transform playerView;

    private CharacterController controller;
    private Vector2 moveDir; // Direction the Player is moving at any given time

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input.MoveEvent += HandleMove;
    }

    void Update()
    {
        RotateToView();
        Move();

    }

    //Movement Method 
    private void Move()
    {
        if(moveDir == Vector2.zero)
        { // If the player has no controls down, so isnt touching any directional input, dont move
            return;
        }
        Vector3 directionalMovement = transform.forward * moveDir.y + transform.right * moveDir.x;

        // move in the desired direction.
        Vector3 movement = directionalMovement * speed * Time.deltaTime;
        controller.Move(movement);
    }

    private void RotateToView()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, playerView.eulerAngles.y, transform.eulerAngles.z);
    }

    // Input Handler // 
    private void HandleMove(Vector2 dir)
    { // Move direction is = to the movement input
        moveDir = dir;
        //Debug.Log(dir); was used to check if we were actualy moving.
    }
}
