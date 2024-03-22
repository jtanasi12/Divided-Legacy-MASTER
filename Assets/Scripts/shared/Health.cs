using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected int currentHealth; // Keep track of current health

    public SpriteRenderer[] sprites;

    [SerializeField]
    protected int maxHealth = 3;

    protected bool isDead = false;

    [SerializeField]
    protected MainAnimationController playerAnimation;

    protected bool isFlickering = false;

    public int flickerAmnt;
    public float flickerDuration;

    protected float flickerTime;

    [SerializeField]
    protected Characters character;

    public int GetHealth()
    {

        return currentHealth;
    }


    public virtual void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            // Player Dies
            playerAnimation.DeathAState();
            isDead = true;
        }
        else
        {
            // Attack Animation
            Debug.Log("Player takes damage");

            if (!isFlickering)
            {
                StartCoroutine(DamageFlicker());
 
            }

        }
    }

    private void Awake()
    {
        flickerTime = (flickerDuration * flickerAmnt);
    }

    public float GetFlickerTime()
    {
        return flickerTime;
    }

    public bool GetIsPlayerDead()
    {
        return isDead;
    }

    protected IEnumerator DamageFlicker()
    {
        Color flickerColor;

        isFlickering = true; // Set flag to indicate flickering is in progress

        Color[] originalColors = new Color[sprites.Length]; // Array to store original colors for each sprite
        for (int i = 0; i < sprites.Length; i++)
        {
            originalColors[i] = sprites[i].color; // Store the original color for each sprite
        }


            flickerColor = new Color(1f, 0f, 0f, 1f); // Red color with full opacity

       
        for (int i = 0; i < flickerAmnt; i++)
        {
            float elapsedTime = 0f;
            while (elapsedTime < flickerDuration)
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].color = Color.Lerp(originalColors[j], flickerColor, elapsedTime / flickerDuration);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < flickerDuration)
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].color = Color.Lerp(flickerColor, originalColors[j], elapsedTime / flickerDuration);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // Reset colors to original after flickering completes
        for (int j = 0; j < sprites.Length; j++)
        {
            sprites[j].color = originalColors[j];
        }

        isFlickering = false; // Reset flag after flickering completes

        
    }

    
}
