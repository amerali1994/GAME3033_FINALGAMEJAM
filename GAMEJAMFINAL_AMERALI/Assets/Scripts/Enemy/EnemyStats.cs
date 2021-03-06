using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyStats : CharacterStats
{ 

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TadeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        animator.Play("AttackedEnemy");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Dying");
            StartCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene(4);
    }
}
