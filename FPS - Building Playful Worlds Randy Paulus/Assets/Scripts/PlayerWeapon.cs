using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

	public string name = "Pistol";
	public int minDamage;
	public int maxDamage;
	public float range = 5.0f;
	public float fireRate = 0.05f;
	public float nextShot;
	public float knockbackForce = 2f;


	public int GetRandomDamage()
	{
		int randomDamage = Random.Range (minDamage, maxDamage);
		return randomDamage;
	}
}
