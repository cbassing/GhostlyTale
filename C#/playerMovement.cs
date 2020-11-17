using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class playerMovement : MonoBehaviour {

   public CharacterController2D controller;
   public float runSpeed = 40f;

   float horizontalMove = 0;
   bool jump = false;
   bool crouch = false;
   
    private void Start() 
    {
        
    }
    
    
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Roll"))
        {
            crouch = true;
        }

    }
    
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.FixedDeltaTime, roll, jump);
        jump = false;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}