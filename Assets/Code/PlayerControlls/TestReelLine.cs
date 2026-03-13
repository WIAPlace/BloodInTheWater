using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TestReelLine : MonoBehaviour
{
    public SplineContainer mySpline;
    public Transform rodTip;
    public Transform lureTip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mySpline !=null)
        {
            if(rodTip!=null) UpdateUP(); //UpdateKnotPosition(2,rodTip.position);
            if(lureTip!=null) UpdateKnotPosition(0,lureTip.position);
            //UpdateKnotPosition(1,lureTip.position - rodTip.position, lureTip.rotation);
            //if(rodTip!=null) UpdateMiddlePosition(1);
        }
        
    }
    public void UpdateKnotPosition(int knotIndex, Vector3 newWorldPosition)
    {
        // 1. Get the current spline reference
        var spline = mySpline.Spline;

        // 2. Get the specific knot's data (SplinePoint struct)
        // Note: You can also use mySpline.Spline.ToArray()[knotIndex]
        var knot = spline[knotIndex];

        // 3. Convert world position/rotation to local space relative to the SplineContainer
        // This is crucial for correct positioning within the container's hierarchy
        knot.Position = mySpline.transform.InverseTransformPoint(newWorldPosition);
        knot.Rotation = Quaternion.Inverse(mySpline.transform.rotation);

        // 4. Set the modified knot back to the spline
        mySpline.Spline.SetKnot(knotIndex, knot);
        
        // Note: The tangents (AnterierTangent, PosteriourTangent) can also be modified similarly if needed.
    }
    public void UpdateUP()
    {
        var spline = mySpline.Spline;

        var knot = spline[2];
        knot.Position = mySpline.transform.InverseTransformPoint(rodTip.position);
        Vector3 rot = new Vector3(270,0,270); // set it to face directly up
        knot.Rotation = Quaternion.Euler(rot);
        mySpline.Spline.SetKnot(2, knot);
    }
}
