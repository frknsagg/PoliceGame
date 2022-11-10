using System.Collections;
using System.Collections.Generic;
using Abstract;
using Controllers;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyDead : EnemyBaseState
{
    private EnemyManager _manager;
    private ThiefAnimationController _thiefAnimationController;
    private NavMeshAgent _agent;
   public EnemyDead(ref EnemyManager manager,ThiefAnimationController thiefAnimationController,NavMeshAgent agent)
    {
        _manager = manager;
        _thiefAnimationController = thiefAnimationController;
        _agent = agent;
    }


   public override void EnterState()
    {
        _manager.SetTriggerAnim(EnemyAnimationsTypes.Death);
        _manager.gameObject.layer = 0;
        
        _manager.transform.DOJump(new Vector3(_agent.transform.position.x, -0.5f, _agent.transform.position.z + 2),
            1,
            1, 2f);
        _agent.enabled = false;
    }

    public override void UpdateState()
    {
        Debug.Log("DeathState update");
    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }

   
}
