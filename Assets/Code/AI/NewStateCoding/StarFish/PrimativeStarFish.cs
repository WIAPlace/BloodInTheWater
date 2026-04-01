using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


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
    CinemachineImpulseSource impSour;

    [SerializeField]
    private AudioSource soundMaker;
    [SerializeField][Tooltip("SFX SO for StarFish Being Hit")]
    private SoundEffectSO hit;
    [SerializeField][Tooltip("SFX SO for StarFish Dying")]
    private SoundEffectSO fall;
    [SerializeField][Tooltip("SFX SO for StarFish arriving")]
    private SoundEffectSO start;

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
        impSour.GenerateImpulse(); // make the player feel the rumble
        body.SetActive(true); // set the body as see able
        SpawnInRandomLocation(); // spawin at one of the spawn points
        start.Play(soundMaker); // make sound on start
        onBoat = true; // boi is on boat
        currentHP = maxHP; // set current hp = max hp
        hurtingBoat = StartCoroutine(HurtBoat()); // start hurting
    }

    // triggers when hp is 0 ///////////////////////////////////
    private void Despawn()
    {
        onBoat = false; // off boat so stop hurting
        fall.Play(soundMaker); // play sfx
        if (hurtingBoat != null)
        { // if hurting boat stop
            StopCoroutine(hurtingBoat);
        }

        body.SetActive(false); // go invis
        StartCoroutine(ChanceSpawn());// start plotting arrival
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
