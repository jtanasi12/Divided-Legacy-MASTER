using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

using UnityEngine;

public class SendHearts : MonoBehaviour
{
    private int LIMIT = 1;

    [SerializeField]
    private PlayerHealth mainPlayer;

    [SerializeField]
    private PlayerHealth recieverPlayer;

    private bool active = true;

    [SerializeField]
    private Image buttonImage;


    public void Reset()
    {
        active = true;
        buttonImage.color = Color.white;

    }

    private int current = 0;


    public void SetCurrent(int newCurrent)
    {
        current = newCurrent;
    }

    public int GetCurrent()
    {
        return current;
    }

    // Triggered when the button is clicked on
    public void SendHeart()
    {
        Debug.Log("Sending a heart to " + recieverPlayer.gameObject.GetComponent<Characters>().GetCharacterName());

        if (active)

        // Allow this power to be used X amount of times only
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
                    active = false;

                    buttonImage.color = Color.gray;

                }

            }
        }
      

    }

    public void ResetColor()
    {
        buttonImage.color = Color.white;
    }
}
