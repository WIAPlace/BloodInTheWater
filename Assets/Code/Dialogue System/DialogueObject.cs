using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 2/12/26
/// Purpose: Sets the Object to Have Dialogue
/// 
/// Edited: 2/21/26
/// Edited By: Weston Tollette
/// Edit Purpose: implemented the Interactable Interface so that it is usable with the new interact system.
/// 
public class DialogueObject : MonoBehaviour, IInteractable
{
    [SerializeField] bool firstInteraction = true;
    [SerializeField] int repeatStartPosition;

    public string npcName;
    public DialogueAsset dialogueAsset;

    [HideInInspector]
    public int StartPosition
    {
        get
        {
            if (firstInteraction)
            {
                firstInteraction = false;
                gameObject.layer = LayerMask.NameToLayer("Default"); //Changes the layer to hide the crosshair hover
                return 0;
            }
            else
            {
                return repeatStartPosition; //Starts the dialogue at the set line. Set it to a line that doesn't exist to disable the ability to talk again
            }
        }
    }
    public void Interact() // Implemented from interface to start the conversation.
    {
        DialogueBoxController.instance.StartDialogue(dialogueAsset.dialogue,dialogueAsset.audioclip, StartPosition, npcName);
    }
}