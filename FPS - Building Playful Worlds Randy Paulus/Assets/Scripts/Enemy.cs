using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy {

	public GameObject enemyGO; 
	public string name = "Grunt";
	public int minDamage;
	public int maxDamage;
	public float coolDown = 2f;
	public float attackRange;
	public float lookRadius = 2;
	public float maxCooldown;
	public float sinkSpeed = 0.1f;
	public float spawnTime = 2;
	public float spawnRate = 5;

	public int GetRandomDamage ()
	{
		int randomDamage = Random.Range (minDamage, maxDamage);
		return randomDamage;
	}
}