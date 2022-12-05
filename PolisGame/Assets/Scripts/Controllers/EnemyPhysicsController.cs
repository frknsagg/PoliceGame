using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysicsController : MonoBehaviour,IDamageable
{
    private EnemyManager _manager;
    private void Awake()
    {
        _manager = gameObject.GetComponentInParent<EnemyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamager damager))
        {
            Destroy(other.gameObject);
            TakeDamage(damager.Damage());
            _manager.OnTakeDamage();
            
        }

        if (other.CompareTag("Robbable"))
        {
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
