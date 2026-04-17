using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// shows the scale ui when interacting with the scale thing on boat.
public class ShowScaleUI : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameState.Instance.CheckCaughtFish();
    }
}
