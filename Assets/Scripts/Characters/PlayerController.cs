using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    #region BasicMovement
    [SerializeField]
    protected Rigidbody2D body;
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected LayerMask groundLayer;

    protected bool isFacingRight = true;
    protected bool isGrounded = true;

    [SerializeField]
    private PlayerAnimationController playerAnimation;

    [SerializeField]
    private Character character;

    #endregion

    #region jumpAndWallMechanics

    protected bool doubleJump;
    private string characterName;
    protected float wallJumpingDirection;
    protected bool isWallSliding;
    protected bool isWallJumping;
    protected readonly float wallSlidingSpeed = 2f;
    protected readonly float wallJumpingTime = 0.2f;
    protected float wallJumpingCounter;
    protected readonly float wallJumpingDuration = 0.4f;  // set wallJumpingCounter
    protected Vector2 wallJumpingPower = new(4f, 12f);
    protected readonly float coyoteTime = 0.2f; // 0.2 seconds to make a jump after leaving ground
    protected readonly float jumpBufferTime = 0.2f; // allows us to jump .2 seconds before we land
    protected float jumpBufferCounter;
    protected float coyoteTimeCounter; // A short window after falling off plateform to jump

    // Serialized Fields
    [SerializeField]
    protected float jumpingPower;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected LayerMask wallLayer;
    #endregion

    #region basicMechanics
    private float horizontalInput;

    #endregion

    #region speedMechanics

    protected float baseSpeed;


    // Serialized Fields 
    [SerializeField]
    protected float maxSpeed;
    [SerializeField]
    protected float accelerationRate;
    [SerializeField]
    protected float decelerationRate;
    [SerializeField]
    protected float speed;

    #endregion


    #region GetterMethods

    public bool GetDoubleJump() { return doubleJump; }
    public Rigidbody2D GetBody() { return body; }
    public float GetJumpingPower() { return jumpingPower; }
    public float GetHorizontalInput() { return horizontalInput;  }

    #endregion

    protected virtual void Awake()
    {
       
        baseSpeed = speed; // Get the current speed before we move
        playerAnimation.SetIdleState(); // Set Player in the idle state
    }

    public void InputMechanics()
    {

        // -1 = LEFT, 0 = NO MOVEMENT, 1 = RIGHT
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Don't allow player to move and flip during the state of wallJumping
        if (!isWallJumping)
        {

            WalkAnimation();

            SpeedMechanics();

            JumpMechanics();

            WallSlide(); // Allows player to slide down the wall

            WallJump(); // Allow player to wall jump

            // Don't allow player to move and flip during the state of wallJumping
            if (!isWallJumping)
            {
                Flip(); // Check if we need to fip the character
            }
        }
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


    protected void SpeedMechanics()
    {

        bool isRunButtonDown = Input.GetKey(KeyCode.X);

        bool isLeftOrRightPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);


        if (isRunButtonDown && isLeftOrRightPressed)
        {
            // If the player is holding down the X key and moving left or right, accelerate
            if (isLeftOrRightPressed)
            {
                speed = Mathf.MoveTowards(speed, maxSpeed, Time.deltaTime * accelerationRate);
            }
            // If the player is only holding down the X key, but not moving left or right, gradually increase speed until maxSpeed
            else
            {
                speed = Mathf.Min(speed + Time.deltaTime * accelerationRate, maxSpeed);
            }
        }
        else
        {
            // If the player is not holding down the X key and not moving left or right, reset speed to base speed
            if (!isLeftOrRightPressed)
            {
                speed = baseSpeed;
            }
            // If the player is not holding down the X key but moving left or right, decelerate
            else
            {
                speed = Mathf.MoveTowards(speed, 5f, Time.deltaTime * decelerationRate);
            }
        }
    }

    #region JumpRegion

    protected void JumpMechanics()
    {
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
    }


    public void Jump()
    {

        jumpBufferCounter = jumpBufferTime;

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {

            // Velocity is delta magnitude (speed) and direction
            body.velocity = new Vector2(body.velocity.x, jumpingPower);
            jumpBufferCounter = 0f; // RESET
        }

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



    }

#endregion

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        // Returns true if there is collision on the ground and false means that we are in the air

        // Physics2D.OverlapCircle checks for overlapping colliders within a circular area 
        // Get the position of our 'groundCheck' which is positioned at our players feet. The players feet will be the center inside of the circular area we are creating. The radius of the circle is 0.2 units. And we are checking if a groundLayer overlaps with the collider we created. If we collide with the ground we return true that we are on the ground if not false and we are in the air
    }

    #region WallRegion

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        // Creates an invisible circle around the position of the wall check with a radius of 0.2 and returns true if it makes collision with a wall layer 
    }

    private void WallSlide()
    {
        // If there is wall collision, the player is no grounded and they are actively pushing left or right 
        if (IsWalled() && !IsGrounded() && horizontalInput != 0f)
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

    private void StopWallJumping()
    {
        isWallJumping = false;
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
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {


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

    #endregion

    public void CalculatePhysics()
    {
        if (!isWallJumping)
        {
            speed = Mathf.MoveTowards(speed, maxSpeed, Time.fixedDeltaTime * accelerationRate);
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        }
    }
   
    private void WalkAnimation()
    {
        if(IsGrounded() && !isWallSliding && !isWallJumping)
        {
            playerAnimation.SetWalkAnimation(horizontalInput);
        }
    }
}
