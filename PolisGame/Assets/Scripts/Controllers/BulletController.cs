
using UnityEngine;

namespace Controllers
{
   public class BulletController : MonoBehaviour,IDamager
   {
      private void Start()
      {
         Invoke(nameof(DestroyGameObject),5);
      }

      void DestroyGameObject()
      {
         Destroy(gameObject);
      }

      public int Damage()
      {
         return 5;
      }
   }
}
