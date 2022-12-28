using System.Collections;
using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     [ContextMenu("SpawnEnemy")]
    public void SpawnEnemy()
    {
        PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.AmateurRobber.ToString(), this.transform);
    }
}
