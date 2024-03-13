using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerhealth;

    [SerializeField]
    private int damage = 2;


    public void DealDamage(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 

            playerhealth.TakeDamage(damage);
        }
    }
}
