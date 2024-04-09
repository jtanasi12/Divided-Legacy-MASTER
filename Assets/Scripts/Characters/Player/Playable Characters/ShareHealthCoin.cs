using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareHealthCoin : Pickups
{
    [SerializeField]
    private SendHearts sendHearts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heart Collider"))
        {
            Debug.Log("FOUND A Share Heart Coin");

            sendHearts.Reset();
            isMoving = false;

            Destroy(gameObject); // Destroy the Coin once collected 
        }
    }

}
