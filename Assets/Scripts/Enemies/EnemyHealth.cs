using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    private void Start()
    {
        currentHealth = maxHealth;
    }


    public override void TakeDamage(int damageAmount)
    {
        bool isStunned = character.GetComponent<EnemyController>().GetIsStunned();


        currentHealth -= damageAmount;

        // If the player is stunned they canno't take more damage
        if (!isStunned)
        {
            if (currentHealth <= 0)
            {
                // Player Dies
                playerAnimation.DeathAState();
                isDead = true;
            }
            else
            {
                // Attack Animation
                Debug.Log("Player takes damage");

                if (!isFlickering)
                {
                    StartCoroutine(DamageFlicker());

                }

            }
        }
    }
}
