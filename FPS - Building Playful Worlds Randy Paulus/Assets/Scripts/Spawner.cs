using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform[] spawnPoints;
	public PlayerHealth playerHP;
	public GameObject enemy;

	public float spawnTime = 2;
	public float spawnRate = 10;

	// Use this for initialization
	void Start () {
		//Spawns enemies with delay
		InvokeRepeating ("Spawn", spawnTime, spawnRate);
	}
	
	void Spawn(){
		// Spawns enemies after spawntimer and everytime when spawntimer

		// If the player has no health left...
		if(playerHP.currHP <= 0f)
		{
			// ... exit the function.
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
