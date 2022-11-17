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
    
    private EnemyBaseState _currentState;
    [SerializeField] private ThiefAnimationController thiefAnimationController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] internal HealthBarController healthBarController;
    public bool _inAttack;

    private StateMachine _stateMachine;
   

    // private EnemyAttack _enemyAttackState;

    [ShowInInspector] public float health;

    private EnemyData _data;
    [SerializeField] protected EnemyTypes types;

    private Transform _targetArea;
    
    [SerializeField] private Transform _target;
    private float _moveSpeed;
    private float _walkSpeed;

    #region States

    private DeathState _deathState;
    private BirthState _birthState;
    private MoveStates _moveState;
    private WalkState _walkState;
    

    #endregion



    private void Awake()
    {
        _data = Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyData;
        types = EnemyTypes.Beginner;
        health = _data.EnemyTypeDatas[types].Health;
        _moveSpeed=_data.EnemyTypeDatas[types].MoveSpeed;
        _walkSpeed = _data.EnemyTypeDatas[types].walkSpeed;
        Debug.Log(_moveSpeed);
        GetReferences();

    }

    private void Start()
    {
        SwitchState(EnemyStatesTypes.Run);


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
    private void Update() => _stateMachine.Tick();
   

    public void SwitchState(EnemyStatesTypes state)
    {


        // _currentState.EnterState();
    }

    public void SetTriggerAnim(EnemyAnimationsTypes enemyAnimationsTypes)
    {

        thiefAnimationController.SetAnim(enemyAnimationsTypes);
    }

    public bool GetHealth()
    {
        return health <= 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            OnTakeDamage();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Robbable"))
        {
            Debug.Log("dokundu");
            _target = other.transform;
            CurrentTarget = _target;
        }
       
    }
    
    
    public void OnTakeDamage()
    {
        health -= 10;
        healthBarController.DecreaseDamage();
    }

    void GetReferences()
    {
       
        var manager = this;
        
        // _enemyDeadState = new EnemyDead(ref manager, thiefAnimationController,agent);
        _deathState = new DeathState(manager,agent,thiefAnimationController);
        _birthState = new BirthState();
        _moveState  = new MoveStates(manager,agent,_moveSpeed,thiefAnimationController,_target);
        _walkState = new WalkState(manager, agent, _walkSpeed, thiefAnimationController);
        //
        
        _stateMachine = new StateMachine();
         At(_birthState,_moveState,HasAnyTarget());
         At(_birthState,_walkState,HasTarget());
         At(_moveState,_walkState,HasTarget());
         At(_walkState,_moveState,HasAnyTarget());
        
        
        
        
        _stateMachine.AddAnyTransition(_deathState, AmIDead());
        
        
        _stateMachine.SetState(_birthState);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> AmIDead() => () => health <= 0;
        Func<bool> HasAnyTarget() => () => _target != null;
        Func<bool> HasTarget() => () => _target == null;
    }

    
}
