using Enemy;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour,IDamageable
    {
        private EnemyManager _manager;
        private void Awake()
        {
            _manager = gameObject.GetComponentInParent<EnemyManager>();
        }
        private void OnEnable()
        {
            gameObject.layer = 6;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamager damager))
            {
            
                TakeDamage(damager.Damage());
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Bullet,other.gameObject);
                _manager.OnTakeDamage();
            }
        }
    
        public float  TakeDamage(int damage)
        {
            if (_manager.Health > 0)
            {
                _manager.Health -= damage;
                if (_manager.Health<=0)
                {
                    gameObject.layer = 0;
                }
                return _manager.Health;
            }
            return 0;
        }
    }
}
