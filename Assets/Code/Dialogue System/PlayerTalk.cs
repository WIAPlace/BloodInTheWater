using System.Runtime.InteropServices;
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
public class PlayerTalk : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input; // used for input reader stuff.
    [SerializeField] float talkDistance = 2.6f; //Should be the same distance as the interactable hover
    bool inConversation;

    private void Start()
    {
        input.ActivateEvent += HandleActivate;
        input.ActivateUIEvent += HandleActivate;
    }
    void OnDestroy()
    {
        input.ActivateEvent -= HandleActivate;
        input.ActivateUIEvent -= HandleActivate;
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
        else //Starts dialogue
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, talkDistance))
            {
                //Debug.Log("Hit");
                if (hitInfo.collider.gameObject.TryGetComponent(out DialogueObject npc))
                {
                    DialogueBoxController.instance.StartDialogue(npc.dialogueAsset.dialogue,npc.dialogueAsset.audioclip, npc.StartPosition, npc.npcName);
                }
                else
                {
                    SwapScene swapScene = hitInfo.transform.gameObject.GetComponent<SwapScene>();
                    swapScene.ChangeScene();
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
    
    private void HandleActivate() // on activate (press E) something will be interacted with.
    {
        Interact();
    }
}