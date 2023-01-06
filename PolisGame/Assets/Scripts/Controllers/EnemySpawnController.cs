using System;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EnemySpawnController : MonoBehaviour
    {
        private Transform _randomTransform;

        // [ContextMenu("SpawnEnemy")]
        public void SpawnEnemy(EnemyTypes enemyTypes)
        {
            var a = PoolType.AmateurRobber;
            var obj = PoolSignals.Instance.onGetPoolObject.Invoke(enemyTypes.ToString(),
                TranslateTransform(RandomNavmeshLocation()));
            LevelManager.Instance.enemyList.Add(obj);
        }

        [ContextMenu("RandomNavmeshLocations")]
        public Vector3 RandomNavmeshLocation()
        {
            float radius = 15;
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

        private Transform TranslateTransform(Vector3 vec)
        {
            _randomTransform = transform;
            _randomTransform.position = vec;
            return _randomTransform;
        }
    }
}