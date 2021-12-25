using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace Werasilz
{
    public class PlayerAttack : MonoBehaviour
    {
        private StarterAssetsInputs _input;
        public GameObject damageWall;

        private void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
        }

        private void Update()
        {
            if (_input.attack)
            {
                _input.attack = false;
                Instantiate(damageWall, transform.position + (Vector3.up / 2), transform.rotation);
            }
        }
    }
}
