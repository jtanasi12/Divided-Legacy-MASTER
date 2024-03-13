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

public class MainAnimationController : MonoBehaviour
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

    public void SetDeadEyes()
    {
       // FindSpriteItem("Common.Basic.Mouth.Zombie");
      //  playerAnimation.FindSpriteItem("Common.Bonus.Mouth.11");

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

    public void SetAttackDamageState()
    {
        Debug.Log("ANIMATION");
        character.Animator.SetTrigger("Hit");
    }

    public void DeathAState()
    {
        character.SetState(CharacterState.DeathB);
    }

    public void DeathBState()
    {
        character.SetState(CharacterState.DeathF);
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

    public void WalkAnimation()
    {
        character.SetState(CharacterState.Walk);
    }

}


