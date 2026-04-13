using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 4/12/26
/// Purpose: To trigger either turning on or off a GameObject when interacting with a trigger button
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
///
public class TriggerButton : MonoBehaviour, IInteractable
{
    public GameObject obj;
    public bool turnOn;

    void Start()
    {
        if (turnOn)
        {
            obj.SetActive(false);
        }
    }

    public void Interact()
    {
        if (turnOn)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }
}
