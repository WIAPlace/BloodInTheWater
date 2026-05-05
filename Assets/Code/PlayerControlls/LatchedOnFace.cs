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
    [SerializeField] [Tooltip("Back")]
    private GameObject back;
    [SerializeField] [Tooltip("Layer of boat Edge")]
    private LayerMask edgeMask;
    void Start()
    {
        GameManager.OnLatch += HandleLatch;
        GameManager.OnLatchCancelled += HandleLatchCancelled;
        //input.CheckEvent += HandleCheck;
        //input.CheckCancelledEvent += HandleCheckCancelled;
        back.SetActive(false);
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

    public void Triggered(Collider other)
    {
        Debug.Log(other.gameObject.layer);

        if(((1 << other.gameObject.layer) & edgeMask.value) != 0)
        {
            Debug.Log("Triggered");
            //Debug.Log("Trogged");
            GameState.Instance.LooseState("Lose_Angel");

        }
    }

    private void HandleLatch()
    {
        angelSpot.SetActive(true);
        back.SetActive(true);
    }
    private void HandleLatchCancelled()
    {
        angelSpot.SetActive(false);
        back.SetActive(false);
    }
}
