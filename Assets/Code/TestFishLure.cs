using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //private GameObject lureInstance; 
    // this will be used to hold the lure in world space, 
    // its a seprate object from prefab so we can check if it is existing, if so we will delete it on click,
    // if not we will create on in the location the player is looking.

    private void Start()
    {
        input.InteractEvent += HandleInteract;
        lurePrefab.SetActive(false); // 
    }


    
    private void HandleInteract() // When you click the left mouse button
    { 
        if(lurePrefab.activeSelf)
        { // This will asume the lure has no childern.
            lurePrefab.SetActive(false); // disable Lure
            return; // End here cause we dont need to do the rest of the script.
        }// possibly look into Object pooling, might not be usefull now since i decided against just destroying and remaking it every time.

        RaycastHit hit; // raycast will work as a line of sight to make contact with closest point on ground/water. 
        //                                                      // Should probably implement a layermask for water.
        
        // Cast a ray forward from the Camera position and rotation
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            lurePrefab.SetActive(true); // activate the lure.
            lurePrefab.transform.position = hit.point; // move the lure to the point that the raycast hit.
        }
    }
}
