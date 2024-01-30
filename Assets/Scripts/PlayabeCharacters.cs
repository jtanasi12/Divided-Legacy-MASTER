using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class of Split & Cloud Boy
public class PlayableCharacters : Characters
{
   
   [SerializeField]
   protected Rigidbody2D body;
   [SerializeField]
   protected Transform groundCheck;
   [SerializeField]
   protected LayerMask groundLayer;
   [SerializeField]
   protected float speed; 

   [SerializeField]
    protected float jumpingPower;


      // Variables 
   protected float horizontalInput;
   
   protected bool isFacingRight = true;
   protected bool doubleJump;

    // After the player leaves the ground, it gives us 0.2 seconds to still make a jump 
   protected float coyoteTime = 0.2f;

    // A buffer that allows us to jump .2 seconds before we land
   protected float jumpBufferTime = 0.2f;
   protected float jumpBufferCounter;

    // A short window of time allowing the player to jump again after walking off a plateform
   protected float coyoteTimeCounter;

    // Update is called once per frame
    protected virtual void Update(){

         horizontalInput = Input.GetAxisRaw("Horizontal");
        // -1 = LEFT, 0 = NO MOVEMENT, 1 = RIGHT

    
        if(IsGrounded() && !Input.GetButton("Jump")){
            doubleJump = false;
            // When the player returns to the ground, we must set doubleJump back to false
        }

         // If we are grounded, set the coyote TIMER 
        if(IsGrounded()){
            coyoteTimeCounter = coyoteTime;
            // If we are grounded we set a 0.2 second timer
            
        }
        else{
            coyoteTimeCounter -= Time.deltaTime;
            // As soon as the player walks off a platform and there is no collision detection on the ground we start decrementing the timer
            // Giving the player .2 seconds to still make a last second jump
        }
          
        if(Input.GetButtonDown("Jump")){

            jumpBufferCounter = jumpBufferTime;
            // As soon as we jump, we have .2 seconds to jump again 
            // Creating this effect where we can jump again before hitting the ground
            
        }
        else{
            jumpBufferCounter -= Time.deltaTime;
            // In the air, the timer will decrement
        }

        // When we jump the jumpBuffer goes to 0.2 and starts decrementing and coyoteTime decrements when are in the air 
        // If we are in the air AND jump was clicked we open the window for another jump 
        // This prevents us from falling off a platform and allowing us to jump before hitting the ground 
        // This should only be triggered after  
        if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f){

            // Velocity is delta magnitude (speed) and direction
            body.velocity = new Vector2(body.velocity.x, jumpingPower);

            jumpBufferCounter = 0f; // RESET
        }

        if(Input.GetButtonUp("Jump") && body.velocity.y > 0f){
         // Reduce the upward velocity in half when the jump button is released 
         // So if we tap the jump button, your velocity will be immediently cut in half
         // if you hold the jump button longer you will be allowed to go higher
          body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
          // Multiplication by a decimal makes the upward velocity smaller

          coyoteTimeCounter = 0f;
            // As soon as we jump we must reset the timer, reduce spamming
        }

         Flip(); // Check if we need to fip the character
    }

      // Used for physics, uses a fix step of 0.002 seconds
    // Time.FixedDeltaTime is implied = 0.002
    protected virtual void FixedUpdate(){
        body.velocity = new Vector2( (horizontalInput * speed), body.velocity.y);
    }

    protected void Flip(){
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
    protected bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        // Returns true if there is collision on the ground and false means that we are in the air

        // Physics2D.OverlapCircle checks for overlapping colliders within a circular area 
        // Get the position of our 'groundCheck' which is positioned at our players feet. The players feet will be the center inside of the circular area we are creating. The radius of the circle is 0.2 units. And we are checking if a groundLayer overlaps with the collider we created. If we collide with the ground we return true that we are on the ground if not false and we are in the air
    }

}
