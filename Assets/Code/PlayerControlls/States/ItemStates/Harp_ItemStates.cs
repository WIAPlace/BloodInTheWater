using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/26/26
/// Purpose: Holder for Harpoon Item States
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
/// 

////////////////////////////////////////////////////// Idle /////////////////////////////////////////////
public class Harp_StateItemIdle : Abs_StateItemIdle
{
    public override void DoEnter(Useable_Controller controller)
    {
        base.DoEnter(controller);
    }

    public override void DoExit(Useable_Controller controller)
    {
        // stop corutine for idle animation
    }
}

////////////////////////////////////////////////////// Readying /////////////////////////////////////////////
public class Harp_StateItemReadying : Abs_StateItemReadying
{
    
}

////////////////////////////////////////////////////// Is Ready //////////////////////////////////////////////
public class Harp_StateItemIsReady : Abs_StateItemIsReady
{
    public override void DoEnter(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Useable_Controller controller)
    {
        base.DoExit(controller);
    }
    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentItem.IsReady;
    }
}

////////////////////////////////////////////////////// Use Item /////////////////////////////////////////////
public class Harp_StateItemUse : Abs_StateItemUse
{
    GameObject obj; // object for the controller so we dont have to continualy call and pass it
    UseableItem_Harp harp; // game object for harpoon so we can access it's variables

    public override void DoEnter(Useable_Controller controller)
    {
        obj = controller.FPCamera; // set object to the controller's pobject.
        harp = controller.harp; // set harp as the harpoon attached to the player.
        // play animation

        TryToHit();
    }

    public override void DoExit(Useable_Controller controller)
    {
        // end animation if its still running
    }

    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentItem.Idle;
    }
    private void TryToHit()
    {
        if(Physics.SphereCast(new Ray(obj.transform.position, obj.transform.forward), harp.AtkRadius, out RaycastHit hitInfo, harp.AtkDistance, harp.MonsterMask))
        { // check if a monster is in range to hit
            if(hitInfo.collider.TryGetComponent(out IMonster monster))
            { // if its a monster send it the hit normal
                Vector3 hitDir = hitInfo.normal;
                monster.MonsterHit(hitDir);
            }
        }
    }
}

////////////////////////////////////////////////////// Place Item /////////////////////////////////////////////
public class Harp_StateItemPlace : Abs_StateItemPlace // place is pickup for an Harp hand.
{
    public override void DoEnter(Useable_Controller controller)
    {
        // do some kind of animation to pick up
        controller.currentItem.useableMesh.SetActive(false); // turn on the game object 
        base.DoEnter(controller);
    }

    public override void DoExit(Useable_Controller controller)
    {
        base.DoExit(controller);
        
    }
}

public class Harp_StateItemUnderAttack : Abs_StateItemUnderAttack
{
    public override void DoEnter(Useable_Controller controller)
    {
        
    }

    public override void DoExit(Useable_Controller controller)
    {
        base.DoExit(controller);
    }
}