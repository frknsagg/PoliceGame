using Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace States.Enemy
{
    public class ChaseState : IState
    {
        private EnemyManager _manager;
        private float _moveSpeed;
        private ThiefAnimationController _thiefAnimationController;
        private NavMeshAgent _agent;
        private float _attackRange;
        private RaycastHit _hit;
        
        public bool attackOnPlayer;
        public bool IsPlayerInRange() => attackOnPlayer;
    
    
        public ChaseState(EnemyManager manager,NavMeshAgent agent,float moveSpeed,ThiefAnimationController thiefAnimationController,float attackRange)
        {
            _manager = manager;
            _agent = agent;
            _moveSpeed = moveSpeed;
            _thiefAnimationController = thiefAnimationController;
            _attackRange = attackRange;

        }
        public void Tick()
        {
            _agent.SetDestination(_manager.PlayerTarget.position);
            _manager.transform.LookAt(_manager.PlayerTarget.position);
           
            CheckAttackDistance();
        }

        public void OnEnter()
        {
            Debug.Log("chase enter");
           
            _agent.enabled = true;
            _agent.speed = _moveSpeed;
            attackOnPlayer = false;
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Run);
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

