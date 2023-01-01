using Controllers;
using Enemy;
using Enums;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace States.Enemy
{
    public class WalkState : IState
    {
        private EnemyData _data;
        private EnemyTypes _types;
        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private float _walkSpeed;
        private ThiefAnimationController _thiefAnimationController;
        private RigBuilder _rigBuilder;
        private GameObject _gun;

        private Vector3? _destination;
        private Vector3 lastPosition = Vector3.zero;
        private Quaternion _lookRotation;
        private Vector3 _direction;
        private float _timeStack;


        public WalkState(EnemyManager manager, NavMeshAgent agent, EnemyData data, EnemyTypes types,
            RigBuilder rigBuilder, GameObject gun)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
            _types = types;
            _rigBuilder = rigBuilder;
            _gun = gun;
        }

        public void Tick()
        {
            if (_destination.HasValue != true || Vector3.Distance(_manager.transform.position,
                    _destination.Value) <= _agent.stoppingDistance)
            {
                // FindRandomDestination();
                RandomNavmeshLocation();
                if (_destination != null) _agent.destination = _destination.Value;
            }

            if (Vector3.Distance(-_manager.transform.position, lastPosition) <= 0)
            {
                _timeStack += Time.deltaTime;
                if (_timeStack > 1f)
                {
                    _manager.transform.rotation = Quaternion.Lerp(_manager.transform.rotation, _lookRotation, 0.5f);
                    _timeStack = 0;
                }
                else
                {
                    _destination = null;
                    // FindRandomDestination();
                    RandomNavmeshLocation();
                }
            }

            lastPosition = _manager.transform.position;
        }

        public void OnEnter()
        {
            _agent.enabled = true;
            _agent.speed = _data.EnemyTypeDatas[_types].walkSpeed;
            _manager.SetTriggerAnim(EnemyAnimationsTypes.Walk);
            _gun.SetActive(false);
            _rigBuilder.enabled = false;
        }

        public void OnExit()
        {
            _destination = null;
        }

        private void FindRandomDestination()
        {
            var randomPositionX = Random.Range(-13, 13);
            var randomPositionZ = Random.Range(-13, 13);
            Vector3 randomPositionVector = new Vector3(randomPositionX, 0, randomPositionZ);
            _destination = new Vector3(randomPositionVector.x, _manager.transform.position.y, randomPositionVector.z);
            _direction = Vector3.Normalize(_destination.Value);

            _direction = new Vector3(_direction.x, 0, _direction.z);
        }

        public Vector3 RandomNavmeshLocation()
        {
            float radius = 15;
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += _manager.transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }

            _destination = finalPosition;
            _direction = finalPosition;
            return finalPosition;
        }
    }
}