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
        if (!isInvulnerable && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            Rigidbody2D rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerHealth != null && rigidBody != null)
            {
                // Apply damage to the player
                playerHealth.TakeDamage(1);

                // Apply bounce force to the player
                rigidBody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

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
