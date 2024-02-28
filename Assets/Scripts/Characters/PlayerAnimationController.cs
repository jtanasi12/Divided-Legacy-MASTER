using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HeroEditor.Common.Enums;

using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.Common;
using System;
using UnityEngine.TextCore.Text;

public class PlayerAnimationController : MonoBehaviour
{

    #region animatorRegion
    public Assets.HeroEditor.Common.Scripts.CharacterScripts.Character character;

    #endregion


    private void FindSpriteItem(string spriteId)
    {
        for (int index = 0; index < character.SpriteCollection.Mouth.Count; ++index)
        {
            if (character.SpriteCollection.Mouth[index].Id == spriteId)
            {
                var mouthIndex = character.SpriteCollection.Mouth[index];
                character.SetBody(mouthIndex, BodyPart.Mouth);

            }

        }
    }

   public void SetIdleState()
    {
        character.SetState(CharacterState.Idle);

    }
    public void SetWalkAnimation(float horizontalInput)
    {

        // Set Animations 
        if (horizontalInput == 0)
        {
            character.SetState(CharacterState.Idle);
        }

        else
        {
            character.SetState(CharacterState.Walk);
        }
    }
}


/*
      if (characterName.Equals("Cloud Boy"))
      {
          FindSpriteItem("Common.Bonus.Mouth.10");
      }
      else
      {
          FindSpriteItem("Common.Emoji.Mouth.Injured");

      }
 */