using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
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
   [SerializeField]
   protected Transform wallCheck;
   [SerializeField]
   protected LayerMask wallLayer;

      #region Controller Support
    private IA_Controller gamepad; // Reference to the IA_Controller (mappings for input to controller)
    private Vector2 gpMove;
    private Vector2 gpPan;

    #endregion

    #region GameState
    [SerializeField]
    private SharedState gameState;
    #endregion
    // Variables 
    protected float horizontalInput;


    // Variables 

   protected bool isFacingRight = true;
   protected bool doubleJump;
    private float wallJumpingDirection;

   protected bool isWallSliding;
   protected bool isWallJumping;
   protected float wallSlidingSpeed = 2f;
   private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
   private float wallJumpingDuration = 0.4f;  // This variable will set the wallJumpingCounter
   private Vector2 wallJumpingPower = new Vector2(6f, 12f);

    // After the player leaves the ground, it gives us 0.2 seconds to still make a jump 
    protected float coyoteTime = 0.2f;

    // A buffer that allows us to jump .2 seconds before we land
    protected float jumpBufferTime = 0.2f;
    protected float jumpBufferCounter;

    // A short window of time allowing the player to jump again after walking off a plateform
    protected float coyoteTimeCounter;

    void Awake()
    {
        // Register the gamepad (Xbox, PlayStation, etc...)
        gamepad = new IA_Controller();
        gamepad.Gameplay.Jump.performed += ctx => Jump(); // Register Jump action to a function
        gamepad.Gameplay.Skill.performed += ctx => ExecuteSkill(); // Register skill to a funtion
        gamepad.Gameplay.SwapActiveCharacter.performed += ctx => SwapCharacter(); // Register character swap to a funtion
    }

    void OnEnable()
    {
        gamepad.Gameplay.Enable();
    }

    void OnDisable()
    {
        gamepad.Gameplay.Disable();
    }
    
    // FIXME: This function should really be cleand up.
    // Cont.: We should have "Update()" simply calling other functions for clarity of responsiblity
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!gameState.isPaused){
            horizontalInput = Input.GetAxisRaw("Horizontal");
            // -1 = LEFT, 0 = NO MOVEMENT, 1 = RIGHT


            if (IsGrounded() && !Input.GetButton("Jump"))
            {
                doubleJump = false;
                // When the player returns to the ground, we must set doubleJump back to false
            }

            // If we are grounded, set the coyote TIMER 
            if (IsGrounded())
            {
                coyoteTimeCounter = coyoteTime;
                // If we are grounded we set a 0.2 second timer
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
                // As soon as the player walks off a platform and there is no collision detection on the ground we start decrementing the timer
                // Giving the player .2 seconds to still make a last second jump
            }

            if (Input.GetButtonDown("Jump"))
                Jump();
            else
                jumpBufferCounter -= Time.deltaTime;

            if (Input.GetButtonUp("Jump") && body.velocity.y > 0f)
            {
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
    }

    void ExecuteSkill() {
        print("this is where we would do some action stuff wooo");
    }

    void Jump()
    {
        jumpBufferCounter = jumpBufferTime;

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {

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

        WallSlide(); // Allows player to slide down the wall

        WallJump(); // Allow player to wall jump

        // Don't allow player to move and flip during the state of wallJumping
        if (!isWallJumping)
        {
            Flip(); // Check if we need to fip the character
        }
       
    }

     // Used for physics, uses a fix step of 0.002 seconds
    // Time.FixedDeltaTime is implied = 0.002
    protected virtual void FixedUpdate(){

        if (!isWallJumping)
        {
            body.velocity = new Vector2((horizontalInput * speed), body.velocity.y);
        }

         
    }

    void SwapCharacter()
    {
        print("Character Swap Logic");
    }


    protected void Flip()
    {
        // If we are facing right and the user hits left
        // Or if we are facing left and input is 1 we need to flip
        if (!isWallJumping && (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight; // Opposite value 

            // Retrieve the dimensions of the game objects coordinates
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            // Flip the coordinates
        }
    }
    protected bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        // Returns true if there is collision on the ground and false means that we are in the air

        // Physics2D.OverlapCircle checks for overlapping colliders within a circular area 
        // Get the position of our 'groundCheck' which is positioned at our players feet. The players feet will be the center inside of the circular area we are creating. The radius of the circle is 0.2 units. And we are checking if a groundLayer overlaps with the collider we created. If we collide with the ground we return true that we are on the ground if not false and we are in the air
    }

    private bool IsWalled() {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        // Creates an invisible circle around the position of the wall check with a radius of 0.2 and returns true if it makes collision with a wall layer 
    }

    private void WallSlide()
    {
        // If there is wall collision, the player is no grounded and they are actively pushing left or right 
        if(IsWalled() && !IsGrounded() && horizontalInput != 0f)
        {
            isWallSliding = true;
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));

            // Create a new velocity that ensures the players speed is clamped inside a range. The range should be negative wall sliding speed and canno't go faster. We allow float.MaxValue so theres no limit in upward range so the player can jump off the wall

            // LIMITS:
            //
            // UPWARD BOUND - A VERY LARGE NUMBER SO THE PLAYER CAN JUMP FREELY OFF THE WALLL

            // LOWER BOUND: NEGATIVE WALL SLIDING SPEED

        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x; // invert the characters position. -1 is left and 1 the character is facing the right

            wallJumpingCounter = wallJumpingTime;
            // Set to 0.4 seconds 

            CancelInvoke(nameof(StopWallJumping));

        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
            // If WallSliding was triggered and now the character is not wall jumping. We give them .4 seconds to still be able to make a jump off the wall. This is similiar to coyoteTime it makes the controls more forgiving 
        }



        // If the player jumps and wallSliding counter is > 0f either the player is wall sliding and it never lowered from 0.4 or the character let go of the wall slide and still above the .4 seconds and decides to jump
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f){

       

                {
                    isWallJumping = true;

                    body.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);

                    // Change the velocity the player will move horizontally off the wall AND vertically in the opposite direction they were facing hence the wallJumpingDirection is inversed

                    wallJumpingCounter = 0f; // Prevents spamming


                    // If the character is not facing the opposite direction which we set above we will need to flip their coordinates
                     if (transform.localScale.x != wallJumpingDirection)
                        {
                            isFacingRight = !isFacingRight;
                            Vector3 newLocalScale = transform.localScale;
                            newLocalScale.x *= -1f;


                            transform.localScale = newLocalScale;

                            // Changes the character to face the opposite direction
                        }

                    Invoke(nameof(StopWallJumping), wallJumpingDuration);

                    // Call StopWallJumping after the wallJumpingDuration is completed, prevent SPAMMING
                }
            

        }

    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

  
}
