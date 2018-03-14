using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWeapon {
	
	public string name = "Glock";

	public int damage = 10;
	public float range = 100f;
	public float fireRate = 0.5f;
	public float nextShot;
}
