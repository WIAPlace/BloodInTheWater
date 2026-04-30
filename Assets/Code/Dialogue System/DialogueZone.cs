using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 3/4/26
/// Purpose: To trigger dialogue when entering a trigger object
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
/// 
public class DialogueZone : MonoBehaviour
{
    public GameObject thePlayer;

    [SerializeField] bool firstInteraction = true;
    [SerializeField] bool noRepeat = true;
    [SerializeField] int repeatStartPosition;

    public DialogueAsset dialogueAsset;

    [SerializeField] GameObject[] lookLocations;

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

    private void OnTriggerEnter(Collider thePlayer)
    {
        DialogueBoxController.instance.StartDialogue(dialogueAsset.dialogue, dialogueAsset.audioclip, StartPosition, dialogueAsset.speaker,lookLocations);
    }
}
