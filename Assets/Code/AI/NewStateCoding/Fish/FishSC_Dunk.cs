using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class FishSC_Dunk : FishSC_Abstact
{
    public LayerMask BoatMask;
    public Transform BoatObj;
    [SerializeField] private SplineContainer circleSpline;
    private float distanceTravelled=0f;
    

    [SerializeField] [Tooltip("In seconds")]
    private float Damage=5;
    
    public void Awake()
    {
        //target = GameManager.Instance.lureTarget;
        FSC = GetComponent<Fish_Controller>();
        Idle = new Dunk_StateIdle();
        Lure = new Dunk_StateLure();
        Bobber = new Dunk_StateBobber();
        Fear = new Dunk_StateFear();
        Unique = new Dunk_StateUnique();
        Enter = new Dunk_StateEnter();
        Hook = new Dunk_StateHooked();
        Line = new Dunk_StateOnLine();
    }

    public override void BobberSpooked(Vector3 lurePosition)
    {
        if(FSC.SC.Fear!=null && FSC.currentState == FSC.SC.Unique) // turns off idle.
        {
            FSC.ChangeState(FSC.SC.Fear);
            FSC.lurePos = lurePosition;
        }
    }

    public override void LureReeledIn()
    {
        
    }

    ///////////////////////////////////////////////////////////////////////// Trigger Functions
    void OnTriggerEnter(Collider other)
    { // when the fish enters the lures trigerzone  
        //Debug.Log("entered");     
        if (FSC.SC.Lure!=null && ((1 << other.gameObject.layer) & FSC.targetMask.value) != 0)
        { // if the trigger is the bobber's lure layermask and able to be lured.
            //Debug.Log("entered");
            if(FSC.currentState == FSC.SC.Unique)
            {
                FSC.ChangeState(FSC.SC.Fear); // run away only if it is coming at u.
            }
        }
    }

    public override Vector3 GetRamTarget(Fish_Controller FSC)
    {
        return BoatObj.position;
    }

    public override void IdleMovement(Fish_Controller FSC)
    {
        if (circleSpline != null)
        {
            // Update the distance travelled based on speed and time
            distanceTravelled += FSC.agent.speed * Time.deltaTime;
            //

            //Debug.Log(distanceTravelled);
            // Optional: for looping behavior on a closed path
            float splineLength = circleSpline.CalculateLength();
            distanceTravelled %= splineLength;

            float posOnSpline = distanceTravelled/splineLength;

            // Get the position and rotation along the spline based on the distance
            Vector3 position = circleSpline.EvaluatePosition(posOnSpline);
            Vector3 forward = circleSpline.EvaluateTangent(posOnSpline);
            Vector3 up = circleSpline.EvaluateUpVector(posOnSpline);

            // Set the rotation to align with the spline's direction and up vector
            FSC.transform.rotation = Quaternion.LookRotation(forward, up);
            FSC.transform.position = position;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(((1 << collision.gameObject.layer) & BoatMask.value) != 0 && FSC.currentState == FSC.SC.Unique)
        {
            //Debug.Log("Collision");
            GameManager.Instance.OnBoatHit(Damage);
            StartCoroutine(CollisionShock(FSC));
        }
    }
    private IEnumerator CollisionShock(Fish_Controller FSC)
    {
        FSC.ChangeState(FSC.SC.Line);
        yield return new WaitForSeconds(.2f);
        FSC.ChangeState(FSC.SC.Fear);
    }

    public override IFishState MoveBackToIdle(Fish_Controller FSC)
    {
        float3 pos = FSC.transform.position;
        float3 nearPoint;
        SplineUtility.GetNearestPoint(
            circleSpline.Spline,
            pos,
            out nearPoint,
            out float t // 't' is the normalized distance along the spline
        );
        
        Vector3 nearestPoint = nearPoint;

        // Calculate the direction to the nearest point and move the object
        float distance = Vector3.Distance(FSC.transform.position,nearestPoint);
        Vector3 direction = nearestPoint - FSC.transform.position;

        // move twoards the nearest point over time
        FSC.transform.position = Vector3.MoveTowards(FSC.transform.position, nearestPoint, FSC.agent.speed * Time.deltaTime); 
        // look at the target point over time
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        FSC.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, FSC.agent.angularSpeed * Time.deltaTime);

        // when a certain distance from the spline get back on it
        if(distance <= .4f)
        {
            //Debug.Log(t);
            distanceTravelled = t*circleSpline.CalculateLength();
            return FSC.SC.Idle;
        }
        else return FSC.SC.Fear;
    }
}
