using Controllers;
using DG.Tweening;
using Enemy;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.Enemy
{
    public class DeathState : IState
    {
        private NavMeshAgent _agent;
        private EnemyManager _manager;
        private ThiefAnimationController _thiefAnimationController;
        private HealthBarController _healthBarController;
        private float _deathTimer;
        private bool isFinish;
        public DeathState(EnemyManager manager,NavMeshAgent agent,ThiefAnimationController thiefAnimationController,HealthBarController healthBarController)
        {
            _manager = manager;
            _agent = agent;
            _thiefAnimationController = thiefAnimationController;
            _healthBarController = healthBarController;
        }
        public void Tick()
        {
            if (isFinish)
            {
                _deathTimer += Time.deltaTime;
                if (_deathTimer>=1.5f)
                {
                    PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.AmateurRobber, _manager.gameObject);
                }
            }
        }

        public void OnEnter()
        {
            _manager.gameObject.layer = 0;
            _agent.enabled = false;
            _healthBarController.gameObject.SetActive(false);
            _thiefAnimationController.SetAnim(EnemyAnimationsTypes.Death);
            DropMoney();
            _manager.PlayerTarget = null;
            LevelManager.Instance.enemyList.Remove(_manager.gameObject);
        }

        public void OnExit()
        {
            
        }

        private void DropMoney()
        {
            for (int i = 0; i < 3; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Money.ToString(),
                    _manager.gameObject.transform);
                obj.transform.DOJump(new Vector3(obj.transform.position.x + Random.Range(-1, 1),
                        obj.transform.position.y + Random.Range(0.5f, 2),
                        obj.transform.position.z + Random.Range(-1, 1)),
                    2, 1, 0.5f).OnComplete(() => isFinish=true);
            }
        }
    }
}
