using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    [SerializeField]
    PlayerHealth playerHealth;

    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private Sprite emptyHeart;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Image[] hearts;

  
    // Update is called once per frame
    void Update()
    {

        health = playerHealth.GetHealth();
        maxHealth = playerHealth.GetMaxHealth();

        for (int heartIndex = 0; heartIndex  < hearts.Length; heartIndex ++)

        { 
            if (heartIndex < health)
            {
                hearts[heartIndex].sprite = fullHeart;
            }
            else
            {
                hearts[heartIndex].sprite = emptyHeart;

            }


            if (heartIndex < maxHealth)
            {
               hearts[heartIndex].enabled = true;

            }
            else
            {
                hearts[heartIndex].enabled = false;

            }

        }
    }
}
