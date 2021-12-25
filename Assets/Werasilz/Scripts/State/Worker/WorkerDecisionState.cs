using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class WorkerDecisionState : NPCBaseFiniteStateMachine
    {
        WorkerAI workerAI;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            npc = animator.gameObject;
            workerAI = npc.GetComponent<WorkerAI>();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (workerAI.bread >= workerAI.breadNeeded)
            {
                workerAI.objective = workerAI.sellPosition.gameObject;
                animator.SetBool("isSell", true);
                return;
            }

            if (workerAI.flour < workerAI.flourNeeded)
            {
                workerAI.objective = workerAI.collectPosition.gameObject;
                animator.SetBool("isCollect", true);
                return;
            }

            if (workerAI.flour >= workerAI.flourNeeded)
            {
                workerAI.objective = workerAI.workPosition.gameObject;
                animator.SetBool("isWork", true);
                return;
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

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
}
