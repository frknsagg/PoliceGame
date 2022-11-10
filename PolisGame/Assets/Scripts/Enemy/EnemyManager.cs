using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Controllers;
using Enemy;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private EnemyBaseState _currentState;
    [SerializeField] private ThiefAnimationController _thiefAnimationController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private HealthBarController healthBarController;

    private EnemyAttack _enemyAttackState;
    private EnemyDead _enemyDeadState;
    [ShowInInspector]
    private float _health;
   
    private EnemyData _data;
    [SerializeField] private EnemyTypes types;
    
    

    private void Awake()
    {
        GetReferences();
    }

    private void Start()
    {
        _currentState = _enemyAttackState;
        _currentState.EnterState();
        _health = _data.EnemyTypeDatas[types].Health;
        Debug.Log(_health);
        FindTheTargets();

    }

    private void Update()
    {
        _currentState.UpdateState();
        if (GetHealth())
        {
            SwitchState(EnemyStatesTypes.Death);
            healthBarController.DestroyHealthBar();
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
            case EnemyStatesTypes.MoveMineTnt:
                break;
            case EnemyStatesTypes.InjuredRun:
                break;
            case EnemyStatesTypes.Steal:
                break;
            case EnemyStatesTypes.Death:
                _currentState = _enemyDeadState;
                break;
            
        }
        _currentState.EnterState();
    }
   public void SetTriggerAnim(EnemyAnimationsTypes enemyAnimationsTypes)
   {
      
       _thiefAnimationController.SetAnim(enemyAnimationsTypes);
   }

   public bool GetHealth()
   {
       return _health <= 0;
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
       _enemyDeadState = new EnemyDead(ref manager, _thiefAnimationController,agent);
       _enemyAttackState = new EnemyAttack(ref manager, ref _thiefAnimationController,agent);
   }
   
   private void FindTheTargets()
   {
       var targetObjects = GameObject.FindGameObjectsWithTag("nesne");
       var target = targetObjects[Random.Range(0, targetObjects.Length)].transform.position;
        
       agent.SetDestination(target);
      
   }

   public void OnTakeDamage()
   {
       _health -= 10;
       healthBarController.DecreaseDamage();
   }
}
