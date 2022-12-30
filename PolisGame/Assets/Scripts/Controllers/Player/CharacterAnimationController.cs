using UnityEngine;

namespace Controllers.Player
{
   public class CharacterAnimationController : MonoBehaviour
   {
      [SerializeField] private Animator animator;
      private static readonly int Run = Animator.StringToHash("Run");
      private static readonly int Idle = Animator.StringToHash("Idle");
      private static readonly int Fire = Animator.StringToHash("Fire");
      private static readonly int Death = Animator.StringToHash("Death");

      public void RunAnimation()
      {
         animator.SetBool(Run,true);
         animator.SetBool(Idle,false);
         animator.SetBool(Fire,false);
      
      }
      public void IdleAnimation()
      {
         animator.SetBool(Run,false);
         animator.SetBool(Idle,true);
         animator.SetBool(Fire,false);
      }
      public void FireAnimation()
      {
         animator.SetBool(Fire,true);
         animator.SetBool(Run,false);
         animator.SetBool(Idle,false);
      
      }

      public void DeathAnimation()
      {
         animator.SetTrigger(Death);
      }
  
   
   }
}
