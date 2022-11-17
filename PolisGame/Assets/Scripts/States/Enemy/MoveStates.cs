using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.AI;

public class MoveStates : IState
{
    private EnemyManager _manager;
    private float _moveSpeed;
    private ThiefAnimationController _thiefAnimationController;
    private NavMeshAgent _agent;
    private Transform _target;
    public MoveStates(EnemyManager manager,NavMeshAgent agent,float moveSpeed,ThiefAnimationController thiefAnimationController,Transform target)
    {
        _manager = manager;
        _agent = agent;
        _moveSpeed = moveSpeed;
        _thiefAnimationController = thiefAnimationController;
        _target = target;
    }
    public void Tick()
    {
        if (_manager.CurrentTarget)
        {

            _agent.destination = _manager.CurrentTarget.transform.position;
        }
    }

    public void OnEnter()
    {
        
        if (_manager.CurrentTarget)
        {
            Quaternion.LookRotation(_manager.CurrentTarget.transform.position);
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Run);
            _agent.enabled = true;
            _agent.speed = _moveSpeed;
            
        }
       
    }

    public void OnExit()
    {
        Debug.Log("Move onexit");
    }

   
}
