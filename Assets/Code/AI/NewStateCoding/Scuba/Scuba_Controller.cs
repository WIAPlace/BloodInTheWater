using UnityEngine;
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
public class Scuba_Controller : MonoBehaviour
{
    [SerializeField][Tooltip("The places he can spawn")]
    private GameObject[] scubaSpots;
    [SerializeField][Tooltip("Target for the navmesh")]
    private GameObject target; // should be the player
    private IBoatStomperState currentState;

    public NavMeshAgent agent; // the part that does the AI.
    
    // All of the States
    public Scuba_StateSpawn SpawnState = new Scuba_StateSpawn();
    public Scuba_StateMove MoveState = new Scuba_StateMove();


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // set agent to this part of the component.
        currentState = SpawnState;
    }

    private void Update()
    {
        if(currentState!=null)
        {
            currentState = currentState.DoState(this);
        }
    }






    // Getters
    public int GetNumberOfSpots()
    {
        return scubaSpots.Length;
    }
    public GameObject GetScubaSpots(int i)
    {
        return scubaSpots[i];
    }
    public GameObject GetTarget()
    {
        return target;
    }
}   
