using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	//Sets variables
	public int startHP = 100;
	public int currHP;
	private Image HealthBar2; 

	public bool isDead = false;
	public bool isSinking;
	PlayerWeapon weapon;
	EnemyController EC;

	void Start(){
		//Find and assign Healthbar sprite to variable.
		HealthBar2 = transform.Find ("EnemyCanvas").Find ("HealthbarBG").Find ("Healthbar").GetComponent<Image> ();

	}

	void Awake(){

		//Sets currentHP to the Starting HP
		currHP = startHP;
	}

	void Update(){

	}

	//void OnGUI (){
	
		//GUI.Label (new Rect(10,10, 100, 100), "Enemy HP: " + currHP.ToString());

	//}

	public void TakeDamage (int amount){

		if (gameObject.tag == "Enemy") {
			//If enemy is dead..
			if (isDead) {
				//..no need to take damage so exit function. 
				return;
			}

			//Reduce health based on taken damage.
			currHP -= amount;

			//Retract Health From UI
			HealthBar2.fillAmount = (float)currHP/(float)startHP; 

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
