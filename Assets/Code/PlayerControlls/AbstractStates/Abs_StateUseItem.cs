using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// 
/// Author: Weston Tollette
/// Created: 2/25/26
/// Purpose: Abstract for all UseItem States
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
public abstract class Abs_StateUseItem : IUsableState
{
    abstract public IUsableState DoState(Usables_Controller controller); // will be implemented in the States themselves.
}
