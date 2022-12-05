using System;
using Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace States.Enemy
{
    public class AttackState : IState
    {
        private NavMeshAgent _agent;
        private ThiefAnimationController _thiefAnimationController;
        private EnemyManager _manager;
        private float _attackRange;
        private float _feverFrequency;
        private float _fireCounter = 2;
        private GunController _gunController;

        public bool _inAttack;
        public bool PlayerExitAttackRange() => _inAttack;

        private RaycastHit _hit;
        
        public AttackState(NavMeshAgent agent,ThiefAnimationController animator,EnemyManager manager,float attackRange,float feverFrequency,GunController gunController)
        {
            _agent = agent;
            _thiefAnimationController = animator;
            _manager = manager;
            _attackRange = attackRange;
            _feverFrequency = feverFrequency;
            _gunController = gunController;
        }
        
        public void Tick()
        {
            Vector3 relative = _manager.transform.InverseTransformPoint(_manager.PlayerTarget.position);
            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            _manager.transform.Rotate(0, angle, 0);
            _fireCounter += Time.deltaTime;
            _inAttack = false;
         
            if (_fireCounter>=_feverFrequency)
            {
                _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Attack);
                _inAttack = true;
                _gunController.Fire();
                _fireCounter = 0;
            }
                // _manager.transform.LookAt(_manager.PlayerTarget);
                // // CheckAttackDistance();
                // if (Physics.Raycast(_agent.transform.position,_agent.transform.TransformDirection(Vector3.forward),out _hit,200))
                // {
                //     if (_hit.collider.gameObject.CompareTag("Player"))
                //     {
                //         Debug.DrawRay(_agent.transform.position, _agent.transform.TransformDirection(Vector3.forward) * _hit.distance, Color.yellow);
                //         Debug.Log("playere çarptı");
                //     }
                // }
                // else
                // {
                //     Debug.DrawRay(_agent.transform.position, _agent.transform.TransformDirection(Vector3.forward) * 200, Color.white);
                //     Debug.Log("Did not Hit");
                // }
        }

        public void OnEnter()
        {
            Debug.Log("atack enter");
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Attack);
            _agent.speed = 0;
        }

        public void OnExit()
        {
           _thiefAnimationController.ResetAnim(EnemyAnimationsTypes.Attack);
        }
    }
}
