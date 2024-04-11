using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : Pickups
{ 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heart Collider"))
        {
            Debug.Log("FOUND A HEART");

            isMoving = false;

            playerHealth.IncreaseHealth();

            Destroy(gameObject); // Destroy the heart once collected 
        }
    }


}



