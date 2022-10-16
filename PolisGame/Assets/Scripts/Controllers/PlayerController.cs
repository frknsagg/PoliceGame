using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TouchController touchController;
    [SerializeField] private CharacterAnimationController animatorController;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject mermi;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private LayerMask layerMask;
    private float _counter;
    [SerializeField] float myBulletSpeed;
    
    public List<GameObject> currentHitObjects = new List<GameObject>();

    private void Start()
    {
        touchController.OnPointerDragEvent += Move;
    }


    private void Move(Vector3 direction,Vector3 rotation)
    {
         direction = new Vector3(touchController.direction.x,0,touchController.direction.y);
         rotation = new Vector3(touchController.rotation.x, 0, touchController.rotation.y);
        _rigidbody.velocity = direction * (moveSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
       
    }
    private void Rotate(Vector3 rotation)
    {
        transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
    }
    

    // private void FixedUpdate()
    // {
    //     
    //     currentHitObjects.Clear();
    //     var direction = new Vector3(touchController.direction.x,0,touchController.direction.y);
    //     var rotation = new Vector3(touchController.rotation.x, 0, touchController.rotation.y);
    //     
    //     Move(direction);
    //     if (direction==Vector3.zero)
    //     {
    //         animatorController.IdleAnimation();
    //         DetectEnemy(transform.position,1.5f);
    //         _counter += Time.deltaTime;
    //         _rigidbody.angularVelocity=Vector3.zero;
    //         if (currentHitObjects.Count>=1 )
    //         {
    //             animatorController.FireAnimation();
    //             if (_counter>=0.5f)
    //             {
    //                 Fire();
    //                 _counter = 0;
    //             }
    //         }
    //     }
    //     else if(direction!=Vector3.zero)
    //     {
    //         animatorController.RunAnimation();
    //         
    //         Rotate(rotation);
    //     }
    //     else
    //     {
    //         _rigidbody.angularVelocity=Vector3.zero;
    //        
    //     }
    //
    // }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Fire()
    {
        Vector3 vec2 = currentHitObjects[0].transform.position;
        vec2.y = 0.0f;
        transform.LookAt(vec2);
        
        //------------------Bullet Section-------
        GameObject myBulletPrefabClone = Instantiate(mermi, bulletSpawn.position,bulletSpawn.rotation);
        var myBulletRigidbody = myBulletPrefabClone.GetComponent<Rigidbody>();

        Vector3 vec = (currentHitObjects[0].transform.position - bulletSpawn.position).normalized;
        myBulletRigidbody.AddForce(vec.x*myBulletSpeed,0,vec.z*myBulletSpeed,ForceMode.Impulse);

    }

    private void DetectEnemy(Vector3 center ,float radius){
    
        var hitColliders = Physics.OverlapSphere(center, radius,layerMask);
        
        for (var i = 0; i < hitColliders.Length; i++)
        {
            currentHitObjects.Add(hitColliders[i].gameObject);
        }
    }

    private void DeActivateMovement()
    {
        
    }

}
