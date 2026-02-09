using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 2/8/26
/// Purpose: Fish should move twoards the bobber when it is in range.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class FishNavToRod : MonoBehaviour
{
    

    private bool inLureRange; // This will be used to check if the the fish is in the lure range

    private NavMeshAgent agent; // the part that makes this AI.

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InLureZone(Transform targetLure) // utilized by: fish state tracker script to pass the lures transform.
    {
        if (agent != null && agent.enabled)
        { // checks to make sure agent exists
            agent.isStopped = false; // reactivates the agent if its stoped.
            agent.SetDestination(targetLure.position);
        }
    }
    public void LureReeledIn()
    {
        if (agent != null && agent.enabled)
        { // checks to make sure agent exists
            agent.isStopped = true; // reactivates the agent if its stoped.
            agent.SetDestination(transform.position);
        }
    }
}
