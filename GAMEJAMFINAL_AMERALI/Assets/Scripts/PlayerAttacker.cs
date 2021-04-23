using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public AnimationManager animator;

    private void Awake()
    {
        animator = GetComponentInChildren<AnimationManager>();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        animator.PlayTargetAnimation(weapon.Light_Attack, true);
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        animator.PlayTargetAnimation(weapon.Heavy_Attack, true);

    }
}
