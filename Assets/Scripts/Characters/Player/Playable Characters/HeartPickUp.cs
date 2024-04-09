using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : Pickups
{
    [SerializeField]
    private PlayerHealth playerHealth;


    public bool GetSwitchedState()
    {
        return switchedState;
    }

    public void SetSwitchedState(bool state)
    {
        switchedState = state;
    }

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
    private void Update()
    {
        // Don't move the heart if we in a switched state 
        if (!switchedState)
        {
            // Move the heart towards point B if movingToB is true, otherwise move towards point A
            if (movingToB)
                MoveTowards(pointB.position);
            else
                MoveTowards(pointA.position);
        }
    }

  

}



