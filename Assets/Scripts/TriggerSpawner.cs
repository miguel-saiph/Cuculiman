using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {

			Instantiate (enemyPrefab, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
