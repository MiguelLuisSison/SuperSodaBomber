using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
PlayerMovement
	ovesees the controllers of the player 
	and interprets it to the PlayerControl Script
*/
public class PlayerMovement : MonoBehaviour {


	//importing scripts
	public PlayerControl controller;

	//Variables
	float horizontalMove = 0f;
	public Joystick joystick;
	public float runSpeed = 40f;
	bool jump = false;

	//OnPress of Jump Button
	public void PressJump(){
		jump = true;
	}
	
	void Update () {

		//movement
		if (joystick.Horizontal >= .5f){
			horizontalMove = runSpeed;
		}
		else if (joystick.Horizontal <= -.5f){
			horizontalMove = -runSpeed;
		}
		else{
			horizontalMove = 0;
		}

		//jump
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

	}

	void FixedUpdate ()
	{
		// interprets the controls to the script
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}
}
