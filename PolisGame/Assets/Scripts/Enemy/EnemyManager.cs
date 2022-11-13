using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Cinemachine;
using Controllers;
using Enemy;
using Enums;
using Sirenix.OdinInspector;
using States.Enemy;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyManager : MonoBehaviour
{
    private EnemyBaseState _currentState;
    [SerializeField] private ThiefAnimationController thiefAnimationController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] internal HealthBarController healthBarController;

    private EnemyAttack _enemyAttackState;
    private EnemyDead _enemyDeadState;
    private EnemySteal _enemyStealState;
    private EnemyRun _enemyRunState;
    [ShowInInspector]
    public float health;

    private EnemyData _data;
    [SerializeField] protected EnemyTypes types;

    private Transform _targetArea;
    
    

    private void Awake()
    {
        types = EnemyTypes.Professional;
        GetReferences();
        health = _data.EnemyTypeDatas[types].Health;
       
    }

    private void Start()
    {
        SwitchState(EnemyStatesTypes.Run);
        FindTheTargets();
    }

    private void Update()
    {
        _currentState.UpdateState();
        if (GetHealth())
        {
            SwitchState(EnemyStatesTypes.Death);
            enabled = false;
        }
    }

    public void SwitchState(EnemyStatesTypes state)
    {
      
        switch (state )
        {
            case EnemyStatesTypes.Attack:
                _currentState = _enemyAttackState;
                break;
            case EnemyStatesTypes.InjuredRun:
                _currentState = _enemyRunState;
                break;
            case EnemyStatesTypes.Run:
                _currentState = _enemyRunState;
                break;
            case EnemyStatesTypes.Steal:
                _currentState = _enemyStealState;
                break;
            case EnemyStatesTypes.Death:
                _currentState = _enemyDeadState;
                break;
            
        }
        _currentState.EnterState();
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
       _currentState.OnTriggerEnter(other);
       Destroy(other.gameObject);
   }

   void GetReferences()
   {
       var manager = this;
       _data = Resources.Load<CD_Enemy>("Data/CD_Enemy").EnemyData;
       _enemyDeadState = new EnemyDead(ref manager, thiefAnimationController,agent);
       _enemyAttackState = new EnemyAttack(ref manager, ref thiefAnimationController,agent);
       _enemyStealState = new EnemySteal(manager, thiefAnimationController, agent);
       _enemyRunState = new EnemyRun(ref manager, thiefAnimationController, agent);
   }
   
  

   public void OnTakeDamage()
   {
       health -= 10;
       healthBarController.DecreaseDamage();
   }
   
   private void FindTheTargets()
   {
       var targetObjects = GameObject.FindGameObjectsWithTag("Robbable");
       _targetArea = targetObjects[Random.Range(0, targetObjects.Length)].transform;
       transform.LookAt(_targetArea);
        
       agent.SetDestination(_targetArea.position);
      
   }

  public IEnumerator StealFromNpc()
  {
      var animator = GetComponent<Animator>();
      
       float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
       // yield return new WaitForSecondsRealtime(animationLength);
       Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
       yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1);
       FindTheTargets();
       SwitchState(EnemyStatesTypes.Run);
       
   }

  public void CallStealFromNpc()
  {
      StartCoroutine(nameof(StealFromNpc));
  }
}
