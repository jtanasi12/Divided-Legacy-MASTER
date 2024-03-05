using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoyAnimations : PlayerAnimationController
{
  public void ShootBowState()
    {
        character.Animator.SetBool("SimpleBowShot", true);

    }
}
