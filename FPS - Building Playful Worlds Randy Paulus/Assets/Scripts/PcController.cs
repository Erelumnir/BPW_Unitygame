using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PcController : MonoBehaviour {

	// Creates the variables
	public float forwardMoveSpeed = 1f;
	public float sidewaysMoveSpeed = 1f;

	public bool menuOpen;
	public GameObject inGameMenu;
	public camMouseLook camLook;

	// Runs at start
	void Start (){
		// Hides and Locks cursor on start
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		menuOpen = false;
	}

	//############################################## MENU ###############################################

	void Update(){
		// In game menu controls
		if (Input.GetKeyDown ("escape") && menuOpen == false) {
			Debug.Log ("Pressed Escape");
			// Returns 
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			menuOpen = true;
			Time.timeScale = 0;
			inGameMenu.SetActive (true);
			camLook.enabled = false;

		}
		// Return to game
		else if (Input.GetKeyDown ("escape") && menuOpen == true) {
			Debug.Log("Pressed Escape again!");
			// Return to game and lock cursor again
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			menuOpen = false;
			Time.timeScale = 1;
			inGameMenu.SetActive (false);
			camLook.enabled = true;
		}
			
		// Exit game
		if (Input.GetKeyDown("tab")){
			Application.Quit ();
		}
	}

	//############################################## MOVEMENT ###############################################

	// Updates every Frame
	void FixedUpdate (){
		// Horizontal and Vertical Movement modified by movespeed variables
		transform.Translate (forwardMoveSpeed * Input.GetAxis ("Vertical") * Time.deltaTime, 0f, sidewaysMoveSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime);
	}
}
