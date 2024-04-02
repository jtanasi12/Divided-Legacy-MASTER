using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour {
    private Vector3 respawnPosition = new Vector3(30f, -33f, 0f); //this is where the player goes if colliding with the kill floor

    private void OnTriggerEnter2D(Collider2D other) {
        // Check if the colliding object is the player character
        if (other.CompareTag("Player")) {
            // Trigger character death or respawn logic
            // For example:
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null){
                Debug.Log("Player has collided with killfloor");
                // Respawn the player at the specified respawn point, currently acts more like a teleport until the decrement health call is made
                RespawnPlayer(other.gameObject);
                //TODO add a decrementHealth() call here
            }
        }
    }
    private void RespawnPlayer(GameObject player){
        // Set the player's position to the respawn point
        player.transform.position = respawnPosition;
    }
}
