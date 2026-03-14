using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 1/11/26
/// Purpose: Player can press "T" to open a dialogue on the spot
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
/// 
public class DialogueThink : MonoBehaviour
{
    [SerializeField] bool firstInteraction = true;
    [SerializeField] bool noRepeat = true;
    [SerializeField] int repeatStartPosition;

    public string npcName;
    public DialogueAsset[] dialogueAsset;
    public InteractSystem interactSystem;

    

    [HideInInspector]
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!interactSystem.inConversation)
            {
                Interact();
            }
            else
            {
                Debug.Log("Busy");
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ThoughtTracker.thoughtNum += 1;
            Debug.Log("Thought Change +1");
        }
    }

    public void Interact() // Implemented from interface to start the conversation. Uses a static varibale from ThoughtTracker.
    {
        DialogueBoxController.instance.StartDialogue(dialogueAsset[ThoughtTracker.thoughtNum].dialogue,dialogueAsset[ThoughtTracker.thoughtNum].audioclip, StartPosition, dialogueAsset[ThoughtTracker.thoughtNum].speaker);
    }
}