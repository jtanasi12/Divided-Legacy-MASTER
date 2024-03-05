using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMechanic : MonoBehaviour
{
    public PlayableCharacters cloudBoy;
    public PlayableCharacters split;
    
    public bool player1Active = true;

    private void Awake()
    {
        split.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.RightShift)){
            SwitchPlayer();
        }   
    }

    private void SwitchPlayer(){
        player1Active = !player1Active;
        cloudBoy.enabled = player1Active;
        split.enabled = !player1Active;
        Debug.Log("SHOULD have toggled controls");

    }
}
