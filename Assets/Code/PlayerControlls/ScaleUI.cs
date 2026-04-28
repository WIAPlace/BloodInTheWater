using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ScaleUI : MonoBehaviour
{
    [SerializeField][Tooltip("Scale game object")]
    GameObject scale;
    [SerializeField][Tooltip("Scale weight marker")]
    GameObject marker;
    [SerializeField][Tooltip("Transform of min")]
    Transform minMarker;
    [SerializeField][Tooltip("Transform of max marker position")]
    Transform maxMarker;
    [SerializeField][Tooltip("Current Scale amount")]
    float scaleAmnt;
    [SerializeField][Tooltip("Duration")]
    float duration;
    [SerializeField][Tooltip("Curve for animations")]
    private AnimationCurve curve;

    [SerializeField][Tooltip("Scale Object")]
    private Renderer scaleRenderer;
    [SerializeField][Tooltip("Marker Object")]
    private Renderer markerRenderer;
    [SerializeField][Tooltip("CompleteMaterial")]
    private Material fullMat;

    private float neededAmnt;
    private float amntJustCaught;
    private bool full;
    private Coroutine running; 

    // Start is called before the first frame update
    void Start()
    {
        // make sure stuff is strting in correct active states
        scale.SetActive(false);
        marker.SetActive(true);
        minMarker.gameObject.SetActive(false);
        maxMarker.gameObject.SetActive(false);
        full = false;


        GameState.OnCatch += HandleCatch;
        neededAmnt = GameState.Instance.CheckLbsNeeded();
        scaleAmnt = 0;
        marker.transform.localPosition = minMarker.localPosition;
    }
    void OnDestroy()
    {
        GameState.OnCatch -= HandleCatch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void HandleCatch(float firstWait)
    {
        if(running != null)
        {
            StopCoroutine(running);
        }
        running = StartCoroutine(ShowScale(firstWait));
        if (!full && scaleAmnt >= neededAmnt)
        {
            full = true;
            scaleRenderer.material = fullMat;
            markerRenderer.material = fullMat;
        }
    }

    IEnumerator ShowScale(float firstWait)
    {
        float lbs = GameState.Instance.CheckLbs();
        scaleAmnt = lbs/neededAmnt; // set scale amnt to 
        yield return new WaitForSeconds(firstWait); // about as much time as it takes to reel in a fish.

        scale.SetActive(true); // set scale active
        yield return new WaitForSeconds(.2f); // wait a beat before doing anything
        float timeElapsed = 0;
        Vector3 goal = Vector3.Lerp(minMarker.localPosition, maxMarker.localPosition, scaleAmnt);
        Vector3 start = marker.transform.localPosition;

        while (timeElapsed < duration)
        {
            // progress goes from 0 to 1
            float t = timeElapsed / duration;
            marker.transform.localPosition = Vector3.Lerp(start,goal,curve.Evaluate(t));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        marker.transform.localPosition = goal;

        yield return new WaitForSeconds(.6f); // wait a beat before disapearing
        scale.SetActive(false);
    }
}
