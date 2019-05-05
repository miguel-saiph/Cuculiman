using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCameraSpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public bool destroyOnSpawn; //If it spawns just one time

	public bool hasSpawned = false;
	private SpriteRenderer sr; //It HAS to have a renderer unlike other spawners
	private GameObject enemy;

	void Start() {

		sr = GetComponent<SpriteRenderer> ();
	}

	/*
	void Update() {

		if (sr.isVisible && !hasSpawned) {
			Instantiate (enemyPrefab, transform.position, Quaternion.identity, transform);
			hasSpawned = true;
		}
	} */
	void Update() {

		if (!enemy) {
			hasSpawned = false;
		}

		if (sr.isVisible && !hasSpawned) {

			enemy = Instantiate (enemyPrefab, transform.position, transform.rotation);
			hasSpawned = true;

		}
	}

	void OnBecameVisible() {
		
	}
}
