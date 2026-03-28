using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TestReelLine : MonoBehaviour
{
    public SplineContainer mySpline;
    public Transform rodTip;
    public Transform midPoint;
    public Transform lureTip;
    private int newRot = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mySpline !=null)
        {
            //
            if(lureTip!=null) UpdateKnotPosition(0,0,lureTip.position);
            if(midPoint!=null) UpdateUP(0,1); //UpdateKnotPosition(2,rodTip.position);

            if(midPoint!=null) UpdateUP(1,0);
            if(rodTip!=null) UpdateUP(1,1);

            //UpdateKnotPosition(1,lureTip.position - rodTip.position, lureTip.rotation);
            //if(rodTip!=null) UpdateMiddlePosition(1);
        }
        
    }
    public void UpdateKnotPosition(int splineIndex, int knotIndex, Vector3 newWorldPosition)
    {
        // 1. Get the current spline reference
        var spline = mySpline.Splines[splineIndex];

        // 2. Get the specific knot's data (SplinePoint struct)
        // Note: You can also use mySpline.Spline.ToArray()[knotIndex]
        var knot = spline[knotIndex];

        // 3. Convert world position/rotation to local space relative to the SplineContainer
        // This is crucial for correct positioning within the container's hierarchy
        knot.Position = mySpline.transform.InverseTransformPoint(newWorldPosition);
        knot.Rotation = Quaternion.Inverse(mySpline.transform.rotation);

        // 4. Set the modified knot back to the spline
        mySpline.Splines[splineIndex].SetKnot(knotIndex, knot);
        
        // Note: The tangents (AnterierTangent, PosteriourTangent) can also be modified similarly if needed.
    }
    public void UpdateUP(int splineIndex,int knotIndex)
    {
        var knot = mySpline.Splines[splineIndex][knotIndex];
        
        knot.Position = mySpline.transform.InverseTransformPoint(midPoint.position);

        if(splineIndex == 1 && knotIndex == 1)
        {   // if its the one that should be a at the tip of the rod
            //Debug.Log("Hit");
            knot.Position = mySpline.transform.InverseTransformPoint(rodTip.position);
            Vector3 rot = new Vector3(270,0,180 + newRot); // set it to face directly up
            knot.Rotation = Quaternion.Euler(rot);
        }
        mySpline.Splines[splineIndex].SetKnot(knotIndex, knot);
    }

    public void setRot(int rot)
    {
        newRot=rot;
    }
}
