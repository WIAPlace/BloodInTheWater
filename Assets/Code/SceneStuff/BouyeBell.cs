using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyeBell : MonoBehaviour
{
    [SerializeField] AudioSource bell;
    [SerializeField] private SoundEffectSO bellSound;
    // Start is called before the first frame update
    [SerializeField]
    private float bellIntervals;

    private Coroutine theBellTolls;
    void Start()
    {
        theBellTolls = StartCoroutine(TollingBells());
    }
    IEnumerator TollingBells()
    {
        while (true)
        {
            yield return new WaitForSeconds(bellIntervals);
            bellSound.Play(bell);
        }
    }
}
