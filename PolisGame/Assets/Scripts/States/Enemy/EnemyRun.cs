using Abstract;
using Controllers;
using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace States.Enemy
{
    public class EnemyRun : EnemyBaseState
    {
        private EnemyManager _manager;
        private ThiefAnimationController _thiefAnimationController;
        private NavMeshAgent _agent;

        public EnemyRun(ref EnemyManager manager,ThiefAnimationController thiefAnimationController,NavMeshAgent agent)
        {
            _manager = manager;
            _thiefAnimationController = thiefAnimationController;
            _agent = agent;
        }
        public override void EnterState()
        {
            _manager.SetTriggerAnim(EnemyAnimationsTypes.Run);
            _agent.speed = 0.7f;
        }

        public override void UpdateState()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _manager.SwitchState(EnemyStatesTypes.Steal);
            }
        }

        public override void OnTriggerEnter(Collider other)
        {
            _manager.OnTakeDamage();
        }
    }
}
