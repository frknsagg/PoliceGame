using System;
using System.Collections.Generic;
using Abstract;
using Controllers;
using Enums;
using Sirenix.OdinInspector;
using States.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public Transform PlayerTarget;
        public List<Transform> RobbableTargets;

        private EnemyBaseState _currentState;
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private ThiefAnimationController thiefAnimationController;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] internal HealthBarController healthBarController;
        [SerializeField] internal StealBarController stealBarController;
        [SerializeField] private GunController _gunController;
        [SerializeField] private GameObject gun;

        private StateMachine _stateMachine;


        // private EnemyAttack _enemyAttackState;
        public int moneyCount;
        [SerializeField] private GameObject money;
        [ShowInInspector] public float Health;

        private EnemyData _data;
        [SerializeField] protected EnemyTypes types;

        [SerializeField] private GunData _gunData;
        [SerializeField] private GunTypes _gunTypes;

        private Transform _targetArea;

        private float _moveSpeed;
        private float _walkSpeed;
        private float _attackRange;
        private float _feverFrequency;
        public float _theftTime;

        #region States

        private WalkState _walkState;
        private DeathState _deathState;
        private ChaseState _chaseState;
        private RunState _runState;
        private StealState _stealState;
        private AttackState _attackState;

        #endregion


        private void Awake()
        {
            GetReferences();
            SendDataToControllers();
        }

        private void OnEnable()
        {
            types = EnemyTypes.AmateurRobber;
            Health = _data.EnemyTypeDatas[types].Health;
            _attackRange = _data.EnemyTypeDatas[types].AttackRange;
            _stateMachine.SetState(_walkState);
            healthBarController.gameObject.SetActive(true);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void SendDataToControllers()
        {
            healthBarController.GetData(_data, types);
            stealBarController.GetData(_data, types);
        }

        public void SetTriggerAnim(EnemyAnimationsTypes enemyAnimationsTypes)
        {
            thiefAnimationController.SetAnim(enemyAnimationsTypes);
        }

        public void OnTakeDamage()
        {
            healthBarController.SetHealth();
        }

        void GetReferences()
        {
            var manager = this;
            _data = Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyData;
            _deathState = new DeathState(manager, agent, thiefAnimationController, healthBarController);
            _walkState = new WalkState(manager, agent, _data, types,rigBuilder,gun);
            _chaseState = new ChaseState(manager, agent, _moveSpeed, thiefAnimationController, _data, types);
            _attackState = new AttackState(agent, thiefAnimationController, manager,
                _gunController, _data, types,rigBuilder,gun);
            _stealState = new StealState(manager, agent, thiefAnimationController, rigBuilder,
                stealBarController, _data, types);

            _stateMachine = new StateMachine();
            At(_walkState, _chaseState, HasPlayerTarget());
            At(_chaseState, _attackState, IsPlayerInAttackRange());
            At(_attackState, _chaseState, HasPlayerTarget());
            At(_chaseState, _walkState, HasNoTarget());
            At(_attackState, _walkState, HasNoTarget());
            At(_walkState, _stealState, HasRobableStuff());
            At(_chaseState, _stealState, HasRobableStuff());
            At(_stealState, _attackState, IsPlayerInAttackRange());
            At(_stealState, _walkState, HasNoTarget());
            At(_stealState, _chaseState, HasPlayerTarget());

            _stateMachine.AddAnyTransition(_deathState, AmIDead());
            _stateMachine.SetState(_walkState);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> AmIDead() => () => Health <= 0;

            Func<bool> HasPlayerTarget() => () => PlayerTarget != null && DistanceToX(PlayerTarget) > _attackRange;

            Func<bool> IsPlayerInAttackRange() => () => PlayerTarget != null && DistanceToX(PlayerTarget) < _attackRange;
            Func<bool> HasNoTarget() => () => PlayerTarget == null && RobbableTargets.Count == 0;

            Func<bool> HasRobableStuff() => () =>
                RobbableTargets.Count > 0 && DistanceToX(RobbableTargets[0]) < _attackRange && PlayerTarget == null;
        }

        private float DistanceToX(Transform x)
        {
            if (x != null)
            {
                var distance = Vector3.Distance(x.position, transform.position);
                return distance;
            }

            return 100;
        }
    }
}