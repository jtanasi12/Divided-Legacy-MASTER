using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    public Transform projectileSpawnPoint; // Reference to the point where the arrow should spawn
    public Vector3 spawnOffset = new Vector3(1f, 2f, 0.0f);

    public GameObject attackObjectPrefab;

    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private EnemyDamage enemyDamage;


    // Update is called once per frame
    void Update()
    {
        enemyController.InputMechanics();
    }


    //called by EnemyController, which has the timer that calls this function
    public void shootProjectile(bool isFacingTheRight)
    {
        if (!isFacingTheRight)
        {
            //change arrow velocity to be negative and change offset to be negative
            spawnOffset.x = -1f; // Update X offset for left direction
        }
        else
        {
            spawnOffset.x = 1f; // Update X offset for right direction
        }


        Vector3 spawnPosition = projectileSpawnPoint.position + spawnOffset;

        // (1.)
        // Instantiate the fireball and ensure that it has no rotation using Quaternion.identity
        // Load the fireball prefab from the position of our spawn point 
        GameObject newProjectile = Instantiate(attackObjectPrefab, projectileSpawnPoint.transform.position, Quaternion.identity);

        // (2.) 
        // New Projectile has a EnemyFireBall Script on it, so the new projectile that we create
        // we hold a reference to it in here. We can now access that script 
        EnemyFireball enemyProjectile = newProjectile.GetComponent<EnemyFireball>();

        // (3.)
        // If the fireball component is found,  set its velocity based on the facing direction
        if (enemyProjectile != null)
        {
            Vector2 direction;

            // If the enemy is facing right, set the direction to move right
            if (isFacingTheRight)
            {
                direction = Vector2.right;
            }
            // If the enemy is facing to the left, set the direction to move left
            else
            {
                direction = Vector2.left;
            }

            enemyProjectile.SetVelocity(direction); // pass direction so that the enemy projectile gets flipped if necessary
        }
        else
        {
            Debug.LogWarning("enemy projectile component not found on the instantiated attack object.");
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        enemyDamage.DealDamage(collision);
    }

}