using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marshall Turner
/// Created: 2/25/26
/// Purpose: For interactables to be able to transistion
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
///
public class TransitionButton : MonoBehaviour, IInteractable
{
    public TransistionScene transistion;
    public SoundEffectSO transitionSound;
    public AudioSource transitionSourse;

    public void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("Default"); //Changes the layer to hide the crosshair hover
        if(transitionSound !=null && transitionSourse != null)
        {
            transitionSound.Play(transitionSourse);
        }
        transistion.StartGame();
    }
}
