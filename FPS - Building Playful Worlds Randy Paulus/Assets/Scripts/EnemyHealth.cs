using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	//Sets variables
	public int startHP = 100;
	public int currHP;

	public bool isDead = false;
	public bool isSinking;
	PlayerWeapon weapon;
	EnemyController EC;

	void Awake(){

		//Sets currentHP to the Starting HP
		currHP = startHP;
	}
		
	void OnGUI (){
		
		//GUI.Label (new Rect(10,10, 100, 100), "Enemy HP: " + currHP.ToString());

	}

	public void TakeDamage (int amount){

		if (gameObject.tag == "Enemy") {
			//If enemy is dead..
			if (isDead) {
				//..no need to take damage so exit function. 
				return;
			}

			//Reduce health based on taken damage.
			currHP -= amount;

			//If health falls below 0 or is equal to...
			if (currHP <= 0) {
				//...enemy dies.
				Death ();
				Debug.Log ("You Killed " + this.gameObject.name);
			}
		}
	}

	void Death(){

		//Enemy died.
		isDead = true;
		isSinking = true;
		//Debug.Log("Enemy Killed");

	}
}
