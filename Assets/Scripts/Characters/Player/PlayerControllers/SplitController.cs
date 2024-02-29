using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitController : PlayerController
{
    public void DoubleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // We want to execute jumpingPower if the player is on the ground OR in the air during a double jump if its' true
            if (IsGrounded() || doubleJump)
            {

                body.velocity = new Vector2(body.velocity.x, jumpingPower); // Increase jumping 

                // *** IMPORTANT ***
                // If we jump, we will then set double jumping to be true and the code will run again allowing us to jump again if the jump button is clicked
                // Once we jump again it will become false / Allows us not to spam the double jump 
                doubleJump = !doubleJump;
            }
        }
    }

}
