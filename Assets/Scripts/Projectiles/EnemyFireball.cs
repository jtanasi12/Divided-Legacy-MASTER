using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball  : MonoBehaviour{
    public Rigidbody2D projectileRB;
    public float speed;
    public float range = 1;
    private float timer;
    private int damage = 1;
    private EnemyDamage enemyDamage;

    protected float setSpeed()
    {
        // This method should be implemented by child classes
        return 0f; // Default speed, child classes should override this method
    }

    // Start is called before the first frame update
    protected void Start()
    {

        speed = setSpeed();
        timer = range;

        // Initialize object through scripting because we are loading
        // the prefab dynamically
        enemyDamage = FindObjectOfType<EnemyDamage>();

     
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocity(Vector2 direction)
    {

        // Flip the enemy is moving left we need to flip the fireball 
        if (direction.x < 0)
        {
            FlipSprite(true);
        }
        else
        {
            FlipSprite(false);
        }

        // Move the projectile
        projectileRB.velocity = direction.normalized * speed;

    }

    // Method to flip the arrow sprite horizontally
    public void FlipSprite(bool isFlip)
    {
        if (isFlip)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
        else
        {
            // RESET transformation
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
    }

    // Called when the fireball collides with another collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "Arrow" tag
        if (collision.gameObject.layer != LayerMask.NameToLayer("Arrow"))
        {
            Destroy(gameObject);
            // If collided with an object tagged as "Arrow", do nothin

            Debug.Log("DESTORY");
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);

        }

    }
}

