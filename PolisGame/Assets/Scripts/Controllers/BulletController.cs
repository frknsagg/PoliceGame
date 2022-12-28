using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
   public class BulletController : MonoBehaviour,IDamager
   {
      private GunData _gunData;

      private GunTypes _gunTypes;

      private int _damage;
      private void OnEnable()
      {
         Invoke(nameof(ReleaseBullet),2);
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
   }
}
