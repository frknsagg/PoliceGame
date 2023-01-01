using System;
using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
   public class BulletController : MonoBehaviour,IDamager
   {
      public ParticleSystem particleSystem;
      private List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
      public GameObject spark;
      private GunData _gunData;

      private GunTypes _gunTypes;

      private int _damage;
      private void OnEnable()
      {
         // Invoke(nameof(ReleaseBullet),2);
         particleSystem.Play();
      }

      void ReleaseBullet()
      {
         PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolType.Bullet,gameObject);
      }

      public void SetGunTypeData(GunTypes gunSpecs,GunData gunData)
      {
         _gunData = gunData;
         _gunTypes = gunSpecs;
      }

      public int Damage()
      {
         return _gunData.GunDatas[_gunTypes].Damage;
      }

      private void OnParticleCollision(GameObject other)
      {
        int events= particleSystem.GetCollisionEvents(other, colEvents);
        Debug.Log("hit");
        for (int i = 0; i < events; i++)
        {
           
        }
      }
   }
}
