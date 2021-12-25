using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class BossChaseState : NPCBaseFiniteStateMachine
    {
        int randomAction;
        bool isAction;

        public GameObject meteor;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            npc = animator.gameObject;
            opponent = npc.GetComponent<NPCAI>().FindEnemy();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (opponent != null)
            {
                MoveToEnemy(animator);
            }
            else
            {
                Debug.Log("ERROR Opponent = null On Chase State " + animator.gameObject.name);
            }

            CooldownTimer();

            FinishCooldown(animator);

            if (isAction == false)
            {
                randomAction = Random.Range(0, 10);

                if (randomAction == 0)
                {
                    isAction = true;
                    SetCooldown(5);
                    MeteorAttack(animator);
                }

                if (randomAction == 1)
                {
                    isAction = true;
                    SetCooldown(5);
                    RoarAction(animator);
                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        private void MeteorAttack(Animator animator)
        {
            if (animator.GetBool("isAttack") == false)
            {
                animator.SetBool("MeteorAttack", true);
                animator.SetBool("isAttack", true);

                // Instantiate(meteor, opponent.transform.position, opponent.transform.rotation);
            }
        }

        private void RoarAction(Animator animator)
        {
            if (animator.GetBool("isRoar") == false)
            {
                animator.SetBool("isRoar", true);
            }
        }

        public override void FinishCooldown(Animator animator)
        {
            if (isCooldown == false)
            {
                isAction = false;
            }
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
