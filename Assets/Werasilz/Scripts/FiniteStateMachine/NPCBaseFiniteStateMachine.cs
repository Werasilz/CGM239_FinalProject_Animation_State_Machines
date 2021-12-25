using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class NPCBaseFiniteStateMachine : StateMachineBehaviour
    {
        public GameObject npc;
        public GameObject opponent;
        public GameObject friend;

        public float cooldownTime;
        public bool isCooldown;

        public float moveSpeed = 1f;
        public float rotateSpeed = 5f;

        public Collider[] hitColliders;
        public float radius = 10f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            npc = animator.gameObject;
            opponent = npc.GetComponent<NPCAI>().FindEnemy();
            friend = npc.GetComponent<NPCAI>().FindFriend();
        }

        public void SetCooldown(float time)
        {
            isCooldown = true;
            cooldownTime = time;
        }

        public void CooldownTimer()
        {
            if (cooldownTime > 0)
            {
                cooldownTime -= Time.deltaTime;
            }

            if (isCooldown)
            {
                if (cooldownTime <= 0)
                {
                    isCooldown = false;
                    cooldownTime = 0;
                }
            }
        }

        public virtual void FinishCooldown(Animator animator) { }

        public void ResetEnemy(Animator animator)
        {
            npc = animator.gameObject;
            opponent = null;
            npc.GetComponent<NPCAI>().enemy = null;
            animator.SetBool("isFoundEnemy", false);
        }

        public void ResetFriend(Animator animator)
        {
            npc = animator.gameObject;
            friend = null;
            npc.GetComponent<NPCAI>().friend = null;
            animator.SetBool("isFoundFriendDown", false);
        }

        public void SearchFriendDown(Animator animator)
        {
            // Scan for Friend
            hitColliders = Physics.OverlapSphere(npc.transform.position, radius);

            foreach (var hitCollider in hitColliders)
            {
                // Not itself
                if (hitCollider.transform != npc.transform.root)
                {
                    // Found Friend
                    if (hitCollider.gameObject.tag == "Enemy")
                    {
                        // Friend is Dead
                        if (hitCollider.GetComponent<NPCAI>().isDead)
                        {
                            // Not have anyone help
                            if (hitCollider.GetComponent<NPCAI>().isReviving == false)
                            {
                                // Set Friend
                                npc.GetComponent<NPCAI>().friend = hitCollider.gameObject;

                                // Go to 
                                //npc.GetComponent<NPCAI>().FindFriend();
                                friend = npc.GetComponent<NPCAI>().FindFriend();

                                // Set This Guy have someone going to help
                                friend.GetComponent<NPCAI>().isReviving = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
