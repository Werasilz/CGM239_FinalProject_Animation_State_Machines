using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class HelpState : NPCBaseFiniteStateMachine
    {
        public float helpTime = 5;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.SetBool("isHelp", true);
            SetCooldown(helpTime);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CooldownTimer();

            FinishCooldown(animator);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void FinishCooldown(Animator animator)
        {
            if (isCooldown == false)
            {
                animator.SetBool("isHelp", false);

                if (friend != null)
                {
                    friend.GetComponent<NPCAI>().animator.SetBool("isRevive", true);
                }

                ResetFriend(animator);
            }
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
