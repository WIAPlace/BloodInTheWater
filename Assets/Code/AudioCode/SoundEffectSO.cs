using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/19/26
/// Purpose: scriptable object for sound effects.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
[CreateAssetMenu(menuName = "Audio/SoundEffect")]
public class SoundEffectSO : ScriptableObject {
    public AudioClip[] clips;
    public float volume = 1f;
    public float pitchVariant = 0.1f;

    public void Play(AudioSource source) {
        source.pitch = 1f + Random.Range(-pitchVariant, pitchVariant);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)], volume);
    }
}
