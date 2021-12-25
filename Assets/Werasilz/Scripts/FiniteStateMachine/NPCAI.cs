using UnityEngine;

namespace Werasilz
{
    public class NPCAI : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        public GameObject enemy;
        public GameObject friend;
        public GameObject weapon;

        [Header("Stats")]
        public bool isDead;
        public bool isReviving;
        public float currentHealth = 100;
        public float minDamage = 5;
        public float maxDamage = 15;

        public GameObject FindEnemy()
        {
            if (enemy == null)
            {
                animator.SetBool("isFoundEnemy", false);
                return null;
            }

            animator.SetBool("isFoundEnemy", true);
            return enemy;
        }

        public GameObject FindFriend()
        {
            if (friend == null)
            {
                animator.SetBool("isFoundFriendDown", false);
                return null;
            }

            animator.SetBool("isFoundFriendDown", true);
            return friend;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            CheckDistanceFromEnemy();
            CheckDistanceFromFriend();
            Dead();
        }

        private void CheckDistanceFromEnemy()
        {
            if (enemy != null)
            {
                animator.SetFloat("DistanceEnemy", Vector3.Distance(transform.position, enemy.transform.position));
            }
        }

        private void CheckDistanceFromFriend()
        {
            if (friend != null)
            {
                animator.SetFloat("DistanceFriend", Vector3.Distance(transform.position, friend.transform.position));
            }
        }

        private void Dead()
        {
            if (isDead == false && currentHealth <= 0)
            {
                isDead = true;
                currentHealth = 0;
                animator.CrossFade("Dead", 0.2f);
            }
        }

        public void TakeDamage(NPCAI enemy)
        {
            float damageTaken = Random.Range(enemy.minDamage, enemy.maxDamage);
            currentHealth -= damageTaken;
            animator.CrossFade("Damage", 0.2f);
        }

        public void TakeDamage(float damageTaken)
        {
            currentHealth -= damageTaken;
            animator.CrossFade("Damage", 0.2f);
        }

        #region Animation Event
        public void EnableHitBoxCollider()
        {
            weapon.GetComponent<Collider>().enabled = true;
        }

        public void DisableHitBoxCollider()
        {
            weapon.GetComponent<Collider>().enabled = false;
        }
        #endregion
    }
}