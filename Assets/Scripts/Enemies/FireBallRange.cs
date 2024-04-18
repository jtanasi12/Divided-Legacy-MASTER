using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallRange : MonoBehaviour
{

    private bool soundRange = false;
    // Start is called before the first frame update



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the entering collider is on the "Heart Collider" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heart Collider"))
        {
            soundRange = true;
            Debug.Log("IN RANGE");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the collider stays within the trigger area
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heart Collider"))
        {
            soundRange = true;
            Debug.Log("IN RANGE");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset the state when the collider exits the trigger area
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heart Collider"))
        {
            soundRange = false;
            Debug.Log("Out Of RANGE");
        }
    }
    public bool GetSoundRange()
    {
        return soundRange;
    }
}
