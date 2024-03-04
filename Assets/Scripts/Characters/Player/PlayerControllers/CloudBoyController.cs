using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoyController : PlayerController
{
    private bool canDash = true;
    private bool isDashing;
    private readonly float dashingPower = 24f;
    private readonly float dashingTime = 0.2f;
    private readonly float dashingCooldown = 1f;


    // Serialized Fields
    [SerializeField]
    private TrailRenderer dashTrail;

    protected new void Awake()
    {
        base.Awake();
        dashTrail.emitting = false;

    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // Store originalGravity 
        float originalGravity = body.gravityScale;

        // Set tempoarily to 0, disable gravity while dashing
        body.gravityScale = 0f;

        // We use localScale because we take in the account of the characters direction he is facing 
        // localScale.x > 0 RIGHT && localScale.x < 0 LEFT
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f); // Increase velocity by dashpower on the x scale 

        // Draw the trail 
        dashTrail.emitting = true;

        // Dash for 0.2 seconds
        yield return new WaitForSeconds(dashingTime);

        //Set back to normal
        body.gravityScale = originalGravity;
        isDashing = false;

        // Prevents spamming the dash 
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        dashTrail.emitting = false;


    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    public void StartDashCoRoutine()
    {


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {

            // Start a co-routine for dashing
            StartCoroutine(Dash());
        }
    }
}