using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderColapse : MonoBehaviour
{
    [SerializeField] Collider triggerCollider;
    void OnTriggerEnter(Collider other)
    {
        triggerCollider.enabled = false;
    }
}
