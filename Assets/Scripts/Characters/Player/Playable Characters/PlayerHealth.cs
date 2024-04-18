using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

using UnityEngine;

public class PlayerHealth : Health
{
    #region Variables

    [SerializeField]
    private PlayableCharacters playableCharacter;

    [SerializeField]
    private GameObject loseMenu;

    [SerializeField]
    AudioSource hurtSoundFX;

    [SerializeField]
    AudioSource deathSoundFX;

    private bool switchState;

    private bool playerIsDead = false;



    #endregion


    public int GetMaxHealth() {

        return maxHealth;
    }

    public void IncreaseHealth()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += 1;
        }
       
    }

    public void DecrementHealth() {

        currentHealth -= 1;

    }

 
    public bool GetSwitchedState()
    {

        switchState = playableCharacter.GetSwitchedState();

        return switchState;
    }

    public override void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            deathSoundFX.Play();

            // Player Dies
            playerAnimation.DeathAState();
            isDead = true;
        }
        else
        {
            // Attack Animation
            Debug.Log("Player takes damage");

            hurtSoundFX.Play();

            if (!isFlickering)
            {
                StartCoroutine(DamageFlicker());

            }

        }
    }




}
