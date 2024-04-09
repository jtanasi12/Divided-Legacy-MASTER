using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{

    [SerializeField]
    protected float moveSpeed = 2f;

    protected bool switchedState = false;

    [SerializeField]
    protected Transform pointA;

    [SerializeField]
    protected Transform pointB;

    protected bool isMoving = true;
    protected bool movingToB = true; // Indicates whether the heart is currently moving towards point B

    protected void MoveTowards(Vector3 targetPosition)
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
