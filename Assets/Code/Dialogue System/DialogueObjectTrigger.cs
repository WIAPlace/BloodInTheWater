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
public class DialogueObjectTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] bool firstInteraction = true;
    [SerializeField] bool noRepeat = true;
    [SerializeField] int repeatStartPosition;
    [Space(5)]
    public DialogueAsset dialogueAsset;
    [Space(5)]
    public GameObject obj;
    public bool turnOn;

    [HideInInspector]

    void Start()
    {
        if (turnOn)
        {
            obj.SetActive(false);
        }
    }

    public int StartPosition
    {
        get
        {
            if (firstInteraction)
            {
                firstInteraction = false;
                if (noRepeat)
                {
                    gameObject.layer = LayerMask.NameToLayer("Default"); //Changes the layer to hide the crosshair hover
                }
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
        if (turnOn)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }

        DialogueBoxController.instance.StartDialogue(dialogueAsset.dialogue,dialogueAsset.audioclip, StartPosition, dialogueAsset.speaker);
    }
}