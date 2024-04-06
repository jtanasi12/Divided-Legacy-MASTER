using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SplitController : PlayerController
{
    [SerializeField]
    SplitAnimations splitAnimator;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private Transform weaponTransform;

    [SerializeField]
    private float weaponRange;


    [SerializeField]
    private int weaponDamage;

    [SerializeField]
    private LayerMask enemyLayer;

   
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

    public override void AttackMechanics()
    {
        // Don't trigger attack if we are clicking on a UI Object
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            if (Input.GetMouseButtonDown(0)) // 0 for left mouse button, 1 for right mouse button, 2
            {
                StartCoroutine(MainAttack());


            }

            else if (Input.GetMouseButton(1))
            {
                splitAnimator.SetJab();

            }


        }

    }


    IEnumerator MainAttack() {

        splitAnimator.SetAttackState();

        Collider2D enemyCollision = Physics2D.OverlapCircle(weaponTransform.position, weaponRange, enemyLayer);



        // Check for collision from the sword, if it collides with an enemy
        yield return new WaitForSeconds(attackDelay);


        if(enemyCollision != null) {

            enemyCollision.GetComponent<EnemyHealth>().TakeDamage(weaponDamage);

            if(enemyCollision.GetComponent<EnemyHealth>().GetHealth() > 0)
            {
                // The enemy will be stunned and not able to move for a brief period of time 
                enemyCollision.GetComponent<EnemyController>().StunEnemy();
            }
          
        }

    }

}
