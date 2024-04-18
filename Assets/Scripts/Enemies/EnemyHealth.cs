using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField]
    AudioSource hurtSoundFX;

    [SerializeField]
    AudioSource deathSoundFX;

    private void Start()
    {
        currentHealth = maxHealth;
    }


    public override void TakeDamage(int damageAmount)
    {
        bool isStunned = character.GetComponent<EnemyController>().GetIsStunned();


        currentHealth -= damageAmount;

        // If the enemy is stunned they canno't take more damage
        if (!isStunned)
        {
            if (currentHealth <= 0 && !isDead)
            {
                deathSoundFX.Play();                 
                // Player Dies
                playerAnimation.DeathAState();
                isDead = true;
            }
            else
            {
                hurtSoundFX.Play();

                // Attack Animation
                Debug.Log("Enemy takes damage");

                if (!isFlickering)
                {
                    StartCoroutine(DamageFlicker());

                }

            }
        }
    }
}
