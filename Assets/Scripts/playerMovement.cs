using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Vector2 movement;

    Rigidbody2D rigidBody;
    // Used to handle physics - uses a consistent speed of 0.002 seconds
    // and is frame-rate independant

    // Called before the first frame update
    void Start(){
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

     // Frame rate independant, is calculated at every 0.002 seconds
    void FixedUpdate(){

     rigidBody.MovePosition(rigidBody.position + (movement * speed * Time.deltaTime));

   }
   
    // Update is called once per frame
    void Update()
    {
        // Check for player movement once per. frame 
        // Left = -1 No Key Press = 0 Right = 0
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
    }
   
}
