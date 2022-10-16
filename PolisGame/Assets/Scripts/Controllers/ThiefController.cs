using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ThiefController : MonoBehaviour
{
    private NavMeshAgent _opponent;
    
    private Rigidbody _rb;
    private bool _isRun;
    private ThiefAnimationController _thiefAnimationController;

    public float health = 100;
    [SerializeField] private HealthBarController _healthBarController;

    private void Start()
    {
        _thiefAnimationController = GetComponent<ThiefAnimationController>();
        _opponent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        FindTheTargets();
    }

    private void LateUpdate()
    {
        if (_isRun)
        {
            if (health>=50)
            {
                _thiefAnimationController.RunAnimation(); 
            }
            else
            {
                _thiefAnimationController.InjuredRunAnimation();
                
            }
        }
        if (_opponent.remainingDistance <= _opponent.stoppingDistance )
        {
            _rb.velocity=Vector3.zero;
            _opponent.speed = 0;
            _isRun = false;
           _thiefAnimationController.RobAnimation();
        }

        
        if (health<=0)
        {
         _thiefAnimationController.DeathAnimation();
         gameObject.layer = 0;
         _healthBarController.DestroyHealthBar();
         enabled = false;
        }
    }

   
    private void FindTheTargets()
    {
        var targetObjects = GameObject.FindGameObjectsWithTag("nesne");
        var _target = targetObjects[Random.Range(0, targetObjects.Length)].transform.position;
        
        _opponent.SetDestination(_target);
        _isRun = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("nesne"))
        {
            Destroy(other.gameObject);
            health -= 10;
            _healthBarController.DecreaseDamage();
            
        }
    }
}
