using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void DeathAnimation()
    {
        animator.SetBool("Death",true);
    }

    public void RunAnimation()
    {
        animator.SetBool("Run",true);
        animator.SetBool("Rob",false);
    }

    public void RobAnimation()
    {
        animator.SetBool("Rob",true);
        animator.SetBool("Run",false);
    }

    public void InjuredRunAnimation()
    {
        animator.SetBool("InjuredRun",true);
        animator.SetBool("Run",false);
        animator.SetBool("Rob",false);
    }
}
