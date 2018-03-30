using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { Idle, Follow, Attack, Dead }

public class EnemyController : MonoBehaviour {

	// Set references
	public Enemy enemy;
	public EnemyHealth enemyHealth;
	public PlayerHealth playerHP;
	public State currentState;

	private GameObject player;
	private NavMeshAgent agent;
	private float distanceToTarget;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		agent = GetComponent<NavMeshAgent> ();
		playerHP = GameObject.FindWithTag ("Player").GetComponent<PlayerHealth> ();
	}

	// Update is called once per frame
	void Update () {
		
		// Checks state every frame
		CheckState ();

	}

	void CheckState(){
		// If player has been flagged as dead...
		if (enemyHealth.isDead == true){
			// ...set state to dead
			currentState = State.Dead;
		}
			
		// If player isn't in range...
		if (player == null) {
			// set state to patrol
			currentState = State.Idle;
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
				currentState = State.Idle;
			}
			break;

		case State.Idle:
			// Enemy wanders in small area
			//a gent.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));

			agent.SetDestination(RandomNavSphere(this.transform.position, 10f, -1));
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
				currentState = State.Idle;
			}
			break;

		case State.Dead:
			// Remove all AI functionalities and colliders
			this.GetComponent<NavMeshAgent> ().enabled = false;
			this.GetComponent<BoxCollider> ().enabled = false;

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

	// Creds: Unityblog user: MysterySoftware
	public static Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask) {
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

		randomDirection += origin;

		NavMeshHit navHit;

		NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);

		return navHit.position;
	}

	void DestroyOnDeath(){
		// Destroys killed gameObject after invoked
		Destroy(gameObject);
		//Debug.Log ("Game Object destroyed!");
	}
}