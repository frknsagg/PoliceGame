using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
   [SerializeField] private Animator animator;

   public void RunAnimation()
   {
      animator.SetBool("Run",true);
      animator.SetBool("Idle",false);
      animator.SetBool("Fire",false);
      
   }
   public void IdleAnimation()
   {
      animator.SetBool("Run",false);
      animator.SetBool("Idle",true);
      animator.SetBool("Fire",false);
   }
   public void FireAnimation()
   {
      animator.SetBool("Fire",true);
      animator.SetBool("Run",false);
      animator.SetBool("Idle",false);
      
   }
  
   
}
