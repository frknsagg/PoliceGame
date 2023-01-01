using UnityEngine;

namespace Controllers
{
    public class ThiefAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetAnim(EnemyAnimationsTypes animTypes)
        {
            animator.SetTrigger(animTypes.ToString());
        }

        public void ResetAnim(EnemyAnimationsTypes animationsTypes)
        {
            animator.ResetTrigger(animationsTypes.ToString());
        }
    }
}