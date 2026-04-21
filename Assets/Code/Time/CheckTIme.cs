using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 3/29/26
/// Purpose: Check the time you have on the pocket watch
/// 
/// Edited: 
/// Edited By:
/// Edit Purpose:
/// 
public class CheckTIme : MonoBehaviour
{   
    [SerializeField]
    private InputReader input;
    
    
    [SerializeField][Tooltip("The pocket watch object itself")]
    private GameObject watch;
    [SerializeField][Tooltip("Min Hand Of the pocket watch")]
    private GameObject hand;

    void Start()
    {
        input.CheckEvent += HandleCheck;
        input.CheckCancelledEvent += HandleCheckCancelled;
        watch.SetActive(false);
    }
    void OnDestroy()
    {
        input.CheckEvent -= HandleCheck;
        input.CheckCancelledEvent -= HandleCheckCancelled;
    }
    // Update is called once per frame
    void Update()
    {
        if (watch.activeSelf)
        {
            float timeLeft = GameManager.Instance.CheckTime();

            hand.transform.localRotation = Quaternion.Euler(
                hand.transform.rotation.x,
                hand.transform.rotation.y+timeLeft,
                hand.transform.rotation.z
                );
            // rotate the hand's y rot around to match with how much time has passed.
        }
        
    }

    private void HandleCheck()
    {
        watch.SetActive(true);
    }
    private void HandleCheckCancelled()
    {
        watch.SetActive(false);   
    }
    
}
