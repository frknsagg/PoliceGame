using Controllers;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace States.Enemy
{
    public class StealState : IState
    {
        private EnemyManager _manager;
        private ThiefAnimationController _thiefAnimationController;
        private NavMeshAgent _agent;
        private float _theftTime;
        private RigBuilder _rigBuilder;
        private float _counter;
       
        public StealState(EnemyManager manager,NavMeshAgent agent,ThiefAnimationController animator,float theftTime,RigBuilder rigBuilder)
        {
            _manager = manager;
            _agent = agent;
            _thiefAnimationController = animator;
            _theftTime = theftTime;
            _rigBuilder = rigBuilder;
        }
        public void Tick()
        {
            _counter += Time.deltaTime;
            if (_counter>=_theftTime)
            {
                _manager.RobbableTargets.RemoveAt(0);
                
            }
        }

        public void OnEnter()
        {
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Idle);
            _rigBuilder.enabled = true;
            Debug.Log("steal on enter");
        }

        public void OnExit()
        {
            _rigBuilder.enabled = false;
            _counter = 0;
        }
    }
}
