using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Split : PlayableCharacters
{


    void Awake()
    {

        SetCharacterName("Split");

        switchedState = true;
        // Sets the default, we start at CloudBoy
    }

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