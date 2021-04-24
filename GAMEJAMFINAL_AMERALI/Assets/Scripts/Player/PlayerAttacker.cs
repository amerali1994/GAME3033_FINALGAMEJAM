using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public PlayerAnimationManager animator;

    private void Awake()
    {
        animator = GetComponentInChildren<PlayerAnimationManager>();
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
