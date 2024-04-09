using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    // Serialized Fields 
    [SerializeField]
    protected float maxSpeed;
    [SerializeField]
    protected float accelerationRate;
    [SerializeField]
    protected float decelerationRate;

    [SerializeField]
    protected Rigidbody2D body;
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected LayerMask groundLayer;
    [SerializeField]
    protected float speed;
    protected float baseSpeed;
    protected bool isFacingRight = true;
    protected bool isGrounded = true;
    protected bool isTurning = false;

    protected virtual void Awake(){
        baseSpeed = speed; // Get the current speed before we move
    }
    public bool GetIsTurning() { return isTurning; }
    public bool GetIsFacingRight(){ return isFacingRight; }

    protected void FlipCharacter(){
        isFacingRight = !isFacingRight; // Opposite value 

        // Retrieve the dimensions of the game objects coordinates
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;


    }

    protected bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        // Returns true if there is collision on the ground and false means that we are in the air

        // Physics2D.OverlapCircle checks for overlapping colliders within a circular area 
        // Get the position of our 'groundCheck' which is positioned at our players feet. The players feet will be the center inside of the circular area we are creating. The radius of the circle is 0.2 units. And we are checking if a groundLayer overlaps with the collider we created. If we collide with the ground we return true that we are on the ground if not false and we are in the air
    }
}
