/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Abstract for all UseItem States
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public abstract class Abs_StateItemUse : IUsableState
{
    public abstract void DoEnter();

    public abstract void DoExit();

    abstract public IUsableState DoState(Usables_Controller controller); // will be implemented in the States themselves.
}
