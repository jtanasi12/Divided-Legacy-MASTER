using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : Pickups
{
    [SerializeField]
    AudioSource heartFX;

    private int counter = 0;

    private Renderer render; 

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heart Collider"))
        {
            // If the player has max health, we do not want to pick up the health 
            if (playerHealth.GetHealth() != playerHealth.GetMaxHealth())
            {


                if( counter == 0)
                {
                    Debug.Log("FOUND A HEART");

                    isMoving = false;

                    playerHealth.IncreaseHealth();

                    ++counter; // Make sure we only increase the health 1 time if we collide with the heart multiple times while we are waiting for it to destroy after the sound plays 

                    heartFX.Play();

                    render.enabled = false; // Hide the object

                    StartCoroutine(DestroyAfterSound());

                }

            }
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(heartFX.clip.length);

        Destroy(gameObject); // Destroy the Coin once collected 

    }
}




