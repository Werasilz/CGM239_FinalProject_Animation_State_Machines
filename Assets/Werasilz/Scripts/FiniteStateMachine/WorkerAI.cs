using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class WorkerAI : MonoBehaviour
    {
        [HideInInspector] public Animator animator;

        public GameObject objective;

        [Header("Inventory")]
        public int flour;
        public int bread;
        public int money;

        [Header("Worker Needed")]
        public int flourNeeded;
        public int breadNeeded;

        [Header("Worker Position")]
        public Transform workPosition;
        public Transform sellPosition;
        public Transform collectPosition;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            CheckDistanceFromObjective();
        }

        private void CheckDistanceFromObjective()
        {
            if (objective != null)
            {
                animator.SetFloat("DistanceTarget", Vector3.Distance(transform.position, objective.transform.position));
            }
        }
    }
}
