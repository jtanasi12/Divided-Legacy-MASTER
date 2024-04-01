using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : CharacterProjectiles {
    protected override void Start(){
        base.Start(); // Call the Start method of the parent class first
    }

    // Update is called once per frame
    void Update(){}
    protected override float setSpeed(){
        // Set the speed for the enemy fireball
        return 6.5f;
    }
}
