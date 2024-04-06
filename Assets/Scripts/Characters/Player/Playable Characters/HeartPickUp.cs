using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private Transform pointA;

    [SerializeField]
    private Transform pointB;

    private bool isMoving = true;
    private bool movingToB = true; // Indicates whether the heart is currently moving towards point B


    private bool switchedState = false;


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

    private void MoveTowards(Vector3 targetPosition)
    {
        // Calculate the distance and direction to the target position
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Move towards the target position
        transform.position += direction * moveSpeed * Time.deltaTime;

        // If the distance to the target is very small, switch the target position
        if (distance < 0.1f)
        {
            movingToB = !movingToB;
        }
    }

}



