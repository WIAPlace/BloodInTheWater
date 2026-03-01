using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 2/12/26
/// Purpose: Allows the Player to start Dialogue with Objects
/// 
/// Edited: 2/12/26
/// Edited By: Weston Tollette
/// Edit Purpose: added input system reader stuff.
///
/// Edited: 2/21/26
/// Edited By: Weston Tollette
/// Edit Purpose: Changed this script from 'PlayerTalk' to be decoupled for use in interacting with stuff.
/// 
public class InteractSystem : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input; // used for input reader stuff.
    [SerializeField] private GameObject FPCamera;
    [SerializeField] private LayerMask interactMask; // mask for what will be hit by raycast
    [SerializeField][Tooltip("Interact Layer and any that would block it")]
    private LayerMask hitMask; // Interact layer and any that could block it
    [SerializeField] float interactDistance = 2.6f; //Should be the same distance as the interactable hover
    [SerializeField] private float radius=1f; // radius of spherecast
    
    public bool inConversation;
    
    

    private void Start()
    {
        input.InteractEvent += HandleInteract;
        input.InteractUIEvent += HandleInteract;
    }
    void OnDestroy()
    {
        input.InteractEvent -= HandleInteract;
        input.InteractUIEvent -= HandleInteract;
    }

    void Update()
    {
        
    }

    void Interact()
    {
        if (inConversation) //Goes to next line
        {
            DialogueBoxController.instance.SkipLine();
        }
        else //Starts interact
        {
            if (Physics.SphereCast(new Ray(FPCamera.transform.position, FPCamera.transform.forward),radius, out RaycastHit hitInfo, interactDistance,hitMask,QueryTriggerInteraction.Collide))
            {
                if((((1 << hitInfo.collider.gameObject.layer) & interactMask.value) != 0) && hitInfo.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }

    void JoinConversation()
    {
        inConversation = true;
        input.SetUI();
    }

    void LeaveConversation()
    {
        inConversation = false;
        input.SetGameplay();
    }

    private void OnEnable()
    {
        DialogueBoxController.OnDialogueStarted += JoinConversation;
        DialogueBoxController.OnDialogueEnded += LeaveConversation;
    }
    
    private void OnDisable()
    {
        DialogueBoxController.OnDialogueStarted -= JoinConversation;
        DialogueBoxController.OnDialogueEnded -= LeaveConversation;
    }
    
    private void HandleInteract() // on Interact (press E) something will be interacted with.
    {
        Interact();
    }
}
