using Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace States.Enemy
{
    public class WalkState : IState
    {
        private readonly bool _anyTarget = false;
        public bool ATarget() => _anyTarget;
        
        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private float _walkSpeed;
        private ThiefAnimationController _thiefAnimationController;
        
        private Vector3? _destination;
        private Vector3 lastPosition = Vector3.zero;
        private Quaternion _lookRotation;
        private Vector3 _direction;
        private float _timeStack;
        
        
       
        public WalkState(EnemyManager manager, NavMeshAgent agent, float walkSpeed,ThiefAnimationController thiefAnimationController)
        {
            _manager = manager;
            _agent = agent;
            _walkSpeed = walkSpeed;
            _thiefAnimationController = thiefAnimationController;
        }
        public void Tick()
        {
            if (_destination.HasValue!=true ||Vector3.Distance(_manager.transform.position,
                    _destination.Value) <= _agent.stoppingDistance )
            {
                FindRandomDestination();
                if (_destination != null) _agent.destination = _destination.Value;
            }
            if (Vector3.Distance(-_manager.transform.position, lastPosition) <= 0)
            {
                _timeStack += Time.deltaTime;
                if (_timeStack > 1f)
                {
                    _manager.transform.rotation = Quaternion.Lerp(_manager.transform.rotation, _lookRotation, 0.2f);
                    _timeStack = 0;
                }
                else
                {
                    FindRandomDestination();
                }
            }
            lastPosition = _manager.transform.position;
        }

        public void OnEnter()
        {
            _agent.speed = _walkSpeed;
            _manager.SetTriggerAnim(EnemyAnimationsTypes.Walk);
        }

        public void OnExit()
        {
            Debug.Log("walk exit");
        }
        private void FindRandomDestination()
        {
            var randomPositionX = Random.Range(-13, 13);
            var randomPositionZ = Random.Range(-13, 13);
            Vector3 randomPositionVector = new Vector3(randomPositionX, 0, randomPositionZ);
            _destination = new Vector3(randomPositionVector.x, _manager.transform.position.y, randomPositionVector.z);
            _direction = Vector3.Normalize(_destination.Value );
            Debug.Log(_destination);
            _direction = new Vector3(_direction.x, 0, _direction.z);
             // _lookRotation = Quaternion.LookRotation(_direction);
             _manager.transform.LookAt(_direction);
        }
    }
}
