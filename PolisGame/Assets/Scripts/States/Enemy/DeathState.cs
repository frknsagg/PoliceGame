using System.Collections;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class DeathState : IState
{
    private NavMeshAgent _agent;
    private EnemyManager _manager;
    private ThiefAnimationController _thiefAnimationController;
    public DeathState(EnemyManager manager,NavMeshAgent agent,ThiefAnimationController thiefAnimationController)
    {
        _manager = manager;
        _agent = agent;
        _thiefAnimationController = thiefAnimationController;
    }
    public void Tick()
    {
        Debug.Log("dead tick");
    }

    public void OnEnter()
    {
        _manager.gameObject.layer = 0;
        _agent.enabled = false;
        _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Death);
        
        
    }

    public void OnExit()
    {
        Debug.Log("dead onexit");
    }
}
