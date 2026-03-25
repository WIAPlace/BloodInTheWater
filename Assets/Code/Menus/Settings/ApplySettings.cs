using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySettings : MonoBehaviour
{
    [SerializeField]
    PlayerPrefrenceScript pref;
    public void ApplyChanges()
    {
        pref.ApplySettings();
    }
}
