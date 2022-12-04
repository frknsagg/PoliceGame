using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Cinemachine;
using Controllers;
using Enums;
using Sirenix.OdinInspector;
using States.Enemy;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyManager : MonoBehaviour
{
    public Transform CurrentTarget;
    public Transform PlayerTarget;
    public Transform MarketTarget;
    
    private EnemyBaseState _currentState;
    [SerializeField] private ThiefAnimationController thiefAnimationController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] internal HealthBarController healthBarController;
    public bool _inAttack;

    private StateMachine _stateMachine;
   

    // private EnemyAttack _enemyAttackState;

    [ShowInInspector] public float Health;

    private EnemyData _data;
    [SerializeField] protected EnemyTypes types;

    private Transform _targetArea;
    
    [SerializeField] public Transform _target;
    private float _moveSpeed;
    private float _walkSpeed;
    private float _attackRange;
    private float _feverFrequency;

    #region States
    private WalkState _walkState;
    private DeathState _deathState;
    private ChaseState _chaseState;
    private RunState _runState;
    private StealState _stealState;
    private AttackState _attackState;
    
    #endregion
    
    [SerializeField] private GameObject mermi;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] float myBulletSpeed;



    private void Awake()
    {
        _data = Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyData;
        types = EnemyTypes.Beginner;
        Health = _data.EnemyTypeDatas[types].Health;
        _moveSpeed=_data.EnemyTypeDatas[types].MoveSpeed;
        _walkSpeed = _data.EnemyTypeDatas[types].walkSpeed;
        _attackRange = _data.EnemyTypeDatas[types].AttackRange;
        _feverFrequency = _data.EnemyTypeDatas[types].FeverFrequency;
        
        GetReferences();

    }

    private void Start()
    {
    }

    // private void Update()
    // {
    //     // _currentState.UpdateState();
    //     if (GetHealth())
    //     {
    //         SwitchState(EnemyStatesTypes.Death);
    //         enabled = false;
    //     }
    //     
    // }
    private void Update()
    {
        _stateMachine.Tick();
        
    }
    public void SetTriggerAnim(EnemyAnimationsTypes enemyAnimationsTypes)
    {

        thiefAnimationController.SetAnim(enemyAnimationsTypes);
    }

    public bool GetHealth()
    {
        return Health <= 0;
    }

    
    public void OnTakeDamage()
    {
        healthBarController.SetHealth();
    }

    void GetReferences()
    {
       
        var manager = this;
        
        // _enemyDeadState = new EnemyDead(ref manager, thiefAnimationController,agent);
        _deathState = new DeathState(manager,agent,thiefAnimationController);
        _walkState = new WalkState(manager, agent, _walkSpeed, thiefAnimationController);
        _chaseState = new ChaseState(manager,agent,_moveSpeed,thiefAnimationController,_attackRange);
        _attackState = new AttackState(agent, thiefAnimationController, manager, _attackRange,_feverFrequency);
        
        
        _stateMachine = new StateMachine();
        At(_walkState,_chaseState,HasPlayerTarget());
        At(_chaseState,_attackState,IsPlayerInAttackRange());
        At(_attackState,_chaseState,HasPlayerTarget());
        At(_chaseState,_walkState,HasNoTarget());
        At(_attackState,_walkState,HasNoTarget());
        
        
        
        _stateMachine.AddAnyTransition(_deathState, AmIDead());
        _stateMachine.SetState(_walkState);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        
        Func<bool> AmIDead() => () => Health <= 0;
        Func<bool> HasPlayerTarget() => () =>
            PlayerTarget != null && DistanceToPlayer() > _attackRange && !_attackState.PlayerExitAttackRange();
        Func<bool> IsPlayerInAttackRange() => () =>  DistanceToPlayer() <= _attackRange;
        Func<bool> HasNoTarget() => () => PlayerTarget == null; 

    }

    private float DistanceToPlayer()
    {
        if (PlayerTarget)
        {
            var distance = Vector3.Distance(PlayerTarget.position , transform.position);
            return distance;
        }
        return 100;
    }

    public void Fire()
    {
        GameObject myBulletPrefabClone = Instantiate(mermi, bulletSpawn.position,bulletSpawn.rotation);
        var myBulletRigidbody = myBulletPrefabClone.GetComponent<Rigidbody>();

        Vector3 vec = (PlayerTarget.position - bulletSpawn.position).normalized;
        myBulletRigidbody.AddForce(vec.x*myBulletSpeed,0,vec.z*myBulletSpeed,ForceMode.Impulse);
    }


}
