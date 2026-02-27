using UnityEngine;
using UnityEngine.Animations;
/// 
/// Author: Weston Tollette
/// Created: 2/26/26
/// Purpose: Holder for Rod Item States
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///


public class Rod_StateItemIdle : Abs_StateItemIdle
{
    public override void DoEnter(Useable_Controller controller)
    {
        if (controller.rod.CheckIfFishing())
        { // if fishing
            // do the fishing animation
        }
        else
        {
            // start the idle holding animation
        }
    }

    public override void DoExit(Useable_Controller controller)
    {
        
    }
}

////////////////////////////////////////////////////// Readying /////////////////////////////////////////////
public class Rod_StateItemReadying : Abs_StateItemReadying
{ 
    // if fishing readying will be reeling in the fish, else it will be readying to cast.
    /*
    public override void DoEnter(Useable_Controller controller)
    {
        Debug.Log("Rod Readying Entered");
    }

    public override void DoExit(Useable_Controller controller)
    {
        Debug.Log("Rod Readying Exited");
    }
    */
}

////////////////////////////////////////////////////// Is Ready //////////////////////////////////////////////
public class Rod_StateItemIsReady : Abs_StateItemIsReady
{
    private GameObject obj; // controllers game object
    private UseableItem_Rod rod; // rod item script
    public override void DoEnter(Useable_Controller controller)
    {
        obj = controller.gameObject;
        rod = controller.rod;
        if (rod.CheckIfFishing())
        {
            controller.ChangeState(controller.currentItem.UseItem);
        }
    }

    public override void DoExit(Useable_Controller controller)
    {
        
    }
    public override IUseableState DoState(Useable_Controller controller)
    {
        if(!rod.CheckIfFishing())
        {
            HoldCasting(controller);
        }

        return controller.currentItem.IsReady;
    }

    private void HoldCasting(Useable_Controller con) // While cast trigger is down, this will be activating. 
    {
        // raycast will work as a line of sight to make contact with closest point on ground/water. 
        //                                                      // Should probably implement a layermask for water.
    
        // Cast a ray forward from the Camera position and rotation
        if (Physics.Raycast(obj.transform.position, obj.transform.forward, out RaycastHit hit, rod.CastRange, rod.WaterMask))
        {
            if (!con.rod.CastSpotPrefab.activeSelf)
            {// if lure is not already activated
                rod.CastSpotPrefab.SetActive(true); // activate the lure.
                rod.GetInRange = true;
            }

            float numInRange = Mathf.PingPong(Time.time * rod.PongSpeed, rod.CastVary); // return a number between 0 and cast range

            rod.CurrentCastValue = -rod.CastVary / 2 + numInRange; // Adds the offset for to the number in the range.

            Vector3 forward3D = obj.transform.forward; // get the forward direction
            Vector3 forward2D = new Vector3(forward3D.x, 0f, forward3D.z); // Change it so that it is only important on the flat plane
            forward2D.Normalize(); // Normalize it so its magnitude 1;

            rod.CastSpotPrefab.transform.position = hit.point + forward2D * rod.CurrentCastValue;
            // Originating from where raycast was hit, in the direction that is forward to the player, along the line.
            //Debug.Log(hit.point);
        }
        else if (rod.CastSpotPrefab.activeSelf)
        {
            rod.CastSpotPrefab.SetActive(false);
            rod.GetInRange = false;
        }
    } // Need to at some point force this so that the bobber indicator cant exist within a certain range of the player. so its not too close to the boat.

}


////////////////////////////////////////////////////// Use Item /////////////////////////////////////////////
public class Rod_StateUseItem : Abs_StateItemUse
{ // using the item from the rod state class

    private GameObject obj; // controllers game object
    private UseableItem_Rod rod; // rod item script

    public override void DoEnter(Useable_Controller controller)
    {   
        //Debug.Log("Entered Fishing Use");
        obj = controller.gameObject;
        rod = controller.rod;

        if (!controller.rod.CheckIfFishing())
        {  //if not fishing send out lure
            // throw out rod animation and deposit lures
            rod.CastSpotPrefab.SetActive(false);
            CastLure();
            rod.SetIfFishing(true);
            
        }
        else
        { // if fishing  bring in lure
            Vector3 currentLurePosition = rod.LurePrefab.transform.position;
            // will be used to get the lures position right before it is deactivated.

            rod.LurePrefab.SetActive(false);
            RetrieveLure(currentLurePosition, rod.LureRadius); // this will let the fish know they are no longer in lure zone
            rod.SetIfFishing(false);
            //Debug.Log(holdToCast);
        }

        
    }
    public override void DoExit(Useable_Controller controller)
    {
        //rod.SetIfFishing(!rod.CheckIfFishing());
        // change the fishing state.
    }
    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentItem.Idle; // change state to idle
    }

    private void RetrieveLure(Vector3 currentLurePosition, float radius) // this will let the fish know they are no longer in lure zone
    {   
        Vector3 lurePos = currentLurePosition; 
        // Get all colliders within the sphere radius at this object's position
        Collider[] hitColliders = Physics.OverlapSphere(lurePos, radius, rod.FishMask);
        // might want to make this non alloc version.
        
        int i = 0;
        
        while (i < hitColliders.Length)
        {
            // Try to get the TargetScript component from the hit object
            FishStateController target = hitColliders[i].GetComponent<FishStateController>();
            
            if (rod.LurePrefab.activeSelf && target != null) // this should probably be traded out for some kind of event thing
            {
                target.BobberSpooked(lurePos); 
                
            }
            else if (target != null)
            {
                target.LureReeledIn();  // we should get the data here
            }
            i++;
        }
    }

    private void CastLure() 
    {
        if (rod.GetInRange)
        { 
            rod.LurePrefab.SetActive(true); 
            rod.LurePrefab.transform.position = rod.CastSpotPrefab.transform.position; 
            RetrieveLure(rod.LurePrefab.transform.position, rod.FearRadius); // this isnt retriving it thats just what the name was before i modifired stuff.
            // set the lure position to the position that the indicator used to be.
        }
    }
}
