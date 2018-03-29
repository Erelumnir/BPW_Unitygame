using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour {

	// Sets references
	public int startHp;
	public int currHP;
	public float sinkSpeed = 2.5f;

	private TextMeshProUGUI UI_HP;

	bool isDead;
	bool isSinking;
	PlayerWeapon weapon;


	void Start(){
		// Sets currentHP to the Starting HP
		currHP = startHp;

		// Get textComponent
		UI_HP = GameObject.Find("LiveCounter").GetComponent<TextMeshProUGUI>();
	}

	void Update(){

		// If the enemy should be sinking...
		if(isSinking){
			//...move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up *sinkSpeed * Time.deltaTime);
		}

		// Adjust UI to match currHP
		UI_HP.text = currHP.ToString();
	}
		

	public void TakeDamage (int amount){

		if (gameObject.tag == "Player") {
			// If player is dead..
			if (isDead) {
				// ..no need to take damage so exit function. 
				return;
			}

			// Reduce health based on taken damage.
			currHP -= amount;

			// If health falls below 0 or is equal to...
			if (currHP <= 0) {
				//...enemy dies.
				Death ();
				Debug.Log ("You Died!");
			}
		}
	}

	void Death(){

		// Player died.
		isDead = true;
		SceneManager.LoadScene (2);
		// Debug.Log("Player Killed");
		isSinking = true;
	}
}
