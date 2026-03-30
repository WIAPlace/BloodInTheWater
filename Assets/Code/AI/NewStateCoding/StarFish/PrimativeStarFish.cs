using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering;

public class PrimativeStarFish : MonoBehaviour
{
    [SerializeField] 
    private GameObject body;
    [SerializeField]
    [Tooltip("Starfish Spawn locations\n0=left\n1=right\n2=back\n3=front")]
    private Transform[] spawnSpots;
    [SerializeField]
    Animator anim;

    [SerializeField]
    private AudioSource soundMaker;
    [SerializeField][Tooltip("SFX SO for StarFish Being Hit")]
    private SoundEffectSO hit;
    [SerializeField][Tooltip("SFX SO for StarFish Dying")]
    private SoundEffectSO fall;

    [SerializeField][Tooltip("Max Number of hits to get this man out of here.")]
    private int maxHP = 3;
    private int currentHP;

    [SerializeField][Tooltip("Amnt of damage he does to ship")]
    private float damageToBoat = .1f;
    [SerializeField][Tooltip("how much time between causing damage to the ship")]
    private float intervals = 1f;
    private bool onBoat = false;
    private Coroutine hurtingBoat;

    [SerializeField][Tooltip("Min Seconds Before this guy is allowed to spawn")]
    private float min = 0;
    [SerializeField][Tooltip("Max Seconds Before this guy is allowed to spawn")]
    private float max = 10;
    
    void OnEnable()
    {
        body.SetActive(false);
        StartCoroutine(ChanceSpawn());

    }

    // trigger on spawinging this man. //////////////////////////
    public void Spawn()
    {
        body.SetActive(true);
        SpawnInRandomLocation();
        onBoat = true;
        currentHP = maxHP;
        hurtingBoat = StartCoroutine(HurtBoat());
    }

    // triggers when hp is 0 ///////////////////////////////////
    private void Despawn()
    {

        onBoat = false;
        fall.Play(soundMaker);
        if (hurtingBoat != null)
        {
            StopCoroutine(hurtingBoat);
        }

        body.SetActive(false);
        StartCoroutine(ChanceSpawn());
    }

    // when a limb is hit ////////////////////////////////////////
    public void LimbHit()
    {
        currentHP -= 1; // -1 to hp
        
        if(currentHP <= 0)
        { // if hp is bellow zero despawn.
            Despawn();
        }
        else
        {   // we have a sound that plays specificly if it dies.
            hit.Play(soundMaker);
        }
    }

    private IEnumerator HurtBoat() /////////////////////////////////////
    {
        while(onBoat){
            yield return new WaitForSeconds(intervals);
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DamageBoat(damageToBoat);
            }
        }
    }
    private IEnumerator ChanceSpawn() ////////////////////////////////// Spawn Chance
    {
        float rando = Random.Range(min,max);
        yield return new WaitForSeconds(rando);
        Spawn();
    }

    public void SpawnInRandomLocation() ///////////////////////////////// Spawn location
    {
        int rando = Random.Range(0,spawnSpots.Length);
        transform.SetPositionAndRotation(spawnSpots[rando].position,spawnSpots[rando].rotation);

        switch (rando)
        {
            case 0:
                anim.SetTrigger("LeftSideGrab");
                break;
            case 1:
                anim.SetTrigger("RightSideGrab");
                break;
            case 2:
                anim.SetTrigger("BackGrab");
                break;
            case 3:
                anim.SetTrigger("FrontGrab");
                break;

            default:
                
                break;
        }
    }


}
