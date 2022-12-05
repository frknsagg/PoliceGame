using System;
using Abstract;
using Controllers;
using Enums;
using Sirenix.OdinInspector;
using States.Enemy;
using UnityEngine;
using UnityEngine.AI;



public class EnemyManager : MonoBehaviour
{
    public Transform CurrentTarget;
    public Transform PlayerTarget;
    public Transform MarketTarget;
    
    private EnemyBaseState _currentState;
    [SerializeField] private ThiefAnimationController thiefAnimationController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] internal HealthBarController healthBarController;
    [SerializeField] private GunController _gunController;

    private StateMachine _stateMachine;
   

    // private EnemyAttack _enemyAttackState;

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

  


}
