using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public void PlayTargetAnimation(string targetAnim, bool IsAttacking)
    {
        animator.applyRootMotion = IsAttacking;
        animator.SetBool("IsAttacking", IsAttacking);
        animator.CrossFade(targetAnim, 0.2f);
    }
}
