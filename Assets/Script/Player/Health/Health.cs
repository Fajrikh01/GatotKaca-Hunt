using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    public Animator anim;
    private bool dead;

    //public GameObject respawnPrefab;
    //public GameObject respawn;

        /*
    private void Start()
    {
        if (respawn == null)
            respawn = GameObject.FindWithTag("Heart");

        Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
    }
    */

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            SoundManager.instance.Play("Hurt");
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                PlayerManager.isGameOver = true;
                Destroy(gameObject, 1f);
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Trap")
        {
            Debug.Log("Damage");
            TakeDamage(3.34f);
        }
    }
}
