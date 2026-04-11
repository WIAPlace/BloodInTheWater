using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// weston  tollette
// this is for when the sea angel latches onto the face of the player
public class LatchedOnFace : MonoBehaviour
{
    [SerializeField] [Tooltip("Sea angel on face spot")]
    private GameObject angelSpot;

    void Start()
    {
        GameManager.OnLatch += HandleLatch;
        GameManager.OnLatchCancelled += HandleLatchCancelled;
        //input.CheckEvent += HandleCheck;
        //input.CheckCancelledEvent += HandleCheckCancelled;
        angelSpot.SetActive(false);
    }
    void OnDestroy()
    {
        GameManager.OnLatch -= HandleLatch;
        GameManager.OnLatchCancelled -= HandleLatchCancelled;
        //input.CheckEvent -= HandleCheck;
        //input.CheckCancelledEvent -= HandleCheckCancelled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleLatch()
    {
        angelSpot.SetActive(true);
    }
    private void HandleLatchCancelled()
    {
        angelSpot.SetActive(false);
    }
}
