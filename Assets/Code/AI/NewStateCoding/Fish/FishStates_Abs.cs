using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        protected bool idleActive = false;
        public abstract void DoEnter(Fish_Controller FSC);

        public abstract void DoExit(Fish_Controller FSC);

        public virtual IFishState DoState(Fish_Controller FSC)
        {
            return this;
        }

        protected IEnumerator WanderRoutine(Fish_Controller FSC)
        {
            while(idleActive)
            {
                Vector3 newPos = FSC.SC.RandomNavMeshLocation(FSC.transform.position, FSC.wanderRadius);
                FSC.agent.SetDestination(newPos);
                    
                // Wait until agent is close to destination or timer runs out
                yield return new WaitForSeconds(FSC.wanderTimer);
            }
        }

        
    }

    ////////////////////////////////////////////////////////////////// In Lure Range
    public abstract class Abs_StateLure : IFishState
    {
        protected bool inLureRange = false; // bool for enumerator to continue
        
        public virtual void DoEnter(Fish_Controller FSC)
        {
            FSC.agent.isStopped = false; // reactivates the agent if its stoped.
            //FSC.agent.SetDestination(FSC.targetPos);
            inLureRange = true; 
            FSC.running = FSC.StartCoroutine(LureRoutine(FSC)); 
            //GameManager.Instance.GiveHint(1,1); // Tapping
            
        }

        public virtual void DoExit(Fish_Controller FSC)
        {
            //FSC.agent.SetDestination(FSC.transform.position); // set destination to self
            inLureRange = false;
            FSC.agent.isStopped = true; // deactivates
            FSC.StopCo(FSC.running); // make sture the corutine is stoped
        }

        public virtual IFishState DoState(Fish_Controller FSC)
        {
            return this;
        }
        IEnumerator LureRoutine(Fish_Controller FSC)
        {
            while (inLureRange)
            { // while lured
                float luredRad = Vector3.Distance(FSC.transform.position, FSC.SC.target.transform.position);
                float varriance = Random.Range(-FSC.lureMoveVary,FSC.lureMoveVary);
                luredRad += varriance; // add varriance to the proposed radius.

                if(luredRad > FSC.tapEnterRange)
                {
                    // set destination to somewhere near where the lure is with an equal closness to where the fish is
                    Vector3 newPos = FSC.SC.RandomNavMeshLocation(FSC.SC.target.transform.position, luredRad);
                    FSC.agent.SetDestination(newPos);  
                }
                else
                {
                    FSC.ChangeState(FSC.SC.Bobber);
                }
                float randomTimeLure = Random.Range(1,FSC.wanderTimer);

                // Wait until agent is close to destination or timer runs out
                yield return new WaitForSeconds(randomTimeLure);
            }
        }
    }

    ////////////////////////////////////////////////////////////////// In Bobber Range
    public abstract class Abs_StateBobber : IFishState
    {
        public virtual void DoEnter(Fish_Controller FSC)
        {
            GameManager.Instance.GiveHint(1,2); // Tapping
        }

        public abstract void DoExit(Fish_Controller FSC);

        public virtual IFishState DoState(Fish_Controller FSC)
        {
            return this;
        }
    }

    ////////////////////////////////////////////////////////////////// In Fear Range
    public abstract class Abs_StateFear : IFishState
    {

        public virtual void DoEnter(Fish_Controller FSC)
        {
            GameManager.Instance.GiveHint(1,0); // Fear
            FSC.running = FSC.StartCoroutine(FearTheBobber(FSC.lurePos,FSC));
        }

        public virtual void DoExit(Fish_Controller FSC)
        {
            FSC.StopCo(FSC.running);
        }

        public virtual IFishState DoState(Fish_Controller FSC)
        {
            return this;
        }

        public IEnumerator FearTheBobber(Vector3 lurePosition, Fish_Controller FSC)
        { // will run away for a bit.
            if (FSC.agent != null && FSC.agent.enabled)
            {
                FSC.agent.isStopped = false;
                    
                Vector3 fleeDir = FSC.transform.position - lurePosition; // get the diffence of this object and 
                Vector3 potentialFleePosition = FSC.transform.position + fleeDir.normalized * FSC.fleeDistance; // get a desired place to flee to
                NavMeshHit hit;
                //Sample the NavMesh to find the nearest valid point to the potential flee position
                if (NavMesh.SamplePosition(potentialFleePosition, out hit, FSC.fleeDistance, NavMesh.AllAreas))
                {
                    // Set the agent's destination to the valid point on the NavMesh
                    FSC.agent.SetDestination(hit.position);
                    //Debug.Log(hit.position);
                }
            }
            yield return new WaitForSeconds(FSC.fleeTimer);
            
            FSC.agent.isStopped = true;

            if (FSC.inLureTrigger) // if in lure trigger zone
            {
                FSC.ChangeState(FSC.SC.Lure); // in trigger zone so go back to lure
            }
            else
            {
                FSC.ChangeState(FSC.SC.Idle); // change to idle state 
            }
        }
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
            Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * 50f;
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

            FSC.StartWaitToChange(FSC.SC.Idle,2f); 
            // in 2 seconds change to Idle
        }

        public abstract void DoExit(Fish_Controller FSC);

        public abstract IFishState DoState(Fish_Controller FSC);
    }

    public abstract class Abs_StateHooked : IFishState
    {
        public abstract void DoEnter(Fish_Controller FSC);

        public abstract void DoExit(Fish_Controller FSC);

        public virtual IFishState DoState(Fish_Controller FSC)
        {
            return this;
        }
    }
    public abstract class Abs_StateOnLine : IFishState
    {
        public abstract void DoEnter(Fish_Controller FSC);

        public abstract void DoExit(Fish_Controller FSC);

        public virtual IFishState DoState(Fish_Controller FSC)
        {
            return this;
        }
    }
}