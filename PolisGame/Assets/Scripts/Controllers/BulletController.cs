
using Data.ValueObject;
using Enums;
using UnityEngine;

namespace Controllers
{
   public class BulletController : MonoBehaviour,IDamager
   {
      private GunData gunData;

      private GunTypes _gunTypes;

      private int _damage;
      private void Start()
      {
         Invoke(nameof(DestroyGameObject),5);
        
      }

      void DestroyGameObject()
      {
         Destroy(gameObject);
      }

      public void SetGunTypeData(GunTypes gunSpecs,GunData gunData)
      {
         this.gunData = gunData;
         _gunTypes = gunSpecs;
      }

      public int Damage()
      {
         return gunData.GunDatas[_gunTypes].Damage;
      }
   }
}
