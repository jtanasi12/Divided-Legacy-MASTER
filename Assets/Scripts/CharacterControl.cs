using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    // Variables 
    private float horizontalInput;
   
    private bool isFacingRight = true;
    private bool doubleJump;

    // After the player leaves the ground, it gives us 0.2 seconds to still make a jump 
    private float coyoteTime = 0.2f;

    // A buffer that allows us to jump .2 seconds before we land
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    // A short window of time allowing the player to jump again after leaving the ground
    private float coyoteTimeCounter;

   [SerializeField]
    private Rigidbody2D body;
   [SerializeField]
   private Transform groundCheck;
   [SerializeField]
   private LayerMask groundLayer;
   [SerializeField]
    private float speed; 

   [SerializeField]
    private float jumpingPower;


    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // If we are on the ground and the jump button isn't pressed
        if(IsGrounded() && !Input.GetButton("Jump")){
            doubleJump = false;
        }
        // Left: -1 / No Movement: 0 / Right: 1

         // If we are grounded we can jump, or if 
        if(IsGrounded()){
            coyoteTimeCounter = coyoteTime;
            // If we are grounded we set a 0.2 second timer
            
        }
        else{
            coyoteTimeCounter -= Time.deltaTime;
            // As soon as the player walks off a platform and there is no collision detection on the ground we start decrementing the timer
        }

        // If we have pressed jump start the timer
     
        if(Input.GetButtonDown("Jump")){

            if(IsGrounded() || doubleJump){

            // Set to 0.02 seconds everytime we jump 

                body.velocity = new Vector2(body.velocity.x, jumpingPower);

            // Set to the opposite  
            doubleJump = !doubleJump;
            }

            jumpBufferCounter = jumpBufferTime;

            
        }
        else{
            jumpBufferCounter -= Time.deltaTime;
            // In the air, the timer will decrement
        }

        // Check if we are pressing to jump
        // and the player has to be on the ground
        // check if the timer is active allowing for more jumps
        if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f){

            // Velocity is delta magnitude (speed) and direction
            body.velocity = new Vector2(body.velocity.x, jumpingPower);

            jumpBufferCounter = 0f; // RESET
        }

        // Allow the player to increase faster if they hold the jump button down, and jump less if they only tap it
        if(Input.GetButtonUp("Jump") && body.velocity.y > 0f){
           
            coyoteTimeCounter = 0f;
            // Reset the coyote jump only when the player has released from jump to prevent spamming
        }

         Flip(); // Check if we need to fip the character

    }

    // Used for physics, uses a fix step of 0.002 seconds
    // Time.FixedDeltaTime is implied = 0.002
    private void FixedUpdate(){
        body.velocity = new Vector2( (horizontalInput * speed), body.velocity.y);
    }

    private void Flip(){
        // If we are facing right and the user hits left
        // Or if we are facing left and input is 1 we need to flip
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f ){
            isFacingRight = !isFacingRight; // Opposite value 

            // Retrieve the dimensions of the game objects coordinates
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            // Flip the coordinates
        }
    }
    private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Physics2D.OverlapCircle checks for overlapping colliders within a circular area 
        // Get the position of our 'groundCheck' which is positioned at our players feet. The players feet will be the center inside of the circular area we are creating. The radius of the circle is 0.2 units. And we are checking if a groundLayer overlaps with the collider we created. If we collide with the ground we return true that we are on the ground if not false and we are in the air
    }
}
