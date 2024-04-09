using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoy : PlayableCharacters
{
    void Awake(){

        SetCharacterName("CloudBoy");

        switchedState = false;
    }

// Update is called once per frame
protected override void Update()
    {
        if (DashChecK())
        {
            return; // If dashing - prevent moving, flipping and dashing again while in dash 
        }

        base.Update();

        if (controller is CloudBoyController cloudController)
        {
            cloudController.StartDashCoRoutine();
        }

      
    }

     protected override void FixedUpdate(){

        if (DashChecK())
        {
            return;
        }

        base.FixedUpdate();
    }



    private bool DashChecK()
    {

        if (controller is CloudBoyController cloudController)
        {
            if (cloudController.GetIsDashing())
            {
                return true;

            }

            return false;
        }
        return false;
    }


  }