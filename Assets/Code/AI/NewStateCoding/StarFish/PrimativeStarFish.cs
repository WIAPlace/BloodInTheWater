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
    [SerializeField]
    private SoundEffectSO remain;

    [SerializeField][Tooltip("Max Number of hits to get this man out of here.")]
    private int maxHP = 3;
    private int currentHP;

    [SerializeField][Tooltip("Amnt of damage he does to ship")]
    private float damageToBoat = .1f;
    [SerializeField][Tooltip("how much time between causing damage to the ship")]
    private float intervals = 1f;
    private bool onBoat = false;
    private Coroutine hurtingBoat;
    private Coroutine scratching;

    [SerializeField][Tooltip("Min Seconds Before this guy is allowed to spawn")]
    private float min = 0;
    [SerializeField][Tooltip("Max Seconds Before this guy is allowed to spawn")]
    private float max = 10;
    
    private int position;
    void OnEnable()
    {
        body.SetActive(false);
        StartCoroutine(ChanceSpawn());

    }
    private void OnDisable()
    {
        if (onBoat)
        {
            GameState.Instance.OnBoard(false);
        }
    }
    // trigger on spawinging this man. //////////////////////////
    public void Spawn()
    {
        
        impSour.GenerateImpulse(); // make the player feel the rumble
        start.Play(soundMaker); // make sound on start
        onBoat = true; // boi is on boat
        GameState.Instance.OnBoard(true);// boi is on boat
        currentHP = maxHP; // set current hp = max hp
        hurtingBoat = StartCoroutine(HurtBoat()); // start hurting
        scratching = StartCoroutine(ScratchBoat());

        
        
    }

    // triggers when hp is 0 ///////////////////////////////////
    private void Despawn()
    {
        onBoat = false; // off boat so stop hurting
        GameState.Instance.OnBoard(false); // boi fall off boat
        fall.Play(soundMaker); // play sfx
        if (hurtingBoat != null)
        { // if hurting boat stop
            StopCoroutine(hurtingBoat);
            if(scratching != null) StopCoroutine(scratching);
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
            //Despawn();
            //ChangeAnimation(5);
            ChangeAnimation(4);
            StartCoroutine(WaitToDespawn());
        }
        else
        {   // we have a sound that plays specificly if it dies.
            hit.Play(soundMaker);
            ChangeAnimation(4);
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
    private IEnumerator ScratchBoat() /////////////////////////////////////
    {
        int sfxSOLength = remain.clips.Length;
        while(true){
            int randoIndex = Random.Range(0, sfxSOLength);
            remain.Play(soundMaker);
            yield return new WaitForSeconds(remain.clips[randoIndex].length + .1f);
        }
    }
    private IEnumerator ChanceSpawn() ////////////////////////////////// Spawn Chance
    {
        float rando = Random.Range(min,max);
        yield return new WaitForSeconds(rando);

        body.SetActive(true); // set the body as see able
        position = Random.Range(0,spawnSpots.Length); // set position to random location
        transform.SetPositionAndRotation(spawnSpots[position].position,spawnSpots[position].rotation);
        ChangeAnimation(6);
        // physicaly set to new position

        yield return new WaitForSeconds(.01f);
        ChangeAnimation(position); // spawin at one of the spawn points
        Spawn();
    }
    
    public void ChangeAnimation(int key)
    {
        anim.ResetTrigger("LeftSideGrab");
        anim.ResetTrigger("RightSideGrab");
        anim.ResetTrigger("BackGrab");
        anim.ResetTrigger("FrontGrab");
        anim.ResetTrigger("Flail");
        anim.ResetTrigger("Death");
        anim.ResetTrigger("Spawn");

        switch (key)
        {
            case 0:
                anim.SetTrigger("LeftSideGrab");
                
                Debug.Log("LeftSide");
                break;
            case 1:
                anim.SetTrigger("RightSideGrab");
                
                Debug.Log("rightSide");
                break;
            case 2:
                anim.SetTrigger("BackGrab");
                
                Debug.Log("backSide");
                break;
            case 3:
                anim.SetTrigger("FrontGrab");
                
                Debug.Log("frontSide");
                break;
            case 4:
                anim.SetTrigger("Flail");
                StartCoroutine(WaitToChange());
                
                // dont scratch while flailing

                Debug.Log("flail");
                break;
            case 5:
                anim.SetTrigger("Death");
                
                Debug.Log("Death");
                break;
            case 6:
                anim.SetTrigger("Spawn");
                
                Debug.Log("Death");
                break;

            default:
                
                break;
        }
    }
    IEnumerator WaitToChange()
    {
        yield return new WaitForSeconds(.01f);
        ChangeAnimation(position);
    }
    IEnumerator WaitToDespawn()
    {
        yield return new WaitForSeconds(1f);
        Despawn();
    }


}
