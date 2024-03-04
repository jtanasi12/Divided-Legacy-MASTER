using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitAnimations : PlayerAnimationController
{
    public void SetAttackState()
    {
        character.Slash();
    }

    public void SetJab()
    {
        character.Animator.SetTrigger("Jab");
    }
}
