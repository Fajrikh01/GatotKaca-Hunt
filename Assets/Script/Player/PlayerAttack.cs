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
    //public int bossDamage = 20;

    public float attackRate = 2f;
    //float nextAttackTime = 0f;

    public int maxHealt = 100;
    int currentHealt;

    /*
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
    */

    public void Attack()
    {
        // Play an attack animation
        SoundManager.instance.Play("Attack");
        animator.SetTrigger("attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * transform.localScale.x, enemyLayers); 

        // Damage then
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    /*
    void bossAttack()
    {
        // Play an attack animation
        animator.SetTrigger("attack");

        // Detect enemies in range of attack
        Collider2D[] hitBoss = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * transform.localScale.x, bossLayers);

        // Damage then
        foreach (Collider2D boss in hitBoss)
        {
            boss.GetComponent<Demon>().TakeDamage(bossDamage);
        }
    }
    */

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange * transform.localScale.x);
    }
}
