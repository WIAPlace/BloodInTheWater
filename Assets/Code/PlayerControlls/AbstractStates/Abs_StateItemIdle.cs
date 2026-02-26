/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Abstract for items in their idle state
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public abstract class Abs_StateItemIdle : IUsableState
{
    public abstract void DoEnter();

    public abstract void DoExit();

    public virtual IUsableState DoState(Usables_Controller controller)
    { // idle is a buffer so it can just return idle
        return this; // will return an instance of the concrete class / x Idlestate
    }
}
