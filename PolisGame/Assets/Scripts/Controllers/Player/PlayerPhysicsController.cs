using System;
using Enums;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour,IDamageable
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerManager playerManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamager damager))
            {
                TakeDamage(damager.Damage());
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Bullet,other.gameObject);
                Destroy(other.gameObject);
            }

            if (other.TryGetComponent(out ICollectable collectable))
            {
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Money,other.gameObject);
               
                playerManager.collectedMoney += collectable.CollectMoney();
            }
        }

        public float TakeDamage(int damage)
        {
            if (playerManager.Health > 0)
            {
                playerManager.Health -= damage;
                if (playerManager.Health<=0)
                {
                    gameObject.layer = 0;
                    CoreGameSignals.Instance.onLevelFailed?.Invoke();
                }
                return playerManager.Health;
            }
            return 0;
        }
    }
}
