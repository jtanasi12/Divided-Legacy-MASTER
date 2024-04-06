using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 10f;

    private bool isInvulnerable = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is currently invulnerable
        if (!isInvulnerable)
        {

            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                // A reference to the players health 
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

                // A reference to the players rigid body
                Rigidbody2D rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

                playerHealth.TakeDamage(1);

                if (rigidBody != null && playerHealth != null)
                {

                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

                }

                // Start the invulnerability coroutine
                StartCoroutine(InvulnerabilityCoroutine());
            }
        }

    }
    private IEnumerator InvulnerabilityCoroutine()
    {
        // Set invulnerability flag to true
        isInvulnerable = true;

        // Wait for 2 seconds
        yield return new WaitForSeconds(.25f);

        // Set invulnerability flag to false after 2 seconds
        isInvulnerable = false;
    }

}
