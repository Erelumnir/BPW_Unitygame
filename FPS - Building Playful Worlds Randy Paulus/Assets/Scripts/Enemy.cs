using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy {

	public string name = "Grunt";
	public int minDamage;
	public int maxDamage;
	public float coolDown = 2f;
	public float attackRange;
	public float lookRadius = 2;
	public float maxCooldown;
	public float sinkSpeed = 0.1f;

	public int GetRandomDamage ()
	{
		int randomDamage = Random.Range (minDamage, maxDamage);
		return randomDamage;
	}
}