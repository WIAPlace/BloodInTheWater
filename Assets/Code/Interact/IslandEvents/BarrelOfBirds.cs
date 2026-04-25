using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Splines;

public class BarrelOfBirds : MonoBehaviour
{
    //[SerializeField]SplineContainer barrelSpline;
    [SerializeField,Tooltip("Gulls needed to be picked up to proceed")]
    private int gullsNeeded;
    private int gullPicked=0;

    [SerializeField] GameObject emptyBird;
    [SerializeField] SplineAnimate emptyAnim;

    

    public event Action pickUpReached;

    // Start is called before the first frame update
    void Start()
    {
        gullPicked=0;
    }
    void Update()
    {
        if(emptyAnim.ElapsedTime >= 1)
        {
            foreach(Transform child in emptyBird.transform)
            {  // destroy the children objects
               GameObject.Destroy(child.gameObject); 
            }
        }
    }

    public void BirdPickedUp(GameObject pickedUpBird)
    {
        gullPicked += 1;//add one for each gull pickedup

        // set position of animator to object so there is no ofset
        //emptyBird.transform.position = pickedUpBird.transform.position;

        emptyAnim.Restart(false); // do not auto play
        
        // set this as child so it will move with the animator.
        GameObject parentBird = pickedUpBird.transform.parent.gameObject;
        parentBird.transform.SetParent(emptyBird.transform);
        
        emptyAnim.Play();

        //if less than needed do nothing
        if(gullPicked < gullsNeeded) return;
        pickUpReached.Invoke(); 
        // activate the event when gull picked up is reached.
    }
}
