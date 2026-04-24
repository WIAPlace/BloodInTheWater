using UnityEngine;
using System.Collections;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 2/24/26
/// Purpose: Scuba controller
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class Scuba_Controller : MonoBehaviour, IMonster
{
    [SerializeField][Tooltip("The places he can spawn")]
    private GameObject[] scubaSpots;
    [SerializeField][Tooltip("The places he can wait to spawn at")]
    private GameObject outOfTheWay; // a transform on the top of the boat.
    [SerializeField][Tooltip("Target for the navmesh")]
    private GameObject target; // should be the player
    [field: SerializeField][Tooltip("Dat body")]
    public GameObject body;
    private IBoatStomperState currentState;
    [SerializeField][Tooltip("Player layer")]
    private LayerMask playerMask;
    [SerializeField][Tooltip("Game Geometry and player layer.")]
    private LayerMask hitMask;
    [SerializeField][Tooltip("Boat Edge layer")]
    private LayerMask edgeMask;
    [SerializeField][Tooltip("Player StateController script")]
    private Useable_Controller useControl;

    
    public QuickTimeData_Scuba scubaData; // stuff for data minigame

    public NavMeshAgent agent; // the part that does the AI.
    public float secondsStunned;
    public float hitForce;
    
    [HideInInspector]
    public Vector3 hitDir;
    [HideInInspector]
    public Rigidbody rb;
    public Coroutine stun;

    [HideInInspector]
    public bool active = true;
    [SerializeField][Tooltip("How long to first spawn")]
    private float spawnMin;
    [SerializeField][Tooltip("How long to first spawn max amount of time")]
    private float spawnMax;
    [SerializeField][Tooltip("How long to respawn")]
    private float respawnMin;
    [SerializeField][Tooltip("How long to respawn max amount of time")]
    private float respawnMax;
    [field: SerializeField][Tooltip("Scuba animator")]
    public Animator anim;
    [field: SerializeField][Tooltip("how long before scuba gets up")]
    public float getUpTime;
    Vector3 direction;

    // All of the States
    public Scuba_StateSpawn SpawnState = new Scuba_StateSpawn(); // when called spawn at a random spot out of avalible ones
    public Scuba_StateMove MoveState = new Scuba_StateMove(); // move twoards player
    public Scuba_StateContact ContactState = new Scuba_StateContact(); // on contact start the minigame
    public Scuba_StateBreakOff BreakOffState = new Scuba_StateBreakOff(); // Activated after breaking out of minigame.
    public Scuba_StateStunned StunnedState = new Scuba_StateStunned(); // After breaking out of minigame he will go into stunned for a moment
    public Scuba_StateHit HitState = new Scuba_StateHit(); // activated once the mans is hit

    private IBoatStomperState previousState;
    public string debugCurrentStateName;
    public string debugPreviousStateName;

    [HideInInspector]
    public bool contacted=false;


    private void Start()
    {
        
        agent = GetComponent<NavMeshAgent>(); // set agent to this part of the component.
        scubaData = GetComponent<QuickTimeData_Scuba>();
        rb = GetComponent<Rigidbody>(); // rigid body for the scuba being hit
        //gameObject.SetActive(false); // start out false because he will be activated in spawn state.
        // scuba will have to be activated by something outside itself, because the update wont run if it is disabled

        GameManager.Instance.unlocks.SaveMonsterData(2);
    }
    private void OnEnable()
    {
        active = false;
        //transform.position = outOfTheWay.transform.position; // dont be in the way
        agent.Warp(outOfTheWay.transform.position);
        agent.enabled = false;
        body.SetActive(false); // dont be seen
        StartCoroutine(StartSpawnDelay(spawnMin,spawnMax)); // start spawning boy.
        //Debug.Log("Spawned");
    }

    void Spawn()
    {
        active = false;
        //transform.position = outOfTheWay.transform.position; // dont be in the way
        
        agent.Warp(outOfTheWay.transform.position);
        agent.enabled = false;
        
        body.SetActive(false); // dont be seen
        StartCoroutine(StartSpawnDelay(respawnMin,respawnMax)); // start spawning boy.
        //Debug.Log("Spawned");
    }

    private void Update()
    {
        if(currentState!=null && active)
        { // set current state to whatever the state tells you to
            IBoatStomperState holder = currentState.DoState(this);
            if(currentState != holder) 
            { // using this as a of being able to utilize change state instead of just changing current state dirrectly
                ChangeState(holder);

                debugCurrentStateName = currentState.GetType().Name; //used for debuging to see name
                debugPreviousStateName = previousState?.GetType().Name; //used for debuging to see name
            }
        }
    }
    public void ChangeState(IBoatStomperState newState)
    {
        //Debug.Log(newState);
        previousState = currentState;
        currentState?.DoExit(this); // leave the prevvious state
        currentState = newState;
        currentState?.DoEnter(this); // enter the new state   
        
        debugCurrentStateName = currentState.GetType().Name; //used for debuging to see name
        debugPreviousStateName = previousState?.GetType().Name; //used for debuging to see name
    }

    IEnumerator StartSpawnDelay(float min, float max)
    {
        float randy = Random.Range(min,max);
        //Debug.Log(randy);
        yield return new WaitForSeconds(randy);
        active = true;
        agent.enabled = true;
        ChangeState(SpawnState); 
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & playerMask.value) != 0 && active)
        {
            //Debug.Log("Gate 1");
            if(currentState != StunnedState&&currentState!=SpawnState)
            {
                //Debug.Log("Gate 2");
                Vector3 heading = other.transform.position - transform.position;
                direction = heading.normalized;
                //Gizmos.DrawRay(transform.position, direction * .8f);
                RaycastHit hit;
                if(Physics.Raycast(transform.position, direction, out hit,2f,hitMask))
                {// shoot a ray twoards the player

                    //Debug.Log("Gate 3");
                    if(((1 << hit.collider.gameObject.layer) & playerMask.value) != 0 && !contacted)
                    { // if the thing hit is the wall do not go for it.
                    // we have to do this a second time so that we arnt checking eveyry time the guy is near a wall.
                        //Debug.Log("Gate 4");
                        ChangeState(ContactState); // data is being transfered;
                        transform.LookAt(other.transform.position);
                        useControl.ChangeState(useControl.currentItem.UnderAtk);
                        contacted = true;
                    }
                }
                // change player's state to hit
            }
            else
            {
                //rb.isKinematic = true; // player can no longer push my mans around
            }
        }
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction * 2f);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(((1 << collision.gameObject.layer) & edgeMask.value) != 0 && currentState == StunnedState)
        {
            //currentState = SpawnState;
            Spawn();
        }
        if(((1 << collision.gameObject.layer) & playerMask.value) != 0 && currentState == StunnedState){
            rb.isKinematic = true;
        }
    }



    // Getters
    public int GetNumberOfSpots() // # of avalible spots to spawn for the scuba
    {
        return scubaSpots.Length;
    }
    public GameObject GetScubaSpots(int i) // Get a spawn spot from the array by number
    {
        return scubaSpots[i];
    }
    public GameObject GetTarget() // Get the target to follow
    {
        return target;
    }

    // Monster Interface
    public void MonsterHit(Vector3 hitDir) // on hit by harpoon
    {
        this.hitDir = new Vector3(-hitDir.x,1,-hitDir.z);
        currentState = HitState;
    }

    public void SetAnimation(int key)
    {
        anim.ResetTrigger("IsWalking");
        anim.ResetTrigger("IsHit");
        anim.ResetTrigger("IsAttacking");
        anim.ResetTrigger("Arnold");
        anim.ResetTrigger("GetUp");

        switch (key)
        {
            case 0:
                anim.SetTrigger("IsWalking");
                break;

            case 1:
                anim.SetTrigger("IsHit");
                break;
            
            case 2:
               anim.SetTrigger("IsAttacking");
                break;

            case 3:
                anim.SetTrigger("Arnold");
                StartCoroutine(getUp());
                break;
            case 4:
                anim.SetTrigger("GetUp");
                break;
            default:
                break;
        }
        IEnumerator getUp()
        {
            yield return new WaitForSeconds(getUpTime);
            SetAnimation(4);
            yield return new WaitForSeconds(getUpTime);
            ChangeState(MoveState);
        }
    }
}   
