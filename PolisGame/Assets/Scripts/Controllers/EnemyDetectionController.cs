using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionController : MonoBehaviour
{
    private EnemyManager _manager;
    private void Awake()
    {
        _manager = gameObject.GetComponentInParent<EnemyManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerPhysicsController player))
        {
            _manager.PlayerTarget = player.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _manager.PlayerTarget = null;
            Debug.Log("enemy çıktı");
        }
    }
}
