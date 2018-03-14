using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	//Sets variables
	public int startHP = 100;
	public int currHP;
	public float sinkSpeed = 2.5f;
	private Image HealthBar2; 

	bool isDead;
	bool isSinking;
	PlayerWeapon weapon;

	void Start(){
		//Find and assign Healthbar sprite to variable.
		HealthBar2 = transform.Find ("EnemyCanvas").Find ("HealthbarBG").Find ("Healthbar").GetComponent<Image> ();

	}

	void Awake(){

		//Sets currentHP to the Starting HP
		currHP = startHP;
	}

	void Update(){
	
		//If the enemy should be sinking...
		if(isSinking){
			//...move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up *sinkSpeed * Time.deltaTime);
		}
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
				Debug.Log ("You Killed" + this.gameObject.name);
			}
		}
	}

	void Death(){
	
		//Enemy died.
		isDead = true;
		Destroy(gameObject);
		Debug.Log("Enemy Killed");
		isSinking = true;
	}
}
