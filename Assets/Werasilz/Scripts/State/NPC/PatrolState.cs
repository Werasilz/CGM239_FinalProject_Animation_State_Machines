using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class PatrolState : NPCBaseFiniteStateMachine
    {
        public GameObject[] wayPoints;
        public int currentWaypoint;
        public float distanceWaypointLimit = 3f;
        public float distanceFromWaypoint;

        private void Awake()
        {
            wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        }

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ResetEnemy(animator);

            currentWaypoint = 0;

            animator.SetFloat("patrolOffset", Random.Range(0f, 1f));
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (wayPoints.Length == 0) return;

            MoveToWaypoint(animator);

            SearchEnemy();

            SearchFriendDown(animator);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        private void MoveToWaypoint(Animator animator)
        {
            distanceFromWaypoint = Vector3.Distance(wayPoints[currentWaypoint].transform.position, npc.transform.position);

            if (distanceFromWaypoint < distanceWaypointLimit)
            {
                int i = Random.Range(0, 2);

                // Change Waypoint
                if (i == 0)
                {
                    currentWaypoint = Random.Range(0, wayPoints.Length);
                }
                // Idle at Current Waypoint
                else
                {
                    // Go to Idle State
                    animator.SetBool("isIdle", true);
                }
            }

            // Move to Waypoint
            Vector3 direction = wayPoints[currentWaypoint].transform.position - npc.transform.position;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
            npc.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        }

        private void SearchEnemy()
        {
            // Scan for Enemy
            hitColliders = Physics.OverlapSphere(npc.transform.position, radius);

            foreach (var hitCollider in hitColliders)
            {
                // Not itself
                if (hitCollider.transform != npc.transform.root)
                {
                    // Found Enemy
                    if (hitCollider.gameObject.tag == "Player")
                    {
                        // Set Enemy
                        npc.GetComponent<NPCAI>().enemy = hitCollider.gameObject;

                        // Go to Chase State
                        npc.GetComponent<NPCAI>().FindEnemy();
                    }
                }
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
