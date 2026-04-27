using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
/// 
/// Author: Weston Tollette
/// Created: 2/7/26
/// Purpose: This wont be in the final build, this is just to beable to see where the lure zone is for testings sake.
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public class TestLureRadius : MonoBehaviour
{
    [SerializeField]private float secondsRipple;
    [SerializeField]private GameObject ripplePrefab;
    private Coroutine running;
    //GameObject rippleHolder;
    //GameObject rippleHolder;
    void Start()
    {
        
        //Debug.Log("enabled");
        SphereCollider lureZone = GetComponent<SphereCollider>(); // the sphere collider trigger
        Transform bobberCenter = GetComponent<Transform>(); // Center of bobber 
        float lureRadius = lureZone.radius; // Get the radius of the Lure Zone 

        /*
        // Create a new GameObject to hold the spline
        GameObject splineGO = new GameObject("RingSpline");
        splineGO.transform.position = bobberCenter.position;

        splineGO.transform.parent = this.transform;

        // Add the SplineContainer component
        SplineContainer container = splineGO.AddComponent<SplineContainer>();
        
        //Get Spline Extrude
        SplineExtrude thickness = splineGO.AddComponent<SplineExtrude>();
        thickness.Container=container;
       
        
        // Create a circular spline (closed)
        Spline spline = container.Spline;
        spline.Clear();
        
        // how many segments there are in this shape.
        int knots = 8;
        for (int i = 0; i < knots; i++)
        { // add the splines around the center in a circular pattern
            float angle = i * Mathf.PI * 2 / knots;
            Vector3 pos = new Vector3(Mathf.Cos(angle) * lureRadius, 0, Mathf.Sin(angle) * lureRadius);
            
            // Add knot with tangent (Bezier)
            spline.Add(new BezierKnot(pos), TangentMode.AutoSmooth);
        }
        if (thickness != null)
        {
            // Set the radius (thickness)
            thickness.Radius = .1f; // thickness of the spline itself
            thickness.SegmentsPerUnit = 1; 
        }
        // Close the spline
        spline.Closed = true;
        */
    }
    private void OnEnable()
    {
        running = StartCoroutine(Ripple());
    }
    private void OnDisable()
    {
        if(running != null)
        {
            StopCoroutine(running);
        }
    }
    IEnumerator Ripple()
    {
        yield return new WaitForEndOfFrame();
        GameObject rippleHolder = null;
        try
        {
            while (true)
            {
                rippleHolder = Instantiate(ripplePrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(secondsRipple);
                Destroy(rippleHolder);
                rippleHolder = null; // Clear reference after successful destruction
            }
        }
        finally
        {
            // This runs even if StopCoroutine() is called while waiting
            if (rippleHolder != null)
            {
                Destroy(rippleHolder);
            }
        }
    }
}
