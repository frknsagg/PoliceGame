using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Enums;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private BulletController _bulletController;
    private GunData _gunData;
    private GunTypes _gunTypes;
    
    [SerializeField] private GameObject mermi;
    private  float _myBulletSpeed;
    private void Awake()
    {
        _gunData = Resources.Load<CD_Gun>("Data/CD_Gun").GunData;
        _gunTypes = GunTypes.Ak47;
        _myBulletSpeed = _gunData.GunDatas[_gunTypes].BulletSpeed;
        
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SendDataToControllers()
    {
        _bulletController.SetGunTypeData(_gunTypes,_gunData);
    }
    // ReSharper disable Unity.PerformanceAnalysis
  [ContextMenu("Fire")]
    public void Fire()
    {
        GameObject myBulletPrefabClone = Instantiate(mermi, transform.position,transform.rotation);
        myBulletPrefabClone.GetComponent<BulletController>().SetGunTypeData(_gunTypes,_gunData);
        var rbVelocity = myBulletPrefabClone.GetComponent<Rigidbody>().velocity;
        rbVelocity.x = transform.forward.normalized.x * _myBulletSpeed;
        rbVelocity.z = transform.forward.normalized.z * _myBulletSpeed;
        myBulletPrefabClone.GetComponent<Rigidbody>().velocity = rbVelocity;
    }
}
