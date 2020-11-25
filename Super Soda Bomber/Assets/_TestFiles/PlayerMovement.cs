﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour {

	public PlayerControl controller;

	public float runSpeed = 40f;
	public Joystick joystick;
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	
	// Update is called once per frame
	void Update () {

		// horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (joystick.Horizontal >= .2f){
			horizontalMove = runSpeed;
		}
		else if (joystick.Horizontal <= -.2f){
			horizontalMove = -runSpeed;
		}
		else{
			horizontalMove = 0;
		}

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

	}

	void FixedUpdate ()
	{

		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
