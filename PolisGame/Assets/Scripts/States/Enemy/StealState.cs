using Controllers;
using Enemy;
using Enums;
using Signals;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace States.Enemy
{
    public class StealState : IState
    {
        private EnemyData _data;
        private EnemyTypes _types;
        private EnemyManager _manager;
        private ThiefAnimationController _thiefAnimationController;
        private NavMeshAgent _agent;
        private float _theftTime;
        private RigBuilder _rigBuilder;
        private float _counter;
        private float _attackRange;
        private bool isStartSteal;
        private StealBarController _stealBar;

        public StealState(EnemyManager manager, NavMeshAgent agent, ThiefAnimationController animator,
            RigBuilder rigBuilder, StealBarController stealBar, EnemyData data, EnemyTypes types)
        {
            _manager = manager;
            _agent = agent;
            _thiefAnimationController = animator;
            _rigBuilder = rigBuilder;
            _stealBar = stealBar;
            _data = data;
            _types = types;
        }

        public void Tick()
        {
            if (_manager.RobbableTargets[0] && !isStartSteal)
            {
                _agent.SetDestination(_manager.RobbableTargets[0].position);
                _manager.transform.LookAt(_manager.RobbableTargets[0].position);

                CheckAttackDistance();
            }

            if (isStartSteal)
            {
                _stealBar.gameObject.SetActive(true);
                _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Idle);
                _agent.speed = 0;

                _counter += Time.deltaTime;
                _stealBar.SetHealth(_counter);
                Vector3 relative = _manager.transform.InverseTransformPoint(_manager.RobbableTargets[0].position);
                float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
                _manager.transform.Rotate(0, angle, 0);
                if (_counter >= _theftTime)
                {
                    _manager.RobbableTargets.RemoveAt(0);
                    _stealBar.SetHealth(0);
                    _stealBar.gameObject.SetActive(false);
                }
            }
        }

        public void OnEnter()
        {
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Run);
            _agent.speed = 1;
            _rigBuilder.enabled = true;
            _theftTime = _data.EnemyTypeDatas[_types].TheftTime;
            _attackRange = _data.EnemyTypeDatas[_types].AttackRange;
        }

        public void OnExit()
        {
            _rigBuilder.enabled = false;
            _counter = 0;
            _thiefAnimationController.ResetAnim(EnemyAnimationsTypes.Idle);
            _stealBar.SetHealth(0);
            _stealBar.gameObject.SetActive(false);
            CoreGameSignals.Instance.onStealFinish?.Invoke();
        }

        private void CheckAttackDistance()
        {
            if (_agent.remainingDistance < _attackRange - 1f)
            {
                isStartSteal = true;
            }
        }
    }
}