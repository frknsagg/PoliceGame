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
           _manager.OnTakeDamage();
            Destroy(other.gameObject);
            TakeDamage(damager.Damage());
            
        }

        if (other.CompareTag("Robbable"))
        {
            // Debug.Log("dokundu");
            // _manager._target = other.transform;
            // _manager.CurrentTarget = _manager._target;
        }
    }
    
    public float  TakeDamage(int damage)
    {
        if (_manager.Health > 0)
        {
            _manager.Health -= damage;
            return _manager.Health;
        }
        return 0;
    }
}
