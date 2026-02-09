using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 2/8/26
/// Purpose: Fish should move when just hanging aroundg
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class FishIdleMove : MonoBehaviour
{
    [SerializeField] [Tooltip("Radius of wandering area")]
    private float wanderRadius = 10f; 

    [SerializeField][Tooltip("Time to wait before choosing a new point")]
    private float wanderTimer = 5f;   


    private bool isActive = true;
    private NavMeshAgent agent;
    private float timer;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(WanderRoutine());
    }

    private IEnumerator WanderRoutine()
    {
        while(isActive)
        {
            Vector3 newPos = RandomNavMeshLocation(transform.position, wanderRadius);
            agent.SetDestination(newPos);
                
            // Wait until agent is close to destination or timer runs out
            yield return new WaitForSeconds(wanderTimer);
            //Debug.Log(wanderTimer);
        }
    }

    // Finds a valid NavMesh point near the center point
    private Vector3 RandomNavMeshLocation(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        NavMeshHit hit;
        
        // Sample position to make sure it is on the NavMesh
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        return hit.position;
    }

    public void IdleStateActive()
    {
        if (agent != null && agent.enabled)
        { // checks to make sure agent exists
            agent.isStopped = false; // reactivates the agent if its stoped.
            isActive = true;
            StartCoroutine(WanderRoutine());
        }
    }
    public void IdleStateInactive()
    {
        if (agent != null && agent.enabled)
        { // checks to make sure agent exists
            agent.isStopped = true; // deactivates agent
            isActive = false;
        }
    }
}
