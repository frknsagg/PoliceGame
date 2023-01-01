using System;
using Controllers;
using Enemy;
using Enums;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace States.Enemy
{
    public class AttackState : IState
    {
        private EnemyData _data;
        private EnemyTypes _types;
        private NavMeshAgent _agent;
        private ThiefAnimationController _thiefAnimationController;
        private EnemyManager _manager;
        private RigBuilder _rigBuilder;
        private GameObject _gun;
        
        private float _attackRange;
        private float _feverFrequency;
        private float _fireCounter = 2;
        private GunController _gunController;

        public bool _inAttack;
        public bool PlayerExitAttackRange() => _inAttack;

        private RaycastHit _hit;

        public AttackState(NavMeshAgent agent, ThiefAnimationController animator, EnemyManager manager,
            GunController gunController, EnemyData data, EnemyTypes types,RigBuilder rigBuilder,GameObject gun)
        {
            _agent = agent;
            _thiefAnimationController = animator;
            _manager = manager;
            _data = data;
            _types = types;
            _gunController = gunController;
            _gun = gun;
            _rigBuilder = rigBuilder;
        }

        public void Tick()
        {
            Vector3 relative = _manager.transform.InverseTransformPoint(_manager.PlayerTarget.position);
            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            _manager.transform.Rotate(0, angle, 0);
            _fireCounter += Time.deltaTime;
            _inAttack = false;

            if (_fireCounter >= _feverFrequency)
            {
                _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Attack);
                _inAttack = true;
                _gunController.Fire();
                _fireCounter = 0;
            }
        }

        public void OnEnter()
        {
            Debug.Log("atack enter");
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Attack);
            _agent.speed = 0;
            _feverFrequency = _data.EnemyTypeDatas[_types].FeverFrequency;
            _attackRange = _data.EnemyTypeDatas[_types].AttackRange;
            _gun.SetActive(true);
            _rigBuilder.enabled = true;
        }

        public void OnExit()
        {
            _thiefAnimationController.ResetAnim(EnemyAnimationsTypes.Attack);
        }
    }
}