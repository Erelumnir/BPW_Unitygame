using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { Idle, Patrol, Follow, Attack, Dead }

public class EnemyController : MonoBehaviour {

	// Set references
	public Enemy enemy;
	public EnemyHealth enemyHealth;
	public PlayerHealth playerHP;
	public State currentState;
	public Transform[] points;
	public Transform[] spawnPoints;

	private int destPoint = 0;
	private GameObject player;
	private NavMeshAgent agent;
	private float distanceToTarget;

	// Use this for initialization
	void Start () {
		enemy.enemyGO = 
		player = GameObject.FindWithTag ("Player");
		agent = GetComponent<NavMeshAgent> ();
		playerHP = GameObject.FindWithTag ("Player").GetComponent<PlayerHealth> ();

		//Spawns enemies with delay
		InvokeRepeating ("Spawn", enemy.spawnTime, enemy.spawnRate);
	}

	// Update is called once per frame
	void Update () {
		
		// Checks state every frame
		CheckState ();

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
		Instantiate (enemy.enemyGO, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
	//########################################################## AI ################################################################

	void CheckState(){
		// If player has been flagged as dead...
		if (enemyHealth.isDead == true){
			// ...set state to dead
			currentState = State.Dead;
		}
			
		// If player isn't in range...
		if (player == null) {
			// set state to patrol
			currentState = State.Patrol;
		}

		// Distance between this enemy and player
		distanceToTarget = Vector3.Distance(player.transform.position, transform.position);
			
	// States
		switch (currentState) {
			
		case State.Attack:
			// If cooldown has been set...
			if (enemy.coolDown > 0) {
				// ...start counting down
				enemy.coolDown -= Time.deltaTime;
			}

			// If player is in attack range...
			if (distanceToTarget < enemy.attackRange && enemy.coolDown <= 0) {
				// ...attack player...
				playerHP.TakeDamage (enemy.GetRandomDamage ());
				// ...and set cooldown timer
				enemy.coolDown = enemy.maxCooldown;
			}
			// If player moves out of attack range...
			if (distanceToTarget > 2 * enemy.attackRange) {
				// ...set state to follow
				currentState = State.Follow;
			}
			// If player moves out of look radius...
			else if(distanceToTarget > enemy.lookRadius){
				// ...set state to Patrol
				currentState = State.Patrol;
			}
			break;

		case State.Idle:
			// If player is in look radius...
			if(distanceToTarget < enemy.lookRadius){
				// ...set state to follow
				currentState = State.Follow;
			}
			break;

		case State.Patrol:
			// Set agent move speed to patrol speed
			agent.speed = 0.5f;

			// Choose next destination point when nearing current one
			if (!agent.pathPending && agent.remainingDistance < 0.1f) {

				// If there aren't any patrol points...
				if (points.Length == 0) {
					return;
				}

				// Set agent to go to the currently selected destination
				agent.destination = points [destPoint].position;

				// Choose next point in array as destination, cycle back to start if necessary
				destPoint = (destPoint + 1) % points.Length;
			}

			// If player is in look radius...
			if(distanceToTarget < enemy.lookRadius){
				// ...set state to follow
				currentState = State.Follow;
			}
			break;

		case State.Follow:
			// Increase agent speed
			agent.speed = 1.25f;

			// Follow the player
			agent.SetDestination (player.transform.position);
			// If the player is in attack range...
			if (distanceToTarget < enemy.attackRange){
				//...set state to Attack
				currentState = State.Attack;
			}
			// If player moves out of look radius...
			if (distanceToTarget > enemy.lookRadius) {
				// ...set state to Patrol
				currentState = State.Patrol;
			}
			break;

		case State.Dead:
			// Remove all AI functionalities and colliders
			this.GetComponent<NavMeshAgent> ().enabled = false;
			this.GetComponent<CapsuleCollider> ().enabled = false;


			// If the enemy should be sinking...
			if (enemyHealth.isSinking) { 
				// ...move the enemy down by the sinkSpeed per second.
				transform.Translate (-Vector3.up * enemy.sinkSpeed * Time.deltaTime);
				// Disable x,z constrains of the rigidbody
				this.GetComponent<Rigidbody>() .freezeRotation = false;

				Invoke ("DestroyOnDeath", 0.75f);

			} 

			break;
		}
	}

	void DestroyOnDeath(){
		// Destroys killed gameObject after invoked
		Destroy(gameObject);
		//Debug.Log ("Game Object destroyed!");
	}
}