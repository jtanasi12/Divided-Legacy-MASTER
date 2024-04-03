using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProjectiles : MonoBehaviour
{
    public Rigidbody2D projectileRB;
    public float speed;
    public float range = 1;
    private float timer;

    protected virtual float setSpeed(){
        // This method should be implemented by child classes
        return 0f; // Default speed, child classes should override this method
    }

    // Start is called before the first frame update
    protected virtual void Start(){
        speed = setSpeed();
        timer = range;
    }

    // Update is called once per frame
    void Update(){
        timer -= Time.deltaTime;
        if (timer < 0){
            Destroy(gameObject);
        }
    }
    
    public virtual void SetVelocity(Vector2 direction){


        projectileRB.velocity = direction.normalized * speed;


        // Flip the arrow sprite if moving left
        if (direction.x < 0){
            FlipSprite(true);
        }
        else{
            // Reset sprite to original orientation if moving right
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    // Method to flip the arrow sprite horizontally
    public virtual void FlipSprite(bool isFlip){
        // Flip the arrow sprite horizontally
        transform.localScale = new Vector3(-1f, 1f, 1);
    }
}
