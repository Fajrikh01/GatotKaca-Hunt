using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int maxHealt = 100;
    int currentHealt;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * transform.localScale.x, enemyLayers); 

        // Damage then
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange * transform.localScale.x);
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;

        animator.SetTrigger("hurt");

        if (currentHealt <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");

        animator.SetTrigger("die");

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
