using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public PlayerWeapon weapon;
	public EnemyHealth enemyHealth;
	public Rigidbody rb;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;


	void Start (){

		//Check if camera has been referenced, else return error.
		if (cam == null) {
			Debug.LogError("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}
	}

	void Update(){
		//Fire
		if (Input.GetButton ("Fire1") && Time.time > weapon.nextShot) {
			Shoot ();
		}
	}

	void Shoot(){

		AudioSource shotSound = GetComponent<AudioSource>();
		shotSound.Play ();
		weapon.nextShot = Time.time + weapon.fireRate;
		//Debug.Log("You shot");

		//Shoots and check hit collider with Raycasting
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out	hit, weapon.range, mask)) {

			if (hit.collider.tag == "Enemy") {

				//Find EnemyHealth script in gameobject hit.
				enemyHealth = hit.collider.GetComponent <EnemyHealth> ();
				rb = hit.collider.GetComponent<Rigidbody> ();

				rb.AddForce(Vector3.forward * -weapon.knockbackForce, ForceMode.Impulse);
				//Debug.Log ("Force is being Applied!");

				//If exists...
				if (enemyHealth != null) {

					//...enemy takes damage. 
					enemyHealth.TakeDamage (weapon.GetRandomDamage());
					//Debug.Log ("You hit for: " + weapon.GetRandomDamage());
					//Debug.Log ("Health Left: " + enemyHealth.currHP);
				}
			} 

		//Debugs
		//Debug.Log ("You hit " + hit.collider.name);
		Debug.DrawRay (cam.transform.position, cam.transform.forward * weapon.range, Color.green, 5.0f);
		}
	}
}
