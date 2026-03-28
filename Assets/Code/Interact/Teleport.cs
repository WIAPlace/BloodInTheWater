using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    public Transform teleportTarget; //Teleport location
    public GameObject thePlayer;

    public void Interact()
    {
        thePlayer.transform.position = teleportTarget.transform.position;
    }
}
