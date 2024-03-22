using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private int damage = 1; 

    [SerializeField]
    private MainAnimationController enemyAnimation;

    [SerializeField]
    private PlayerController playerMovement;


    [SerializeField]
    private EnemyHealth enemyHealth;

    private bool canDamage = true;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (canDamage && player.CompareTag("Player") && !enemyHealth.GetIsPlayerDead())
        {

            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(DamageCooldown());
            
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f); 
        canDamage = true;
    }


}
