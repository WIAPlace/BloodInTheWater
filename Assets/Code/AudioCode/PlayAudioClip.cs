using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    [SerializeField]SoundEffectSO stepSFX;
    [SerializeField]AudioSource soundMaker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayStep()
    {
        stepSFX.Play(soundMaker);
    }
}
