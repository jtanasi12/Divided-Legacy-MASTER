using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareHeartPickups : Pickups
{
    [SerializeField]
    private SendHearts sendhearts;

    // Reset the hearts back to 0 so that it reactivates 
    private void Reset()
    {
        sendhearts.SetCurrent(0);
        sendhearts.ResetColor();
    }


      private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heart Collider"))
        {
            Debug.Log("FOUND A HEART");

            isMoving = false;

          Reset();

            Destroy(gameObject); // Destroy the coin once collected 
        }
    }

}
