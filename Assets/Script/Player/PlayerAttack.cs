using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask bossLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int maxHealt = 100;
    int currentHealt;

    public void Attack()
    {
        // Play an attack animation
        if (Time.time >= nextAttackTime)
        {
            SoundManager.instance.Play("Attack");
            animator.SetTrigger("attack");
            nextAttackTime = Time.time + 1f / attackRate;
        }

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
}
