using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private ChangeScene sceneChange;
    [SerializeField] [Tooltip("Warning message to throw")] DialogueAsset dialogueAsset; 
    [SerializeField] private bool toMenu=false;
    private IProgressFlag progressFlag = new Progress_EndFishing();

    public void Interact()
    {
        if (progressFlag.Progress())
        {
            ChangeScene();
        }
        else
        {
            DialogueBoxController.instance.StartDialogue(dialogueAsset.dialogue,dialogueAsset.audioclip, 0);
        }
    }

    public void ChangeScene()
    {
        sceneChange.LoadSceneByName(sceneName);
    }
}
