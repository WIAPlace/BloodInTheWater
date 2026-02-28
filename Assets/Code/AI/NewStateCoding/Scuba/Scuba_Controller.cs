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
    [SerializeField][Tooltip("Target for the navmesh")]
    private GameObject target; // should be the player
    private IBoatStomperState currentState;
    [SerializeField][Tooltip("Player layer")]
    private LayerMask playerMask;
    [SerializeField][Tooltip("Player StateController script")]
    private Useable_Controller useControl;

    public QuickTimeData_Scuba scubaData; // stuff for data minigame

    public NavMeshAgent agent; // the part that does the AI.
    public float secondsStunned;
    
    // All of the States
    public Scuba_StateSpawn SpawnState = new Scuba_StateSpawn(); // when called spawn at a random spot out of avalible ones
    public Scuba_StateMove MoveState = new Scuba_StateMove(); // move twoards player
    public Scuba_StateContact ContactState = new Scuba_StateContact(); // on contact start the minigame
    public Scuba_StateBreakOff BreakOffState = new Scuba_StateBreakOff(); // Activated after breaking out of minigame.
    public Scuba_StateStunned StunnedState = new Scuba_StateStunned(); // After breaking out of minigame he will go into stunned for a moment


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // set agent to this part of the component.
        scubaData = GetComponent<QuickTimeData_Scuba>();
        //gameObject.SetActive(false); // start out false because he will be activated in spawn state.
        currentState = SpawnState; 
        // scuba will have to be activated by something outside itself, because the update wont run if it is disabled
    }

    private void Update()
    {
        if(currentState!=null)
        { // set current state to whatever the state tells you to
            currentState = currentState.DoState(this);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & playerMask.value) != 0 && currentState != StunnedState)
        {
            currentState = ContactState; // data is being transfered;
            
            useControl.ChangeState(useControl.currentItem.UnderAtk);
            // change player's state to hit
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
    public void SetCurrentState(IBoatStomperState newState) // set state to something else.
    { // transition used to change state to new state.
        currentState = newState;
    }



    // Monster Interface
    public void MonsterHit(Vector3 hitDir) // on hit by harpoon
    {
        Debug.Log("MonsterHit");
    }
}   
