using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMechanic : MonoBehaviour
{
    public GameObject cloudBoy;
    public GameObject split;
    
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
            cloudBoy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            split.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            split.GetComponent<SplitAnimations>().SetIdleState();

        }
        else 
        {
            split.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            cloudBoy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            cloudBoy.GetComponent<CloudBoyAnimations>().SetIdleState();


        }
        cloudBoy.GetComponent<PlayableCharacters>().enabled = isCloudBoyActive;

        split.GetComponent<PlayableCharacters>().enabled = !isCloudBoyActive;
        Debug.Log("SHOULD have toggled controls");


    }
}
