using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    [SerializeField]
    private GameObject exclamationMark;

    [SerializeField]
    private Door DoorA;

    [SerializeField]
    private Door DoorB;

    private bool exclamationState = true;


    private void Start()
    {
        DoorB.ToggleDoorState(); // Switch door B to be opened 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is standing on the pressure plate 
        if( collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player is standing on the pressure plate");

            exclamationMark.SetActive(false);


            DoorA.ToggleDoorState(true);
            // Toggle Door A which is initally a closed door to become an open door

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        exclamationMark.SetActive(true);

        DoorA.ToggleDoorState(false);
        // Close the door again when the pressure plate is off

    }



}
