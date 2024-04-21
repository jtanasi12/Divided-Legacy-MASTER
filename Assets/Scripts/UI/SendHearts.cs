using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

using UnityEngine;

public class SendHearts : MonoBehaviour
{
    [SerializeField]
    private AudioSource heartsFX;

    [SerializeField]
    private PlayerHealth mainPlayer;

    [SerializeField]
    private PlayableCharacters player; 

    [SerializeField]
    private PlayerHealth recieverPlayer;

    private bool active = true;
    // For Button
    [SerializeField]
    private Image buttonImage;


   public bool GetActive()
    {
        return active;
    }

    public void Reset()
    {
        active = true;
        buttonImage.color = Color.white;

    }

    private int current = 0;

    private void Update()
    {
        if (mainPlayer.gameObject.GetComponent<PlayableCharacters>().GetFlag())

        {
            active = false;

            buttonImage.color = Color.gray;
        }
    }
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

            {
            // Make sure the other player hasn't already completed the level
            if (!recieverPlayer.gameObject.GetComponent<PlayableCharacters>().GetFlag())
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

                        heartsFX.Play();

                    }

                }
            }
            }


    }

    public void ResetColor()
    {
        buttonImage.color = Color.white;
    }
}
