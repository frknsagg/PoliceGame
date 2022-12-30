using Controllers;
using Enemy;
using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace States.Enemy
{
    public class ChaseState : IState
    {
        private EnemyData _data;
        private EnemyTypes _types;
        private EnemyManager _manager;
        private float _moveSpeed;
        private ThiefAnimationController _thiefAnimationController;
        private NavMeshAgent _agent;
        private float _attackRange;
        private RaycastHit _hit;
        
        public bool attackOnPlayer;
        public bool IsPlayerInRange() => attackOnPlayer;
    
    
        public ChaseState(EnemyManager manager,NavMeshAgent agent,float moveSpeed,ThiefAnimationController thiefAnimationController,EnemyData data,EnemyTypes types)
        {
            _manager = manager;
            _agent = agent;
            _moveSpeed = moveSpeed;
            _thiefAnimationController = thiefAnimationController;
            _data = data;
            _types = types;

        }
        public void Tick()
        {
            if (_manager.PlayerTarget)
            {
                _agent.SetDestination(_manager.PlayerTarget.position);
                _manager.transform.LookAt(_manager.PlayerTarget.position);
           
                CheckAttackDistance();
            }

        }

        public void OnEnter()
        {
            Debug.Log("chase enter");
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Run);
            _agent.enabled = true;
            _agent.speed = _data.EnemyTypeDatas[_types].MoveSpeed;
            _attackRange = _data.EnemyTypeDatas[_types].AttackRange;
            attackOnPlayer = false;
            
        }

        public void OnExit()
        {
            _thiefAnimationController.ResetAnim(EnemyAnimationsTypes.Run);
            _agent.SetDestination(_manager.transform.position);
        }
        private void CheckAttackDistance()
        {
            if (_agent.remainingDistance < _attackRange)
            {
                attackOnPlayer = true;
            }
        }

   
    }
}

