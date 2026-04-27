
using UnityEngine;
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
    [SerializeField] [Tooltip("Character controller of player body")]
    private CharacterController controller;
    [SerializeField][Tooltip("Player Body")]
    private GameObject playerBody; 

    [SerializeField] [Tooltip("How fast the player moves")]
    private float speed;

    [SerializeField] [Tooltip("Speed While Angel is latched on")]
    private float latchSpeed;

    [SerializeField] [Tooltip("First Person Camera attached to this so the body moves with the camera")]
    private Transform playerView;

    [SerializeField]
    private AudioSource footStepSource;

    [SerializeField,Tooltip("0 = Dirt\n1 = Grass\n2 = Gravel\n3 = Sand\n4 = Wood\n")]
    private SoundEffectSO[] walking;
    [SerializeField,Tooltip("Time between footseps")]
    private float stepTime;
    private float currentStepTime;
    private float gravity = -9.81f; 

    [SerializeField,Tooltip("Debugging only"),Range(0,4)]
    private int terrainIndex=4; // used for checking which terrain we are walking on
    //private float newStepTime=0;
    private int audioClipPlaying;
    

    private Vector2 moveDir; // Direction the Player is moving at any given time
    private Vector3 velocity; // speed 

    private bool checking = false;
    public bool latchOn = false;

    void Start()
    {
        input.MoveEvent += HandleMove;
        GameManager.OnLatch += HandleLatch;
        GameManager.OnLatchCancelled += HandleLatchCancelled;
        //input.CheckEvent += HandleCheck;
        //input.CheckCancelledEvent += HandleCheckCancelled;
    }
    void OnDestroy()
    {
        input.MoveEvent -= HandleMove;
        GameManager.OnLatch -= HandleLatch;
        GameManager.OnLatchCancelled -= HandleLatchCancelled;
        //input.CheckEvent -= HandleCheck;
        //input.CheckCancelledEvent -= HandleCheckCancelled;
    }

    void Update()
    {
        RotateToView();
        if(latchOn) IsLatched();
        else Move();
        Gravity();

    }

    
    //Movement Method 
    private void Move()
    {
        if(moveDir == Vector2.zero)
        { // If the player has no controls down, so isnt touching any directional input, dont move
            currentStepTime=stepTime; // reset both of these because we have stoped walking
            //newStepTime = 0;
            return;
        }
        //Debug.Log("Moving");
        Vector3 directionalMovement = playerBody.transform.forward * moveDir.y + playerBody.transform.right * moveDir.x;

        // move in the desired direction.
        Vector3 movement = directionalMovement * speed * Time.deltaTime;
        controller.Move(movement);

        // audio for footsteps
        currentStepTime += Time.deltaTime;
        // loop walking sounds
        if (currentStepTime >= stepTime) //currentStepTime >= newStepTime &&
        {   // if the current step time is greater than the audio clip length and time between steps
            currentStepTime = 0;
            
            audioClipPlaying =  Random.Range(0, walking[terrainIndex].clips.Length); // audio clip that be playing
            //newStepTime = walking[terrainIndex].clips[audioClipPlaying].length;

            walking[terrainIndex].Play(footStepSource,audioClipPlaying);
        }

    }
    
    private void RotateToView()
    { // the object / player turns to where the camera is looking
        if(!checking){
            playerBody.transform.rotation = Quaternion.Euler(playerBody.transform.eulerAngles.x, playerView.eulerAngles.y, playerBody.transform.eulerAngles.z);
        }
    }

    // Input Handler // 
    private void HandleMove(Vector2 dir)
    { // Move direction is = to the movement input
        moveDir = dir;
        //Debug.Log(dir); was used to check if we were actualy moving.
    }

    //Gravity
    private void Gravity()
    {
        // Reset velocity when on the ground
        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        // Move the controller
        controller.Move(velocity * Time.deltaTime);
    }

    private void IsLatched() // while deep angel is on face move backwards
    {
        Vector3 directionalMovement = -playerBody.transform.forward;

        // move in the desired direction.
        Vector3 movement = directionalMovement * latchSpeed * Time.deltaTime;
        controller.Move(movement);
    }

    private void HandleCheck()
    {
        checking = true;
    }
    private void HandleCheckCancelled()
    {
        checking = false;
    }

    private void HandleLatch()
    {
        latchOn = true;
    }
    private void HandleLatchCancelled()
    {
        latchOn = false;
    }
}
