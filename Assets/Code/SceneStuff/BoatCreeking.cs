using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCreeking : MonoBehaviour
{
    [SerializeField][Tooltip("Boat Rocking")]
    private SoundEffectSO BoatCreek;
    [SerializeField][Tooltip("BoatAudio Sourse")]
    private AudioSource soundMaker;

    [SerializeField][Tooltip("Wether boat should make noises")]
    private bool creekOn = true;
    // Start is called before the first frame update
    void Start()
    {
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
            yield return new WaitForSeconds(20);
            BoatCreek.Play(soundMaker);
        }
        
    }
}
