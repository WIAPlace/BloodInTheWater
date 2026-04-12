using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Marsahll Turner
/// Created: 4/12/26
/// Purpose: To trigger either turning on or off a GameObject when entering a trigger zone
/// 
/// Edited: 
/// Edited By: 
/// Edit Purpose: 
///
public class TriggerZone : MonoBehaviour
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

    private void OnTriggerEnter(Collider thePlayer)
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
