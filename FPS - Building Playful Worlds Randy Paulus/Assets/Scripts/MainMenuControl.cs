using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuControl : MonoBehaviour {

	// References
	PcController inGameMenu;
	public Slider musicSlider;
	public AudioMixer audioMixer;

	void Start(){
		inGameMenu = GetComponent<PcController> ();
	}

	public void SetVolume (float volume){
		// Debug.Log (volume);
		audioMixer.SetFloat("musicVol", volume);
	}

	public void PlayGame(){
		// Load next scene
		SceneManager.LoadScene(1);
	}

	public void ExitGame(){
		// Exit Game
		Application.Quit ();
	}

	public void ResumeGame(){
		// Resume Game
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		inGameMenu.menuOpen = false;
		Time.timeScale = 1;
		inGameMenu.inGameMenu.SetActive (false);
		inGameMenu.camLook.enabled = true;
	}

	public void ReturnMenu(){
		// Return to menu
		SceneManager.LoadScene(0);
	}
}
