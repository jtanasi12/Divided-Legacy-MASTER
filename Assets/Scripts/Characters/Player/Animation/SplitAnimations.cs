using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitAnimations : MainAnimationController
{

    public void SetJab()
    {
        character.Animator.SetTrigger("Jab");
    }
}
