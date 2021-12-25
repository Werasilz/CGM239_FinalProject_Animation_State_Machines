using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class ChaseState : NPCBaseFiniteStateMachine
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (npc.GetComponent<NPCAI>().friend == null)
            {
                if (opponent != null)
                {
                    MoveToEnemy(animator);
                }
                else
                {
                    Debug.Log("ERROR Opponent = null On Chase State " + animator.gameObject.name);
                }
            }

            SearchFriendDown(animator);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        private void MoveToEnemy(Animator animator)
        {
            Vector3 direction = opponent.transform.position - npc.transform.position;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
            npc.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
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
