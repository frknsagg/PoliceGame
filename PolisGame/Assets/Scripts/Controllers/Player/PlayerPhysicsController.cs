using System;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour,IDamageable
    {
        [SerializeField] private PlayerController playerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamager damager))
            {
                TakeDamage(damager.Damage());
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Bullet,other.gameObject);
                Destroy(other.gameObject);
            }
        }

        public float TakeDamage(int damage)
        {
            if (playerController.Health > 0)
            {
                playerController.Health -= damage;
                if (playerController.Health<=0)
                {
                    gameObject.layer = 0;
                }
                return playerController.Health;
            }
            return 0;
        }
    }
}
