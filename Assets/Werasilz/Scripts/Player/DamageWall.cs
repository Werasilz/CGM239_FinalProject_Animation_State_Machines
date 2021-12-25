using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Werasilz
{
    public class DamageWall : MonoBehaviour
    {
        public float damage = 10;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                NPCAI enemy = other.GetComponent<NPCAI>();

                if (enemy.isDead == false)
                {
                    if (this.gameObject.tag == "PlayerWall")
                    {
                        enemy.TakeDamage(damage);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
