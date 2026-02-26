using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsableState
{
    public IUsableState DoState(Usables_Controller controller); // called in the update
    public void DoEnter();
    public void DoExit();
}
