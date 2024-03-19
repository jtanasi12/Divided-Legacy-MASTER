using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : BasicController
{
    #region Variables
    [SerializeField]
    PlayableCharacters mainPlayer;

    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private int patrolDestination;

    [SerializeField]
    private MainAnimationController enemyAnimation;

    [SerializeField]
    private LayerMask player;

    [SerializeField]
    private Transform enemyHead;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private float rayCastDistance;

    [SerializeField]
    private float chaseSpeed;

    private float normalSpeed;

    [SerializeField]
    private bool isChasing;
    [SerializeField]
    private float chaseDistance;

    [SerializeField]
    private GameObject alert;

    [SerializeField]
    private EnemyHealth enemyHealth;

    [SerializeField]
    private float detectionPauseTime;

    private bool playerDetected;

    [SerializeField]
    private float stunDuration = 1f;

    protected bool isStunned;



    [SerializeField]
    private PlayerHealth playerHealth;


    #endregion

    public bool GetIsStunned()
    {
        return isStunned;
    }

    private void Start()
    {
        // A place holder for our regular speed 
        normalSpeed = speed;

        isStunned = false;
    }
    // Update is called once per frame
    public void InputMechanics()
    {
    
        if (!enemyHealth.GetIsPlayerDead())
        {
            if (!isStunned){
                EnemyIsAlive();

            }

        }
        else {

            EnemyIsDead();
        }

    }


    private void EnemyIsAlive() {

      

        // If we land on the enemys head we have the enemy not move
        // Then break out the method so no enemy rotation is applied 
        if (IsPlayerHeadCollision())
        {

            enemyAnimation.SetIdleState();
            return;
        }
            EnemyMovement();


    }

    private void EnemyPatrol()
    {

        if (!isStunned)
        {

            alert.SetActive(false);
            speed = normalSpeed;
            // RESET

            enemyAnimation.WalkAnimation();

            // Represents the first point in the array of destinations 
            if (patrolDestination == 0)
            {

                // The enemy will move toward the first destination position 
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);

                // If the distance between the enemy and the destination is less than .2 we need to flip the character 
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                {

                    patrolDestination = 1;

                    FlipCharacter();
                }

            }
            if (patrolDestination == 1)
            {
                // The enemy will move toward the second (point B) destination position 
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);

                // If the distance between the enemy and the destination is less than .2 we need to flip the character 
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
                {

                    patrolDestination = 0;


                    FlipCharacter();
                }

            }
        }
        

    }


    void ChasePlayer()
    {
        speed = chaseSpeed;

        // If we start chasing the player and he goes out of bounds go back to patrolling
        // If the player is dead, stop chasing
        // If the player is in the switched state, stop chasing
        if (PlayerOutOfBounds() || playerHealth.GetIsPlayerDead() == true || playerHealth.GetSwitchedState() ) {

            EnemyPatrol();

        }

        // Enemy is in bounds 
        else
        {

            alert.SetActive(true);
            enemyAnimation.SetRunState(); // Have the enemy enter running state 

            // If the player is on the enemies left side, monster is on our right 
            if (transform.position.x > playerTransform.position.x)
            {

                isFacingRight = false;
                transform.position += Vector3.left * speed * Time.deltaTime;
                patrolDestination = 0;
                transform.localScale = new Vector3((float)-0.5, (float)0.5, 1);


            }
            // The player is on the right side of the enemy and enemy is on left
            else if (transform.position.x < playerTransform.position.x)
            {
                isFacingRight = true;
                patrolDestination = 1;
                transform.localScale = new Vector3((float)0.5, (float)0.5, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;


            }

            // If chasing is true, but the player runs out of the distance
            // Then we need to set isChasing to false and go back to Patrol Mode 
            if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
            {
                isChasing = false;


            }
        }
    }


    bool PlayerOutOfBounds()
    {
        // Player is too far to the left to chase 
        if (playerTransform.position.x < patrolPoints[0].position.x)
        {

            return true;
        }

        // Player is too far to the right to chase 
        else if (playerTransform.position.x > patrolPoints[1].position.x)
        {
            return true;
        }

        return false;
    }


    private bool IsPlayerHeadCollision() {

        return Physics2D.OverlapCircle(enemyHead.position, 0.2f, player);

    }


    public void EnemyIsDead() {

        isChasing = false;

        alert.SetActive(false);

        body.constraints = RigidbodyConstraints2D.FreezeAll;

        gameObject.layer = LayerMask.NameToLayer("DeadEnemy");

        StartCoroutine(DestroyAfterDelay(3f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void EnemyMovement()
    {

        if (!isStunned){

            if (isChasing)
            {

                ChasePlayer();
            }

            else
            // If the enemy is not chasing, the enemy will be patrolling
            {

                // If the enemys position and the players position is less than the chaseDistance
                // We need to start chasing
                if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
                {
                    isChasing = true;

                }
                else
                {
                    EnemyPatrol();

                }

            }
        }
    }

    // Method to stun the enemy for a short peroid of time/ pause movement
    public void StunEnemy()
    {
        if (!isStunned)
        {
            isStunned = true;
            enemyAnimation.SetIdleState();
            enemyAnimation.FindSpriteItemEyes("Common.Emoji.Eyes.Dead");

            StartCoroutine(RecoverFromStun());

        }
    }

    private IEnumerator RecoverFromStun()
    {

        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        enemyAnimation.FindSpriteItemEyes("Common.Undead.Eyes.ZombieEyes7");

    }
}
