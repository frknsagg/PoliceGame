using Abstract;
using Controllers;
using Enums;
using UnityEngine;
using UnityEngine.AI;


namespace States.Enemy
{
    public class EnemySteal : EnemyBaseState
    {
        private EnemyManager _manager;
        private ThiefAnimationController _thiefAnimationController;
        private NavMeshAgent _agent;
        private Transform _targetArea;
      public  EnemySteal( EnemyManager manager, ThiefAnimationController thiefAnimationController, NavMeshAgent agent)
        {
            _manager = manager;
            _thiefAnimationController = thiefAnimationController;
            _agent = agent;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void EnterState()
        {
            _agent.speed = 0;
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Steal);
            _manager.CallStealFromNpc();
            
        }

        public override void UpdateState()
        {
            if (_manager.GetHealth())
            {
                _manager.SwitchState(EnemyStatesTypes.Death);
            }
            
            
        }

        public override void OnTriggerEnter(Collider other)
        {
           _manager.OnTakeDamage();
        }

        

    }
    
}

