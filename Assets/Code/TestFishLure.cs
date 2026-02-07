using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 2/6/26
/// Purpose: Test for the fish Lure mechanic.
///     Definitly not final, mainly this will just spawn and despawn it 
///     This should be attached to the camera for now
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 
public class TestFishLure : MonoBehaviour
{
    [SerializeField] [Tooltip("Insert the Scriptable Object Input Reader")]
    private InputReader input;

    [SerializeField] [Tooltip("The Prefab For the Lure")]
    private GameObject lurePrefab; // The Prefab for the lure object.
    // make sure not to destroy this.

    [SerializeField] [Tooltip("The Prefab for the Cast Spot Indicator")]
    private GameObject castSpotPrefab; 

    [SerializeField] [Tooltip("Line For Line Rendering under the casting bit")]
    private LineRenderer lineIndicatior;

    [SerializeField] [Tooltip("Layer for Water")]
    private LayerMask waterMask;

    [Header("Rod Variables")]
    [SerializeField] [Tooltip("Max Cast Distance")]
    private float castRange;

    [SerializeField] [Tooltip("Cast Variance Range")]
    private float castVary;

    [SerializeField] [Tooltip("Cast Ping Pong Speed")]
    private float pongSpeed;

    private bool holdToCast = false; // This bool will tell if the player is holding down the cast trigger, used to see if the cast has occured or not yet.

    private float currentCastValue; // the value of the cast at a certain point

    private void Start()
    {
        input.InteractEvent += HandleInteract;
        input.InteractCancelledEvent += HandleInteractCancelled;
        castSpotPrefab.SetActive(false); // Set inactive at the start cause why not
    }

    void Update()
    {
        if (holdToCast)
        { 
            HoldCasting();
        }
    }

    private void HoldCasting() // While cast trigger is down, this will be activating. 
    {
        RaycastHit hit; // raycast will work as a line of sight to make contact with closest point on ground/water. 
        //                                                      // Should probably implement a layermask for water.
        
        // Cast a ray forward from the Camera position and rotation
        if (Physics.Raycast(transform.position, transform.forward, out hit, castRange, waterMask))
        {
            if(!castSpotPrefab.activeSelf)
            {// if lure is not already activated
                castSpotPrefab.SetActive(true); // activate the lure.
            }
            lineIndicatior.enabled = true; // set the line indicator true so it can be seen.

            
            //float castVaryRelRange = castVary/2 * (hit.distance/testDivRange); //Cast variance relative range, used for -1/2 itself to posative 1/2 itself.
            float numInRange = Mathf.PingPong(Time.time * pongSpeed, castVary); // return a number between 0 and cast range
            
            currentCastValue = -castVary/2+numInRange; // Adds the offset for to the number in the range.

            Vector3 forward3D = transform.forward; // get the forward direction
            Vector3 forward2D = new Vector3 (forward3D.x, 0f, forward3D.z); // Change it so that it is only important on the flat plane
            forward2D.Normalize(); // Normalize it so its magnitude 1;

            castSpotPrefab.transform.position = hit.point + forward2D * currentCastValue; 
            // Originating from where raycast was hit, in the direction that is forward to the player, along the line.
        }
    }


    private void CastLure() 
    {
        lurePrefab.SetActive(true); 
        lurePrefab.transform.position = castSpotPrefab.transform.position; 
        // set the lure position to the position that the indicator used to be.
    }

    
    private void HandleInteract() // When you click the left mouse button
    {
        if (lurePrefab.activeSelf)
        {// if there is a lure already out only get rid of it, dont start the cast process again.
            lurePrefab.SetActive(false);
            return;
        }
        holdToCast = true;

        if(castSpotPrefab.activeSelf)
        { // This will asume the lure has no childern.
            castSpotPrefab.SetActive(false); // disable Lure
            return; // End here cause we dont need to do the rest of the script.
        }// possibly look into Object pooling, might not be usefull now since i decided against just destroying and remaking it every time.
        castSpotPrefab.SetActive(true);
    }
    private void HandleInteractCancelled() // when release left mouse
    {
        if(holdToCast)
        { // if the cast indicator is around.
            holdToCast = false;
            castSpotPrefab.SetActive(false);
            lineIndicatior.enabled = false; // set the line indicator false so it cant be seen.
            CastLure();
        }
    }
}
