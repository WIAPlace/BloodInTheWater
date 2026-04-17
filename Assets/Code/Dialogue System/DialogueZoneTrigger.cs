using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 4/12/26
/// Purpose: To trigger either turning on or off a GameObject when entering a trigger zone and allows dialogue
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
///  
public class DialogueZoneTrigger : MonoBehaviour
{
    public GameObject thePlayer;

    [SerializeField] bool firstInteraction = true;
    [SerializeField] bool noRepeat = true;
    [SerializeField] int repeatStartPosition;
    [Space(5)]
    public DialogueAsset dialogueAsset;
    [Space(5)]
    public GameObject obj;
    public bool turnOn;

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

    private void OnTriggerEnter(Collider thePlayer)
    {
        if (turnOn)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }

        DialogueBoxController.instance.StartDialogue(dialogueAsset.dialogue, dialogueAsset.audioclip, StartPosition, dialogueAsset.speaker);
    }
}
