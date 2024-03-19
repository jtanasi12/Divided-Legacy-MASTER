using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private float damageInterval;

    private bool canDamage = true;

    [SerializeField]
    private int damage = 2;


    public void DealDamage(Collision2D collision)
    {
        // If the enemy is stunned, we do not deal damage
        if (enemyController.GetIsStunned() == false)
        {

            if (collision.gameObject.CompareTag("Player") && canDamage)
            {
                // Uses a timer to let the player become invulnerable for a few seconds
                StartCoroutine(ApplyDamage(collision));
            }
        }
    }

    // Apply Damage Timer
    IEnumerator ApplyDamage(Collision2D collision)
    {
        // Prevent further damage from happening until a certain time has passed
        canDamage = false;

        playerHealth.TakeDamage(damage);

        // Don't allow damage until after the interval has passed
        yield return new WaitForSeconds(damageInterval);

        canDamage = true;
    }
}
