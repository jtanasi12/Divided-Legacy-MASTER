using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Door : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject respawnLocation;

    [SerializeField]
    private GameObject respawnLocation2;

    [SerializeField]
    private Characters player;

    [SerializeField]
    private Sprite openedDoor;

    [SerializeField]
    private Sprite closedDoor;

    private bool isOpened = false;

    private bool hasRespawned = false;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();


        SetState(isOpened);
        // By default the door will be closed 
    }


    public void ToggleDoorState()
    {
        isOpened = !isOpened; // Flip it to the opposite
        SetState(isOpened);
    }

    public void ToggleDoorState(bool state)
    {
        isOpened = state;
        SetState(isOpened);
    }

    private void SetState(bool isOpened)
    {
        // Check if the spriteRenderer is not null and the object is not destroyed
        if (spriteRenderer != null)
        {
            StartCoroutine(SetStateWithDelay(isOpened, 0.25f)); 
        }
        else
        {
            // Optionally, you can log a message indicating that the SpriteRenderer is null
            Debug.LogWarning("SpriteRenderer is null. Unable to set door state.");
        }

    }

    private IEnumerator SetStateWithDelay(bool isOpened, float delayTime)
    {
        // Check if the spriteRenderer is not null and the object is not destroyed
        if (spriteRenderer != null)
        {
            // Optionally, you can add some animation or effect here before changing the sprite

            // Wait for one second
            yield return new WaitForSeconds(delayTime);

            // Set the sprite based on whether the door is open or closed
            spriteRenderer.sprite = isOpened ? openedDoor : closedDoor;
        }
        else
        {
            // Optionally, you can log a message indicating that the SpriteRenderer is null
            Debug.LogWarning("SpriteRenderer is null. Unable to set door state.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Only apply this to doors that are open
        // prevent the player from respawning over and over 
        if (isOpened && !hasRespawned)
        {
            Debug.Log("Respawn player");

            if (collision.gameObject.layer == LayerMask.NameToLayer("DoorCollider"))
            {
                Debug.Log("Respawn player");
                // Spawn the player to Door B, we treat the script as if it was Door A 

                // Move the player
                player.transform.position = respawnLocation2.transform.position;


                StartCoroutine(SetStateWithDelay(false, 0.5f));

                // Ensures we only spawn one time
                hasRespawned = true;
            }
        }
    }
}
