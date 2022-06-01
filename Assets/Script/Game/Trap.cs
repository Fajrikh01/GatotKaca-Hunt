using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Trap")
        {
            Debug.Log("Damage");
            //collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
