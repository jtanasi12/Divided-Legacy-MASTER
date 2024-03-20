using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private int damage = 1; // Adjust the damage value as needed

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
            float totalTime = player.GetComponent<PlayerController>().GetKnockBackTotalTime();
            player.GetComponent<PlayerController>().SetKnockBackCounter(totalTime);

            // Check if the player is on the leftside of the enemy
            // This means we are being hit from the rightside
            if(player.transform.position.x <= transform.position.x)
            {
                player.GetComponent<PlayerController>().SetKnockFromRight(true);
            }

            // We are on the rightside of the enemy, and being hit to the left
            else if(player.transform.position.x >= transform.position.x)
            {
                player.GetComponent<PlayerController>().SetKnockFromRight(false);
            }

            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(DamageCooldown());
            
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f); // Adjust cooldown duration as needed
        canDamage = true;
    }


}
