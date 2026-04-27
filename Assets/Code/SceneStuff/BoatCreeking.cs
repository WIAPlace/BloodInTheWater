using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCreeking : MonoBehaviour
{
    [SerializeField][Tooltip("Boat Rocking")]
    private SoundEffectSO BoatCreek;
    [SerializeField]
    private SoundEffectSO BoatHit;
    [SerializeField][Tooltip("BoatAudio Sourse")]
    private AudioSource soundMaker;

    

    [SerializeField][Tooltip("Wether boat should make noises")]
    private bool creekOn = true;
    [SerializeField]
    float creekingIntervals;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.BoatHit += HandleBoatHit;
        StartCoroutine(Creeking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Creeking()
    {
        while (creekOn)
        {
            yield return new WaitForSeconds(creekingIntervals);
            BoatCreek.Play(soundMaker);
        }
        
    }
    public void HandleBoatHit(float notUsed)
    {
        BoatHit.Play(soundMaker);
    }
}
