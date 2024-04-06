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

  

    


}
