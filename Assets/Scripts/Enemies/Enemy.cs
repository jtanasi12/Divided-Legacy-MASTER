using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters{
    public Transform projectileSpawnPoint; // Reference to the point where the arrow should spawn
    public Vector3 spawnOffset = new Vector3(1f, 1.4f, 0.0f); // this offset makes it so that the arrow spawns in front of Cloud Boy at the Bow's height-ish
    //the x is flipped to a negative in the shoot method if CloudBoy is facing left

    public GameObject attackObjectPrefab;

    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private EnemyDamage enemyDamage;

    // Start is called before the first frame update
    void Awake()
    {
     //   SetCharacterName("Enemy");
    }

    // Update is called once per frame
    void Update(){
        enemyController.InputMechanics();
    }


    //called by EnemyController, which has the timer that calls this function
     public void shootProjectile(bool isFacingTheRight){
        if(!isFacingTheRight){
            //change arrow velocity to be negative and change offset to be negative
            spawnOffset.x = -1f; // Update X offset for left direction
        } else{
            spawnOffset.x = 1f; // Update X offset for right direction
        }
        //THIS is the code for getting the bow to spawn the arrow
        Vector3 spawnPosition = projectileSpawnPoint.position + spawnOffset;

        //create the arrow and hold onto that arrow as a gameobject
        GameObject newProjectile = Instantiate(attackObjectPrefab, transform.position, Quaternion.identity);
        EnemyFireball enemyProjectile = newProjectile.GetComponent<EnemyFireball>();
       
        // If the Arrow component is found, set its velocity based on the facing direction
        if (enemyProjectile != null){
            Vector2 direction = isFacingTheRight ? Vector2.right : Vector2.left;
            enemyProjectile.SetVelocity(direction); // pass direction so that the enemy projectile gets flipped if necessary
        }
        else{ Debug.LogWarning("enemy projectile component not found on the instantiated attack object.");}
     }

       void OnCollisionStay2D(Collision2D collision){
         enemyDamage.DealDamage(collision);
     }
}
