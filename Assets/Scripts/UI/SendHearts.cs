using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

using UnityEngine;

public class SendHearts : MonoBehaviour
{

    [SerializeField]
    private PlayerHealth mainPlayer;

    [SerializeField]
    private PlayerHealth recieverPlayer;

    private bool active = true;

    [SerializeField]
    private Image buttonImage;

    [SerializeField] private Button sendButton; // Reference to the button component

    private bool inputEnabled = true; // Indicates whether keyboard input is enabled


    public void Reset()
    {
        active = true;
        buttonImage.color = Color.white;

    }

    // Triggered when the button is clicked on
    public void SendHeart()
    {
        Debug.Log("Sending a heart to " + recieverPlayer.gameObject.GetComponent<Characters>().GetCharacterName());

        if (active)
        {
            // Make sure the player has more than 1 health before sending a heart 
            if (mainPlayer.GetHealth() > 1)
            {
                // The receiving player canno't have maximum health
                if (recieverPlayer.GetHealth() < recieverPlayer.GetMaxHealth())
                {
                    mainPlayer.DecrementHealth();
                    recieverPlayer.IncreaseHealth();
                    active = false;

                    buttonImage.color = Color.gray;
                    sendButton.interactable = false; // Disable button interaction
                    inputEnabled = false; // Disable keyboard input

                    // Re-enable keyboard input after a short delay
                    Invoke("EnableInput", 0.2f); // Adjust the delay as needed
                }

            }
        }
      

    }


}
