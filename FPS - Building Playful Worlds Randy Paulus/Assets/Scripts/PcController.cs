using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcController : MonoBehaviour {

	//Creates the variables
	public float forwardMoveSpeed;
	public float sidewaysMoveSpeed;

	//Runs at start
	void Start (){
		Cursor.lockState = CursorLockMode.Locked;
	}

	//Updates every Fram
	void FixedUpdate (){
		
		//Horizontal and Vertical Movement modified by movespeed variables
		transform.Translate (forwardMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0f, sidewaysMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);

		if (Input.GetKeyDown ("escape"))
			Cursor.lockState = CursorLockMode.None;
	}
}
