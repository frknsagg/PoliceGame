
using UnityEngine;

public class BulletController : MonoBehaviour
{
   private void Start()
   {
      Invoke(nameof(DestroyGameObject),5);
   }

   void DestroyGameObject()
   {
      Destroy(gameObject);
   }
}
