using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HeroEditor.Common.Enums;

using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.Common;
using System;


// Parent class of Split & Cloud Boy
public class PlayableCharacters : Characters
{ 
    #region basicMechanics


    [SerializeField]
    public PlayerController controller;
    

    // Keeps track if the player has switched 
    protected bool switchedState;

    protected bool flagIsCaptured = false;

    #endregion


    [SerializeField]

    #region Controller Support
    private IA_Controller gamepad; // Reference to the IA_Controller (mappings for input to controller)
    private Vector2 gpMove;
    private Vector2 gpPan;

    #endregion

    #region GameState
    [SerializeField]
    private SharedState gameState;
    #endregion
     
     void Awake()
    {


        // Register the gamepad (Xbox, PlayStation, etc...)
        gamepad = new IA_Controller();
        gamepad.Gameplay.Jump.performed += ctx => controller.Jump(); // Register Jump action to a function
        gamepad.Gameplay.Skill.performed += ctx => ExecuteSkill(); // Register skill to a funtion
        gamepad.Gameplay.SwapActiveCharacter.performed += ctx => SwapCharacter(); // Register character swap to a funtion

    }

    public bool GetFlag()
    {
        return flagIsCaptured;
    }

    public bool GetSwitchedState()
    {
        return switchedState;
    }
    void OnEnable(){
        gamepad?.Gameplay.Enable();
    }

    void OnDisable(){
        gamepad?.Gameplay.Disable();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
      
        if (!gameState.isPaused){

          controller.InputMechanics();
           
            
        }
    }


    // Use for Physics - Time.FixedDeltaTime is implied = 0.002
    protected virtual void FixedUpdate()
    {
        controller.CalculatePhysics();


    }

    void ExecuteSkill()
    {
        print("this is where we would do some action stuff wooo");
    }


    public void SetCharacterState(bool switched)
    {
        switchedState = switched;
    }

    void SwapCharacter()
    {
        print("Character Swap Logic");

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem"))
        {
            Debug.Log("Collided with a Gem Stone");

            flagIsCaptured = true;

            controller.GetComponent<MainAnimationController>().VictoryAnimation();
        }


  
    }


}
