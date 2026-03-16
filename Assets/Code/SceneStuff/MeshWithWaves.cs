using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/15/26
/// Purpose: Move Along with the waves.
/// 
public class MeshWithWaves : MonoBehaviour
{
    [SerializeField]
    private Transform UseableMesh;

    [SerializeField]
    private float smoothTime = .1f;

    private float yVelocity = 0f;


    void Update()
    {
        WaveManager.Instance.WaveUpdate(UseableMesh,yVelocity,smoothTime);
    }
}
