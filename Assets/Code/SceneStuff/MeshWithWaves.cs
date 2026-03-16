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
    [SerializeField] [Tooltip("The game object holding the transform of what will move with the waves")]
    private Transform UseableMesh;
    [SerializeField] [Tooltip("How far under water it should be")]
    private float displacement = 0;

    void Update()
    {
        if (UseableMesh != null)
        {
            WaveManager.Instance.WaveUpdate(UseableMesh);
            UseableMesh.position += Vector3.down * displacement; 
        }
    }
}
