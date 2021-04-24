using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int Maxhealth;
    public int currentHealth;

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Maxhealth = SetMaxHealthFromHealthLevel();
        currentHealth = Maxhealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        Maxhealth = healthLevel * 10;
        return Maxhealth;
    }

    public void TadeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        animator.Play("AttackedEnemy");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("AttackedEnemy");
        }
    }
}
