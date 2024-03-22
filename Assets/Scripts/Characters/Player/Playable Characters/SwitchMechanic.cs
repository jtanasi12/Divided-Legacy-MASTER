using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMechanic : MonoBehaviour
{
    public GameObject cloudBoy;
    public GameObject split;

    [SerializeField]
    private PlayerHealth cloudBoyHealth;

    [SerializeField]
    private PlayerHealth splitHealth;

    public bool isCloudBoyActive = true;

    private void Awake()
    {
        
        split.GetComponent<PlayableCharacters>().enabled = false;

        split.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.RightShift)){
            SwitchPlayer();
        }   
    }

    private void SwitchPlayer(){

        isCloudBoyActive = !isCloudBoyActive;


        if (isCloudBoyActive)
        {
            // If we switch to Cloudboy set Split inactive
            SwitchSplitInactive();
            SwitchCloudBoyActive();

            cloudBoy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            split.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            // If the player is dead, we do not want to switch the animataion
            if (!splitHealth.GetIsPlayerDead())
            {
                split.GetComponent<SplitAnimations>().SetIdleState();

            }

        }
        else 
        {
            // If we switch to Split, set CloudBoy inactive
            SwitchCloudBoyInactive();
            SwitchSplitActive();

            split.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            cloudBoy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            // If the player is dead, we do not want to switch the animataion
            if (!cloudBoyHealth.GetIsPlayerDead())
            {
                cloudBoy.GetComponent<CloudBoyAnimations>().SetIdleState();

            }


        }
        cloudBoy.GetComponent<PlayableCharacters>().enabled = isCloudBoyActive;

        split.GetComponent<PlayableCharacters>().enabled = !isCloudBoyActive;



    }

    private void SwitchCloudBoyInactive()
    {
        cloudBoy.layer = LayerMask.NameToLayer("SwitchedState");

        cloudBoy.GetComponent<PlayableCharacters>().SetCharacterState(true);

        Debug.Log("ClOUDBOY INACTIVE");
    }

    private void SwitchSplitInactive()
    {
        split.layer = LayerMask.NameToLayer("SwitchedState");

        Debug.Log("SPLIT INACTIVE");

        split.GetComponent<PlayableCharacters>().SetCharacterState(true);
    }

    private void SwitchCloudBoyActive()
    {
        cloudBoy.layer = LayerMask.NameToLayer("Player");

        cloudBoy.GetComponent<PlayableCharacters>().SetCharacterState(false);

        Debug.Log("CLOUDBOY ACTIVE");

    }

    private void SwitchSplitActive()
    {
        split.layer = LayerMask.NameToLayer("Player");

        split.GetComponent<PlayableCharacters>().SetCharacterState(false);

        Debug.Log("SPLIT ACTIVE");


    } 
}
