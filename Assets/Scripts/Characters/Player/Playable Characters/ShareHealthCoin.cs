using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareHealthCoin : Pickups
{
    private Renderer render;

    [SerializeField]
    private SendHearts sendHearts;

    [SerializeField]
    private AudioSource coinFX;

    private void Start()
    {
        render = GetComponent<Renderer>(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heart Collider"))
        {
            Debug.Log("FOUND A Share Heart Coin");

            sendHearts.Reset();
            isMoving = false;

            coinFX.Play(); // Play sound FX 

            render.enabled = false; // Hide the object 

            StartCoroutine(DestroyAfterSound()); // delete object from memory after the sound FX has finished playing 

        }
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(coinFX.clip.length);

        Destroy(gameObject); // Destroy the Coin once collected 

    }
}
