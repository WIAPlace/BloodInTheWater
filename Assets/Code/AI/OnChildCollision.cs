using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChildCollision : MonoBehaviour
{
    [SerializeField] Fish_Controller FSC;
    void OnCollisionEnter(Collision collision)
    {
        FSC.SC.Collision(collision);
    }
}
