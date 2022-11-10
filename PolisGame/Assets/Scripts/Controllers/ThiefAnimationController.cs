using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class ThiefAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void SetAnim(EnemyAnimationsTypes animTypes)
    {
        animator.SetTrigger(animTypes.ToString());
    }
}
