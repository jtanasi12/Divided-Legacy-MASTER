using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : CharacterProjectiles{
    protected override void Start(){
        base.Start(); // Call the Start method of the parent class first
    }

    // Update is called once per frame
    protected override float setSpeed(){
        // Set the speed for the arrow
        return speed;
    }

    // Called when the fireball collides with another collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Arrow" tag
        if (collision.gameObject.layer != LayerMask.NameToLayer("Fireball"))
            {
            Destroy(gameObject);
            // If collided with an object tagged as "Arrow", do nothin
        }


        // Destroy the fireball when it collides with any object
       // Destroy(gameObject);
        
    }
}
