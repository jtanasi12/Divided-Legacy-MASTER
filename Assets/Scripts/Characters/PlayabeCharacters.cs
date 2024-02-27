using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HeroEditor.Common.Enums;

using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.Common;
using System;


// Parent class of Split & Cloud Boy
public class PlayableCharacters : Characters
{
    #region animatorRegion
    public Character character;




    #endregion

    #region basicMechanics
    private float horizontalInput;
    private bool isFacingRight = true;
    private bool isGrounded = true;
    private string characterName;

    [SerializeField]
    protected Rigidbody2D body;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    #endregion

    #region jumpAndWallMechanics

    protected bool doubleJump;
    private float wallJumpingDirection;
    private bool isWallSliding;
    private bool isWallJumping;
    private readonly float wallSlidingSpeed = 2f;
    private readonly float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private readonly float wallJumpingDuration = 0.4f;  // set wallJumpingCounter
    protected Vector2 wallJumpingPower = new(4f, 12f);
    private readonly float coyoteTime = 0.2f; // 0.2 seconds to make a jump after leaving ground
    private readonly float jumpBufferTime = 0.2f; // allows us to jump .2 seconds before we land
    private float jumpBufferCounter;
    protected float coyoteTimeCounter; // A short window after falling off plateform to jump

    // Serialized Fields
    [SerializeField]
    protected float jumpingPower;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private LayerMask wallLayer;
    #endregion

    #region speedMechanics

    private float baseSpeed;


    // Serialized Fields 
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float accelerationRate;
    [SerializeField]
    private float decelerationRate;
    [SerializeField]
    private float speed;

    #endregion

    #region Controller Support
    private IA_Controller gamepad; // Reference to the IA_Controller (mappings for input to controller)
    private Vector2 gpMove;
    private Vector2 gpPan;

    #endregion

    #region GameState
    [SerializeField]
    private SharedState gameState;
    #endregion

  
    void Awake()
    {


        baseSpeed = speed; // Get the current speed before we move

        // Register the gamepad (Xbox, PlayStation, etc...)
        gamepad = new IA_Controller();
        gamepad.Gameplay.Jump.performed += ctx => Jump(); // Register Jump action to a function
        gamepad.Gameplay.Skill.performed += ctx => ExecuteSkill(); // Register skill to a funtion
        gamepad.Gameplay.SwapActiveCharacter.performed += ctx => SwapCharacter(); // Register character swap to a funtion

        // Set Character to an idle state when we first load up 
        character.SetState(CharacterState.Idle);

    }

    void OnEnable()
    {
        gamepad.Gameplay.Enable();
    }

    void OnDisable()
    {
        gamepad.Gameplay.Disable();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!gameState.isPaused)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 = LEFT, 0 = NO MOVEMENT, 1 = RIGHT

            // Render walking or idle animations
            HandleAllAnimations(horizontalInput);

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


    // Use for Physics - Time.FixedDeltaTime is implied = 0.002
    protected virtual void FixedUpdate()
    {
        if (!isWallJumping)
        {
            speed = Mathf.MoveTowards(speed, maxSpeed, Time.fixedDeltaTime * accelerationRate);
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        }

    }

    void SpeedMechanics()
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

    void JumpMechanics()
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

    void ExecuteSkill()
    {
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

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    protected void SetCharacterName(string name)
    {
        characterName = name;
    }


    // Handle Idle animations 
    void HandleIdleAnimation(float horizontalInput)
    {

        // Only Idle if we are not moving 
        if (IsGrounded())
        {

            // Change Mouth Sprites 
            if (characterName == "Cloud Boy")
            {
                FindSpriteItem("Common.Bonus.Mouth.10"); 
            }
            else
            {
                FindSpriteItem("Common.Emoji.Mouth.Injured");

            }
        }

        // Set Animations 
        if (horizontalInput == 0)
        {
            character.SetState(CharacterState.Idle);
        }

        else
        {
            character.SetState(CharacterState.Walk);
        }
    }

    void HandleJumpAnimation()
    {

        if (!IsGrounded())
        {
            FindSpriteItem("Common.Bonus.Mouth.11");

        }

    }

    void HandleAllAnimations(float horizontalInput)
    {
        HandleIdleAnimation(horizontalInput);
        HandleJumpAnimation();
    }
    // Search for sprites - body parts we want to swap 

    void FindSpriteItem(string spriteId)
    {
        for (int index = 0; index < character.SpriteCollection.Mouth.Count; ++index)
        {
            if (character.SpriteCollection.Mouth[index].Id == spriteId)
            {
                var mouthIndex = character.SpriteCollection.Mouth[index];
                character.SetBody(mouthIndex, BodyPart.Mouth);

            }

        }
    }

}
