using Abstract;
using Controllers;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace Enemy
{
    public class EnemyAttack : EnemyBaseState
    {
        private EnemyManager _manager;
        private ThiefAnimationController _thiefAnimationController;
        private NavMeshAgent _agent;
        public EnemyAttack(ref EnemyManager manager, ref ThiefAnimationController thiefAnimationController,NavMeshAgent agent)
        {
            _manager = manager;
            _thiefAnimationController = thiefAnimationController;
            _agent = agent;
        }
        public override void EnterState()
        {
            _manager.SetTriggerAnim(EnemyAnimationsTypes.Run);
        }

        public override void UpdateState()
        {
            Debug.Log("Hello from Update state of Enemy Attack script");
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("bullet"))
            {
                
                _manager.OnTakeDamage();
            
            }
        }

    }
}
