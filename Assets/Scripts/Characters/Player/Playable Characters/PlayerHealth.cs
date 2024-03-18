using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

using UnityEngine;

public class PlayerHealth : Health
{
    #region Variables

    [SerializeField]
    private PlayableCharacters playableCharacter;

    private bool switchState;

   


    #endregion

   

    public int GetMaxHealth() {

        return maxHealth;
    }


 
    public bool GetSwitchedState()
    {

        switchState = playableCharacter.GetSwitchedState();

        return switchState;
    }

  

    


}
