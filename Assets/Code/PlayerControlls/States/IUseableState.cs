using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseableState
{
    public IUseableState DoState(Useable_Controller controller); // called in the update
    public void DoEnter(Useable_Controller controller);
    public void DoExit(Useable_Controller controller);
}
