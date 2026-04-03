using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gull_Behavior : StateMachineBehaviour
{
    [SerializeField]
    private float _timeUntilIdle;

    [SerializeField]
    private int _numberOfBirdIdles;

    private bool isIdling;

    private float idletime;
    private int idleAnim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isIdling == false)
        {
            idletime += Time.deltaTime;
            if(idletime > _timeUntilIdle && stateInfo.normalizedTime% 1< 0.02f)
            {
                isIdling = true;
                idleAnim = Random.Range(1, _numberOfBirdIdles + 1);
                idleAnim = idleAnim * 2 - 1;

                animator.SetFloat("IdleAnim", idleAnim - 1);
                
            }
        }

        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle();
        }

        animator.SetFloat("IdleAnim", idleAnim, 0.2f, Time.deltaTime);
    }

    private void ResetIdle()
    {
        if (isIdling)
        {
            idleAnim--;
        }

        isIdling = false;
        idletime = 0;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
