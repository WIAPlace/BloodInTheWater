using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXInIntervals : MonoBehaviour
{
    [SerializeField] AudioSource soundMaker;
    [SerializeField] SoundEffectSO soundEffect;

    [SerializeField] float intervals;

    public bool continuePlay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySFXLoop());
    }

    IEnumerator PlaySFXLoop()
    {
        while (continuePlay)
        {
            yield return new WaitForSeconds(intervals);
            soundEffect.Play(soundMaker);
        }
    }
    
}
