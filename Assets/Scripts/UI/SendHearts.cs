using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendHearts : MonoBehaviour
{

    [SerializeField]
    private PlayerHealth mainPlayer;

    [SerializeField]
    private PlayerHealth recieverPlayer;

    // Triggered when the button is clicked on
    public void SendHeart()
    {
        Debug.Log("Sending a heart to " + recieverPlayer.gameObject.GetComponent<Characters>().GetCharacterName());

        // Make sure the player has more than 1 health before sending a heart 
        if (mainPlayer.GetHealth() > 1)
        {
            // The receiving player canno't have maximum health
            if(recieverPlayer.GetHealth() < recieverPlayer.GetMaxHealth())
            {
                mainPlayer.DecrementHealth();
                recieverPlayer.IncreaseHealth();

            }
        }
        
    }


}
