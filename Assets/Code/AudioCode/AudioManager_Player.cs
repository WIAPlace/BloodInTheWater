using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/19/26
/// Purpose: holder for scriptable audio objects used by the player. 
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class AudioManager_Player : MonoBehaviour
{
    [field: Header("Audio Sources")]
    [SerializeField] [Tooltip("Audio Sourse for Fishing Rod")]
    private AudioSource rod;



    [field: Header("Audio Clips")]

    [field: Header("Fishing Rod")]
    [field: SerializeField]
    public SoundEffectSO FishOnHook {get; private set;}
}
