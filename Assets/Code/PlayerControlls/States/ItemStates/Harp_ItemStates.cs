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
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
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
        //throw new System.NotImplementedException();
    }
    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentItem.IsReady;
    }
}

////////////////////////////////////////////////////// Use Item /////////////////////////////////////////////
public class Harp_StateItemUse : Abs_StateItemUse
{
    public override void DoEnter(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }

    public override void DoExit(Useable_Controller controller)
    {
        //throw new System.NotImplementedException();
    }

    public override IUseableState DoState(Useable_Controller controller)
    {
        return controller.currentItem.Idle;
    }
}

////////////////////////////////////////////////////// Place Item /////////////////////////////////////////////
public class Harp_StateItemPlace : Abs_StateItemPlace // place is pickup for an Harp hand.
{
    public override void DoEnter(Useable_Controller controller)
    {
        // do some kind of animation to pick up
        base.DoEnter(controller);
        controller.currentItem.useableMesh.SetActive(false); // turn on the game object 
    }

    public override void DoExit(Useable_Controller controller)
    {
        base.DoExit(controller);
        
    }
}