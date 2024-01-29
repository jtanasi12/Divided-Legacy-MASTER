using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Used to handle physics - uses a consistent speed of 0.002 seconds
    // and is frame-rate independant
    [SerializeField]
    private CharacterController2D controller;
    [SerializeField]
    private float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;


    // Called before the first frame update
    void Start(){
    }

   
    // Update is called once per frame
    void Update()
    {
        // Get input Left & Right and A & D
        // Left = -1, No movement = 0, Right = 1
         horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }

        // Keep crouch true while we hold the 's' button down 
        // and only stop crouching when we let go of 's'
        if(Input.GetButtonDown("Crouch")){
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch")){
            crouch = false;
        }
    }

     // Frame rate independant, is calculated at every 0.002 seconds
    void FixedUpdate(){

        // Time.fixedDeltaTime has us move the same amount of time every frame which is 0.002 seconds. This makes it consistent across all plateforms
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        // Ensures that we don't jump forever
        jump = false;

   }
   
}
