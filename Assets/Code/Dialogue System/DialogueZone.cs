using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueZone : MonoBehaviour
{
    public GameObject thePlayer;

    [SerializeField] bool firstInteraction = true;
    [SerializeField] bool noRepeat = true;
    [SerializeField] int repeatStartPosition;

    public string npcName;
    public DialogueAsset dialogueAsset;


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
        DialogueBoxController.instance.StartDialogue(dialogueAsset.dialogue, dialogueAsset.audioclip, StartPosition, npcName);
    }
}
