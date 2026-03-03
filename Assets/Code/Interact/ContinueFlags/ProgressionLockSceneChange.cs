using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/1
/// Purpose: progress to the next scene if criteria is met.
/// 
/// Edited: 
/// Edited By:
/// Edit Purpose:
///
public class ProgressionLockSceneChange : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneName;
    [SerializeField] private TransistionScene transition;
    [SerializeField] [Tooltip("Warning message to throw")] DialogueAsset dialogueAsset; 
    //[SerializeField] private bool toMenu=false;
    [SerializeField]private ProgressionBlock_Abs progress;

    public void Interact()
    {
        if(progress != null){
            if (progress.progressFlag.Progress())
            {
                ChangeScene();
            }
            else
            {
                DialogueBoxController.instance.StartDialogue(dialogueAsset.dialogue,dialogueAsset.audioclip, 0);
            }
        }
        else
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        transition.StartGame();
    }
}
