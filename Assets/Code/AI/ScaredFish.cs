using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 2/9/26
/// Purpose: If the bobber lands to close to the fish it will scare them off.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class ScaredFish : MonoBehaviour
{
    [SerializeField][Tooltip("Time to wait calming down.")]
    private float FearTimer = 5f; 
    private NavMeshAgent agent;
    private float fleeDistance = 20f;
    private FishStateController fishState;

    void Start()
    {
        fishState = GetComponent<FishStateController>(); //shame i have to use this here
        agent = GetComponent<NavMeshAgent>();
    }
    public IEnumerator FearTheBobber(Vector3 lurePosition)
    { // will run away for a bit.
        if (agent != null && agent.enabled)
        {
            agent.isStopped = false;
                
            Vector3 fleeDir = transform.position - lurePosition; // get the diffence of this object and 
            Vector3 potentialFleePosition = transform.position + fleeDir.normalized * fleeDistance; // get a desired place to flee to
            NavMeshHit hit;
            //Sample the NavMesh to find the nearest valid point to the potential flee position
            if (NavMesh.SamplePosition(potentialFleePosition, out hit, fleeDistance, NavMesh.AllAreas))
            {
                // Set the agent's destination to the valid point on the NavMesh
                agent.SetDestination(hit.position);
                //Debug.Log(hit.position);
            }
        }
        yield return new WaitForSeconds(FearTimer);
        //Debug.Log("EngedWait"); // checking to see how long the wait was, i had accidently put it at like 2 seconds.
        agent.isStopped = true;
        fishState.EndSpook();
    }
}
