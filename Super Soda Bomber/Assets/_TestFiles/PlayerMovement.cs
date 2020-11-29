using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour {

	public PlayerControl controller;
	public float runSpeed = 40f;
	public Joystick joystick;
	float horizontalMove = 0f;
	bool jump = false;

	//OnPress of Jump Button
	public void PressJump(){
		jump = true;
	}

	// triggers when player touches the checkpoint
    private void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.layer == 10){
			Debug.Log("Checkpoint!");
		}
    }
	
	// Update is called once per frame
	void Update () {


		if (joystick.Horizontal >= .5f){
			horizontalMove = runSpeed;
		}
		else if (joystick.Horizontal <= -.5f){
			horizontalMove = -runSpeed;
		}
		else{
			horizontalMove = 0;
		}

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}
}
