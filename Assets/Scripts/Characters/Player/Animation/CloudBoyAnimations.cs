using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CloudBoyAnimations : MainAnimationController
{
  public void ShootBowState()
    {
        character.Animator.SetBool("SimpleBowShot", true);
    }
}

