using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 2/7/26
/// Purpose: Fish should move twoards the bobber when it is in range.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class FishNavToRod : MonoBehaviour
{
    [SerializeField] [Tooltip("The target to move twoards")]
    private Transform target;

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
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("exited");
        agent.SetDestination(transform.position);
    }
}
