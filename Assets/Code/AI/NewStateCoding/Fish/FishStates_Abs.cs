using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// 
/// Author: Weston Tollette
/// Created: 3/8/26
/// Purpose: Holder for all the fish abstact states
///     Usualy implies what range they are in
/// 
/// Edited:
/// Edited By:
/// Edit Purpose:
///
namespace TLC.FishStates{
    ////////////////////////////////////////////////////////////////// In Idle Range
    public abstract class Abs_StateIdle : IFishState
    {
        public abstract void DoEnter(Fish_Controller FSC);

        public abstract void DoExit(Fish_Controller FSC);

        public abstract IFishState DoState(Fish_Controller FSC);
    }

    ////////////////////////////////////////////////////////////////// In Lure Range
    public abstract class Abs_StateLure : IFishState
    {
        public abstract void DoEnter(Fish_Controller FSC);

        public abstract void DoExit(Fish_Controller FSC);

        public abstract IFishState DoState(Fish_Controller FSC);
    }

    ////////////////////////////////////////////////////////////////// In Bobber Range
    public abstract class Abs_StateBobber : IFishState
    {
        public abstract void DoEnter(Fish_Controller FSC);

        public abstract void DoExit(Fish_Controller FSC);

        public abstract IFishState DoState(Fish_Controller FSC);
    }

    ////////////////////////////////////////////////////////////////// In Fear Range
    public abstract class Abs_StateFear : IFishState
    {
        public abstract void DoEnter(Fish_Controller FSC);

        public abstract void DoExit(Fish_Controller FSC);

        public abstract IFishState DoState(Fish_Controller FSC);
    }

    ////////////////////////////////////////////////////////////////// Unique Behavior
    public abstract class Abs_StateUnique : IFishState
    {
        public abstract void DoEnter(Fish_Controller FSC);

        public abstract void DoExit(Fish_Controller FSC);

        public abstract IFishState DoState(Fish_Controller FSC);
    }

    ////////////////////////////////////////////////////////////////// Enter
    public abstract class Abs_StateEnter : IFishState
    {
        public virtual void DoEnter(Fish_Controller FSC)
        {
            // set locaton of fish to somewhere in range
            Vector3 randomPoint = FSC.transform.position + Random.insideUnitSphere * 50f;
            NavMeshHit hit;
            //Debug.Log("Running Enter");
            if (NavMesh.SamplePosition(randomPoint, out hit, 50f, -1)) 
            {
                FSC.transform.position = hit.position;
            }
            else
            {
                FSC.transform.position = Vector3.zero;
                //Debug.Log("Spot was not in valid Range");
            } 

            FSC.StartWaitToChange(FSC.stateController.Idle,2f); 
            // in 2 seconds change to Idle
        }

        public abstract void DoExit(Fish_Controller FSC);

        public abstract IFishState DoState(Fish_Controller FSC);
    }
}