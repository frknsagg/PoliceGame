using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class EnemySpawnController : MonoBehaviour
    {
        [ContextMenu("SpawnEnemy")]
        public void SpawnEnemy()
        {
            PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.AmateurRobber.ToString(), this.transform);
        }
    }
}