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

    [SerializeField]
    private int LIMIT = 1;

    [SerializeField]
    private Image buttonImage;


    private int current = 0;

    // Triggered when the button is clicked on
    public void SendHeart()
    {
        Debug.Log("Sending a heart to " + recieverPlayer.gameObject.GetComponent<Characters>().GetCharacterName());

        // Allow this power to be used 3 times ONLY
        if (current < LIMIT)
        {
            // Make sure the player has more than 1 health before sending a heart 
            if (mainPlayer.GetHealth() > 1)
            {
                // The receiving player canno't have maximum health
                if (recieverPlayer.GetHealth() < recieverPlayer.GetMaxHealth())
                {
                    mainPlayer.DecrementHealth();
                    recieverPlayer.IncreaseHealth();
                    current++;

                    if(current == LIMIT)
                    {
                        buttonImage.color = Color.grey;
                    }

                }

            }
        }
        else
        {
            buttonImage.color = Color.grey;
        }

    }


}
