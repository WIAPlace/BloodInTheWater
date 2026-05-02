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
    [SerializeField] [Tooltip("Warning message to throw")] DialogueAsset[] dialogueAsset; 
    //[SerializeField] private bool toMenu=false;
    [SerializeField]private ProgressionBlock_Abs[] progress;
    //[SerializeField] private GameObject[] lookLocations;
    private bool progression = false;

    public void Interact()
    {
        if(progress != null){
            progression = true;
            for(int i = 0; i < progress.Length;i++)
            {
                if (progress[i] != null && !progress[i].progressFlag.Progress())
                {   // if this is false play the dialouge to let them know
                    if(dialogueAsset[i]!=null)DialogueBoxController.instance.StartDialogue(dialogueAsset[i], 0);
                    progression = false;
                    return;
                }
            }
            
            if (progression)
            {
                ChangeScene();
            }
        }
        else
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        GameManager.Instance.SetPause(true);
        transition.StartGame();
    }
}
