using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class BossAttackState : NPCBaseFiniteStateMachine
    {
        int randomAction;
        int randomAttack;
        float distanceFromTarget;
        float distanceAttackLimit = 3f;

        bool isAction;

        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            npc = animator.gameObject;
            opponent = npc.GetComponent<NPCAI>().FindEnemy();
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (opponent == null) return;

            if (isAction == false)
            {
                Attack(animator);
            }

            CooldownTimer();

            FinishCooldown(animator);
        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void FinishCooldown(Animator animator)
        {
            if (isCooldown == false)
            {
                isAction = false;
            }
        }

        private void Attack(Animator animator)
        {
            distanceFromTarget = Vector3.Distance(opponent.transform.position, npc.transform.position);

            if (distanceFromTarget < distanceAttackLimit)
            {
                // npc.transform.LookAt(opponent.transform.position);

                // randomAction = Random.Range(0, 3);
                randomAction = 0;

                switch (randomAction)
                {
                    case 0:
                        RandomAttack(animator);
                        isAction = true;
                        SetCooldown(3);
                        break;
                    case 1:
                        // MeteorAttack(animator);
                        isAction = true;
                        SetCooldown(5);
                        break;
                    case 2:
                        // RoarAction(animator);
                        isAction = true;
                        SetCooldown(5);
                        break;
                }
            }
        }

        private void RandomAttack(Animator animator)
        {
            if (animator.GetBool("isAttack") == false)
            {
                randomAttack = Random.Range(0, 3);
            }

            switch (randomAttack)
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

        // private void MeteorAttack(Animator animator)
        // {
        //     if (animator.GetBool("isAttack") == false)
        //     {
        //         animator.SetBool("MeteorAttack", true);
        //         animator.SetBool("isAttack", true);
        //     }
        // }

        // private void RoarAction(Animator animator)
        // {
        //     if (animator.GetBool("isRoar") == false)
        //     {
        //         animator.SetBool("isRoar", true);
        //     }
        // }

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
