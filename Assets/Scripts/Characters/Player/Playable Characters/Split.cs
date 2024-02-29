using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Split : PlayableCharacters
{




// Called once per frame
protected override void Update()
    {
        base.Update();

        DoubleJump(); // Split has the ability to perform double jumps
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void DoubleJump()
    {
        if (controller is SplitController splitController)
        {
            splitController.DoubleJump();
        }

    }
}