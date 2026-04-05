using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySettings : MonoBehaviour
{
    [SerializeField]
    PlayerPrefrenceScript pref;
    void Start()
    {
        ApplyChanges();
    }
    public void ApplyChanges()
    {
        pref.ApplySettings();
    }

    
}
