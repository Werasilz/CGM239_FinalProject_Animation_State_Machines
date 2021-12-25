using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class AttackState : NPCBaseFiniteStateMachine
    {
        int randomAction;
        float distanceFromTarget;
        float distanceAttackLimit = 3f;

        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (opponent != null)
            {
                Attack(animator);
            }
            else
            {
                Debug.Log("ERROR Opponent = null on Attack State " + animator.gameObject.name);
            }
        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        private void Attack(Animator animator)
        {
            distanceFromTarget = Vector3.Distance(opponent.transform.position, npc.transform.position);

            if (distanceFromTarget < distanceAttackLimit)
            {
                npc.transform.LookAt(opponent.transform.position);

                if (animator.GetBool("isAttack") == false)
                {
                    randomAction = Random.Range(0, 3);
                }

                switch (randomAction)
                {
                    case 0:
                        animator.SetBool("TriggerAttack_01", true);
                        animator.SetBool("isAttack", true);
                        break;
                    case 1:
                        animator.SetBool("TriggerAttack_02", true);
                        animator.SetBool("isAttack", true);
                        break;
                    case 2:
                        animator.SetBool("TriggerAttack_03", true);
                        animator.SetBool("isAttack", true);
                        break;
                }
            }
        }

        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        //{
        //    
        //}

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        //{
        //    
        //}
    }
}
