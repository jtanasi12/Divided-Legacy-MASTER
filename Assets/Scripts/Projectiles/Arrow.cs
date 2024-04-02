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
        return 4.5f;
    }
}
