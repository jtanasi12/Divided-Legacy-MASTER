using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour

{
    public Transform arrowSpawnPoint; // Reference to the point where the arrow should spawn
    public Vector3 spawnOffset = new Vector3(1f, 1.4f, 0.0f); // this offset makes it so that the arrow spawns in front of Cloud Boy at the Bow's height-ish
    //the x is flipped to a negative in the shoot method if CloudBoy is facing left
    public GameObject arrowPrefab;  //projectile being spawned

    public float coolDown = 0.5f; //amount of time between arrow shots to prevent spam
    private float timer;    //keeps track of time between arrow shots

    // Start is called before the first frame update
    void Start(){
        timer = 0f;   //initialize timer
    }

    // Update is called once per frame
    void Update(){
        timer -= Time.deltaTime;    //decrement timer
    }

//shoot arrow is called by CloudBoyController
    public void shootArrow(bool isFacingTheRight){
        if(timer <= 0 ){
            if(!isFacingTheRight){
                //change arrow velocity to be negative and change offset to be negative
                spawnOffset.x = -1f; // Update X offset for left direction
            } else{
                spawnOffset.x = 1f; // Update X offset for right direction
            }
            //THIS is the code for getting the bow to spawn the arrow
            Vector3 spawnPosition = arrowSpawnPoint.position + spawnOffset;

            //create the arrow and hold onto that arrow as a gameobject
            GameObject newArrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);
            Arrow arrow = newArrow.GetComponent<Arrow>();

            // If the Arrow component is found, set its velocity based on the facing direction
            if (arrow != null){
                Vector2 direction = isFacingTheRight ? Vector2.right : Vector2.left;
                arrow.SetVelocity(direction); // pass direction so that the arrow gets flipped if necessary
            }
            else{ Debug.LogWarning("Arrow component not found on the instantiated arrow object.");}
            timer = coolDown;
        }
    }
}
