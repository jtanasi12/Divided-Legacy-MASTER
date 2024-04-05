using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 10f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // A reference to the players health 
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // A reference to the players rigid body
            Rigidbody2D rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

            playerHealth.TakeDamage(1);

            if(rigidBody != null && playerHealth != null)
            {
              
                // Calculate the bounce direction based on the player's current velocity
                Vector2 currentDirection = rigidBody.velocity.normalized;

                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

            }

        }

    }
}
