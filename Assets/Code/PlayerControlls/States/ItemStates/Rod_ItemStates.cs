using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
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

////////////////////////////////////////////////////// Idle /////////////////////////////////////////////
public class Rod_StateItemIdle : Abs_StateItemIdle
{
    private UseableItem_Rod rod;
    public override void DoEnter(Useable_Controller controller)
    {
        rod = controller.rod;
        base.DoEnter(controller);
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
        if (rod != null && rod.LurePrefab.activeSelf)
        {
            rod.RetrieveLure(rod.LurePrefab.transform.position, rod.LureRadius);
        }
    }
}

////////////////////////////////////////////////////// Readying /////////////////////////////////////////////
public class Rod_StateItemReadying : Abs_StateItemReadying
{ 
    // if fishing readying will be reeling in the fish, else it will be readying to cast.
    
    public override void DoEnter(Useable_Controller controller)
    {
        if(controller.previousState == controller.currentItem.UnderAtk) // catch for if attack just occured
        {
            controller.ChangeState(controller.currentItem.Idle);
            return;
        }
        if(!controller.rod.CheckIfFishing())
        { // only do this if not fishing
            controller.StartWaitToChange(controller.currentItem.IsReady, controller.readyTime);
            controller.WindUpOn(controller.readyTime); // turns on the wind up indicator
        }
        else
        { // if fishing just reel in reel quick
            //controller.ChangeState(controller.currentItem.IsReady);
        }
    }

    public override void DoExit(Useable_Controller controller)
    {
        controller.StopCo(controller.running);
    }

    public override IUseableState DoState(Useable_Controller controller)
    {
        if(controller.rod.CheckIfFishing())
        {
            Vector3 lookDir = new Vector3( // look at the fishing rod mesh
                controller.rod.useableMesh.transform.position.x, 
                controller.rod.LurePrefab.transform.position.y,
                controller.rod.useableMesh.transform.position.z
                );

            controller.rod.LurePrefab.transform.LookAt(lookDir);
            controller.rod.LurePrefab.transform.Translate(Vector3.forward * controller.rod.ReelIn * Time.deltaTime );

            float dist = Vector3.Distance(controller.rod.LurePrefab.transform.position, lookDir);
            if (dist <= 1)
            {   // retrive lure if it is at a certain distance
                controller.rod.LurePrefab.SetActive(false);
                controller.rod.RetrieveLure(controller.rod.LurePrefab.transform.position, controller.rod.LureRadius); // this will let the fish know they are no longer in lure zone
                controller.rod.SetIfFishing(false);
            }
        }

        return this;
    }
}

////////////////////////////////////////////////////// Is Ready //////////////////////////////////////////////
public class Rod_StateItemIsReady : Abs_StateItemIsReady
{
    private GameObject obj; // controllers game object
    private UseableItem_Rod rod; // rod item script
    public override void DoEnter(Useable_Controller controller)
    {
        //Debug.Log("Entered Ready");
        obj = controller.FPCamera;
        rod = controller.rod;
        
        
        if (rod.fishHookIndicator != null && rod.CheckIfFishing())
        {
            // indicator that the fish is ready to be caught.
            
            controller.audioSource.PlayOneShot(rod.fishHookIndicator);
        }
        
        
    }

    public override void DoExit(Useable_Controller controller)
    {
        base.DoExit(controller);
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
public class Rod_StateItemUse : Abs_StateItemUse
{ // using the item from the rod state class

    private GameObject obj; // controllers game object
    private UseableItem_Rod rod; // rod item script

    public override void DoEnter(Useable_Controller controller)
    {   
        //Debug.Log("Entered Fishing Use");
        obj = controller.FPCamera;
        rod = controller.rod;

        if (!controller.rod.CheckIfFishing())
        {  //if not fishing send out lure
            // throw out rod animation and deposit lures
            rod.CastSpotPrefab.SetActive(false);
            CastLure();
            rod.SetIfFishing(true);
            //Debug.Log("Hit");
        }
        else
        { // if fishing  bring in lure
            Vector3 currentLurePosition = rod.LurePrefab.transform.position;
            // will be used to get the lures position right before it is deactivated.

            rod.LurePrefab.SetActive(false);
            rod.RetrieveLure(currentLurePosition, rod.LureRadius); // this will let the fish know they are no longer in lure zone
            rod.SetIfFishing(false);
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

    

    private void CastLure() 
    {
        if (rod.GetInRange)
        { 
            rod.LurePrefab.SetActive(true); 
            rod.LurePrefab.transform.position = rod.CastSpotPrefab.transform.position; 
            rod.RetrieveLure(rod.LurePrefab.transform.position, rod.FearRadius); // this isnt retriving it thats just what the name was before i modifired stuff.
            // set the lure position to the position that the indicator used to be.
        }
    }
}

////////////////////////////////////////////////////// Place Item /////////////////////////////////////////////
public class Rod_StateItemPlace : Abs_StateItemPlace
{
    private UseableItem_Rod rod;
    public override void DoEnter(Useable_Controller controller)
    {
        rod = controller.rod;
        // start animation to place or pickup something
        controller.currentItem.useableMesh.SetActive(false); // turn of the game object 
        if (rod != null && rod.LurePrefab.activeSelf)
        {
            //Debug.Log("Hit retrive on place");
            rod.LurePrefab.SetActive(false); // either it stays out or not.
            rod.RetrieveLure(rod.LurePrefab.transform.position,rod.LureRadius); // retrive lure
            rod.SetIfFishing(false);
        }
        if (rod != null && rod.CastSpotPrefab.activeSelf)
        { // bring in cast spot
            rod.CastSpotPrefab.SetActive(false); // either it stays out or not.
        }
        base.DoEnter(controller);

    }

    public override void DoExit(Useable_Controller controller)
    {
        base.DoExit(controller);
    }    
}

////////////////////////////////////////////////////// Under Attack /////////////////////////////////////////////
public class Rod_StateItemUnderAttack : Abs_StateItemUnderAttack
{
    private UseableItem_Rod rod;
    public override void DoEnter(Useable_Controller controller)
    {
        //Debug.Log("Hit");
        rod = controller.rod;
        if (rod != null && rod.LurePrefab.activeSelf)
        { // bring in lure
            rod.LurePrefab.SetActive(false); // either it stays out or not.
            rod.RetrieveLure(rod.LurePrefab.transform.position, rod.LureRadius);
            //rod.SetIfFishing(false);
        }
        if (rod != null && rod.CastSpotPrefab.activeSelf)
        { // bring in cast spot
            rod.CastSpotPrefab.SetActive(false); // either it stays out or not.
        }
        rod.SetIfFishing(false);
        
    }

    public override void DoExit(Useable_Controller controller)
    {
        base.DoExit(controller);
    }
}
