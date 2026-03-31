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
    [SerializeField][Tooltip("Camera For Looking at watch")]
    private CinemachineMixingCamera mixingCamera;
    
    [SerializeField][Tooltip("The pocket watch object itself")]
    private GameObject watch;
    [SerializeField][Tooltip("Min Hand Of the pocket watch")]
    private GameObject hand;

    [SerializeField]
    private Coroutine transitionCoroutine;
    private Coroutine putWatchAway;
    [SerializeField][Tooltip("First Person Camera")]
    private CinemachineInputAxisController inputController;
    // Transition settings
    public float transitionDuration = 1.0f;
    public AnimationCurve blendCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    void Start()
    {
        input.CheckEvent += HandleCheck;
        input.CheckCancelledEvent += HandleCheckCancelled;
        
        mixingCamera.Weight0 = 1;
        mixingCamera.Weight1 = 0;
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
        //SetChildActive(false);
        //TransitionToCamera(1);
        /*
        if(putWatchAway != null)
        {
            StopCoroutine(putWatchAway);
        }
        */
    }
    private void HandleCheckCancelled()
    {
        //putWatchAway = StartCoroutine(PutAwayWatch());
        //TransitionToCamera(0);
        watch.SetActive(false);   
    }
    public void TransitionToCamera(int targetIndex)
    {
        //Debug.Log("TransitionHit");
        // Stop any existing transition coroutine to allow cancellation
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
        
        // Start a new coroutine for the new transition
        transitionCoroutine = StartCoroutine(TransitionCoroutine(targetIndex));
    }

    private IEnumerator TransitionCoroutine(int targetIndex)
    {
        // Store current weights
        float[] startWeights = new float[mixingCamera.ChildCameras.Count];
        for (int i = 0; i < mixingCamera.ChildCameras.Count; i++)
        {
            startWeights[i] = mixingCamera.GetWeight(i);
        }

        float timer = 0f;
        while (timer < transitionDuration)
        {
            //Debug.Log("Hit");
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / transitionDuration);
            float curveValue = blendCurve.Evaluate(progress);

            for (int i = 0; i < mixingCamera.ChildCameras.Count; i++)
            {
                // Interpolate weights: target camera goes from startWeight to 1, others go to 0
                if (i == targetIndex)
                {
                    mixingCamera.SetWeight(i, Mathf.Lerp(startWeights[i], 1f, curveValue));
                }
                else
                {
                    mixingCamera.SetWeight(i, Mathf.Lerp(startWeights[i], 0f, curveValue));
                }
            }

            yield return null;
        }
        
        // Ensure final weights are exact
        for (int i = 0; i < mixingCamera.ChildCameras.Count; i++)
        {
            mixingCamera.SetWeight(i, (i == targetIndex) ? 1f : 0f);
        }
        if(mixingCamera.Weight0 == 1)
        {
            SetChildActive(true);
        }
        transitionCoroutine = null; // Mark coroutine as finished
    }

    public void SetChildActive(bool active)
    {
        if (inputController != null)
        {
            // Disabling the controller stops it from reading input
            inputController.enabled = active;
        }
    }
    IEnumerator PutAwayWatch()
    {
        yield return new WaitForSeconds(transitionDuration);
        watch.SetActive(false);
    }
}
