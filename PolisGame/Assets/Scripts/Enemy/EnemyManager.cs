using System;
using System.Collections.Generic;
using Abstract;
using Controllers;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using States.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;


public class EnemyManager : MonoBehaviour
{
    
    public Transform PlayerTarget;
    public List<Transform> RobbableTargets;
    
    private EnemyBaseState _currentState;
    [SerializeField] private RigBuilder rigBuilder;
    [SerializeField] private ThiefAnimationController thiefAnimationController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] internal HealthBarController healthBarController;
    [SerializeField] private GunController _gunController;

    private StateMachine _stateMachine;
   

    // private EnemyAttack _enemyAttackState;
    public int moneyCount;
    [ShowInInspector] public float Health;

    private EnemyData _data;
    [SerializeField] protected EnemyTypes types;

    [SerializeField] private GunData _gunData;
    [SerializeField] private GunTypes _gunTypes;

    private Transform _targetArea;
    
    [SerializeField] public Transform _target;
    private float _moveSpeed;
    private float _walkSpeed;
    private float _attackRange;
    private float _feverFrequency;
    private float _theftTime;

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
        _data = Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyData;
        GetEnemyData();
        GetReferences();

    }

    private void GetEnemyData()
    {
        types = EnemyTypes.Beginner;
        Health = _data.EnemyTypeDatas[types].Health;
        _moveSpeed=_data.EnemyTypeDatas[types].MoveSpeed;
        _walkSpeed = _data.EnemyTypeDatas[types].walkSpeed;
        _attackRange = _data.EnemyTypeDatas[types].AttackRange;
        _feverFrequency = _data.EnemyTypeDatas[types].FeverFrequency;
        _theftTime = _data.EnemyTypeDatas[types].TheftTime;

    }

    private void Start()
    {
    }
    private void Update()
    {
        _stateMachine.Tick();
        
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
        
        // _enemyDeadState = new EnemyDead(ref manager, thiefAnimationController,agent);
        _deathState = new DeathState(manager,agent,thiefAnimationController,healthBarController);
        _walkState = new WalkState(manager, agent, _walkSpeed, thiefAnimationController);
        _chaseState = new ChaseState(manager,agent,_moveSpeed,thiefAnimationController,_attackRange);
        _attackState = new AttackState(agent, thiefAnimationController, manager, _attackRange,_feverFrequency,_gunController);
        _stealState = new StealState(manager, agent, thiefAnimationController, _theftTime, rigBuilder);
        
        
        _stateMachine = new StateMachine();
        At(_walkState,_chaseState,HasPlayerTarget());
        At(_chaseState,_attackState,IsPlayerInAttackRange());
        At(_attackState,_chaseState,HasPlayerTarget());
        At(_chaseState,_walkState,HasNoTarget());
        At(_attackState,_walkState,HasNoTarget());
        At(_walkState,_stealState,HasRobableStuff());
        At(_chaseState,_stealState,HasRobableStuff());
        At(_stealState,_attackState,IsPlayerInAttackRange());
        At(_stealState,_walkState,HasNoTarget());
        At(_stealState,_chaseState,HasPlayerTarget());
        
        _stateMachine.AddAnyTransition(_deathState, AmIDead());
        _stateMachine.SetState(_walkState);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        
        Func<bool> AmIDead() => () => Health <= 0;

        Func<bool> HasPlayerTarget() => () =>
            (PlayerTarget != null || RobbableTargets.Count > 0) && DistanceToX(PlayerTarget) > _attackRange;

        Func<bool> IsPlayerInAttackRange() => () => PlayerTarget != null && DistanceToX(PlayerTarget) < _attackRange;
        Func<bool> HasNoTarget() => () => PlayerTarget == null && RobbableTargets.Count == 0;

        Func<bool> HasRobableStuff() => () =>
            RobbableTargets.Count > 0 && DistanceToX(RobbableTargets[0]) < _attackRange && PlayerTarget == null;

    }

    private float DistanceToX(Transform x)
    {
        if (x!=null)
        {
            var distance = Vector3.Distance(x.position , transform.position);
            return distance;
        }
        return 100;
    }
    

  


}
