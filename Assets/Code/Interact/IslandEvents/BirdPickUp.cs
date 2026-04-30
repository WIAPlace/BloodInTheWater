using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

//[RequireComponent(typeof(SplineAnimate))]
public class BirdPickUp : MonoBehaviour,IInteractable
{
    //SplineAnimate splan;
    [SerializeField] SplineContainer splineCan;
    [SerializeField] BarrelOfBirds bob;
    [SerializeField,Tooltip("When interacted with set new layer to this")] string newLayer;
    int layerIndex;
    void Start()
    {
        layerIndex = LayerMask.NameToLayer(newLayer);
        //splan = GetComponent<SplineAnimate>();
        bob.pickUpReached += HandlePickUpReached;
    }
    void OnDestroy()
    {
        bob.pickUpReached -= HandlePickUpReached;
    }
    public void Interact()
    {
        gameObject.layer = layerIndex; // make un interactable

        var barrelSpline = splineCan.Spline; // get spline attached to container
        Vector3 localPos = splineCan.transform.InverseTransformPoint(transform.position);
        // get local position 

        BezierKnot knot = barrelSpline[0]; // set knot 0 to a setable format
        knot.Position = localPos; // new knot is equal to the position of the bird
        barrelSpline.SetKnot(0,knot); // set knot 0 as the new knot

        //splan.Play(); // play animation
        bob.BirdPickedUp(gameObject); // add one to the current birds picked up 
    }
    private void HandlePickUpReached()
    {
        gameObject.layer = layerIndex; // make un interactable
    }
}
