using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Variables

    public SpriteRenderer[] sprites;

    public int flickerAmnt;
    public float flickerDuration;

    private float flickerTime;

    [SerializeField]
    private PlayableCharacters playableCharacter;

    private bool switchState;

    private bool isFlickering = false;
 
    [SerializeField]
    private int currentHealth; // Keep track of current health

    [SerializeField]
    private int maxHealth = 3;

    private bool playerIsDead = false;

    [SerializeField]
    private MainAnimationController playerAnimation;

    [SerializeField]
    private Character character;

    #endregion

    public int GetHealth() {

        return currentHealth;
    }

    public int GetMaxHealth() {

        return maxHealth;
    }


    private void Awake()
    {
        flickerTime = (flickerDuration * flickerAmnt);
    }

    public float GetFlickerTime()
    {
        return flickerTime;
    }
    public bool GetSwitchedState()
    {

        switchState = playableCharacter.GetSwitchedState();

        return switchState;
    }

    public bool GetIsPlayerDead()
    {
       return playerIsDead;
    }

    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount;

        playerAnimation.SetAttackDamageState();


        if (currentHealth <= 0)
        {
            // Player Dies
            playerAnimation.DeathAState();
            playerIsDead = true;
        }
        else
        {
            // Attack Animation
            Debug.Log("Player takes damage");

            // playerAnimation.SetDeadEyes();

            if (!isFlickering)
            {
                StartCoroutine(DamageFlicker());
            }

        }
    }

    IEnumerator DamageFlicker()
    {
        isFlickering = true; // Set flag to indicate flickering is in progress

        Color[] originalColors = new Color[sprites.Length]; // Array to store original colors for each sprite
        for (int i = 0; i < sprites.Length; i++)
        {
            originalColors[i] = sprites[i].color; // Store the original color for each sprite
        }

        Color flickerColor = new Color(1f, 0f, 0f, 1f); // Red color with full opacity

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
