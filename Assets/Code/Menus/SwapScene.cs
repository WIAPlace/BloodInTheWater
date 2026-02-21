using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/x/26 //this existed but i forgot when i made it, and didnt add this header
/// Purpose: Swaps the scenes
/// 
/// Edited: 2/21/26
/// Edited By: Weston Tollette
/// Edit Purpose: implemented the interactable interface.
///
public class SwapScene : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneName;
    [SerializeField] private ChangeScene sceneChange;

    public void ChangeScene()
    {
        sceneChange.LoadSceneByName(sceneName);
    }

    public void Interact()
    {
        ChangeScene();
    }
}
