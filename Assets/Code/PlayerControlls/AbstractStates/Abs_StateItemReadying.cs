/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Abstract for states of readying
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public abstract class Abs_StateItemReadying : IUsableState
{
    public abstract void DoEnter();

    public abstract void DoExit();

    public virtual IUsableState DoState(Usables_Controller controller)
    { // much like idle this is a buffer state.
        return this; // will return the concrete version of this class
    }
}
