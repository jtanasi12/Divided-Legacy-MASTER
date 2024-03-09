using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HeroEditor.Common.Enums;

using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.Common;
using System;
using UnityEngine.TextCore.Text;
using HeroEditor.Common;

public class PlayerAnimationController : MonoBehaviour
{

    #region animatorRegion
    public Assets.HeroEditor.Common.Scripts.CharacterScripts.Character character;

    #endregion


    public void FindSpriteItem(string spriteId)
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

    public void SetVictoryState() {
        character.Animator.SetBool("Victory", true);

    }


    public void EatSupplyState()
    {
        character.Animator.SetBool("UseSupply", true);

    }

    public void SetClimbState()
    {
        character.SetState(CharacterState.Climb);
    }

    public void SetJumpState()
    {

        character.SetState(CharacterState.Jump);

        
    }

    public void SetRunState()
    {
        character.SetState(CharacterState.Run);

    }

   

    public void SetWalkAnimation(float horizontalInput, bool isGrounded)
    {
        // Set Animations 
        if (horizontalInput == 0 && isGrounded && !Input.GetButton("Jump"))
        {
            character.SetState(CharacterState.Idle);
        }
        else if(horizontalInput != 0 && isGrounded)
        {
            character.SetState(CharacterState.Walk);
        }
    }

}


