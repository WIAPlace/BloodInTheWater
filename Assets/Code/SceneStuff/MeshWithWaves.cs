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
    [Tooltip("The game object holding the transform of what will move with the waves")]
    public Transform UseableMesh;
    [SerializeField][Tooltip("If they should be effected by this.")]
    private bool onWaves = true;
    [SerializeField] [Tooltip("How far under water it should be")]
    private float displacement = 0;
    
    [HideInInspector]
    public Vector3 originalPosition;

    void Start()
    {
        if(UseableMesh == null)
        {
            UseableMesh = transform.GetChild(0).gameObject.transform;
        }
        originalPosition = UseableMesh.localPosition;
    }

    void Update()
    {
        if (WaveManager.Instance != null && UseableMesh != null && onWaves)
        {
            WaveManager.Instance.WaveUpdate(UseableMesh);
            UseableMesh.position += Vector3.down * displacement; 
        }
    }

    public void SetOnWaves(bool setter)
    {
        onWaves = setter;

        if(setter == false)
        {
            UseableMesh.localPosition = originalPosition;
        }
    }
    public GameObject GetFishMesh()
    {
        //UseableMesh.localPosition = originalPosition;
        return UseableMesh.gameObject;
    }
}
