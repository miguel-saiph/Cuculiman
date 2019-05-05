using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public Vector2 respawnPosition;
	public Vector3 cameraPosition;

	public bool activated = false;
	public static GameObject[] checkpointsList;

	void Start() {

		checkpointsList = GameObject.FindGameObjectsWithTag ("Checkpoint");

	}

	private void ActivateCheckpoint() {

		//Deactivate all checkpoints in the scene
		foreach (GameObject temp in checkpointsList) {

			temp.GetComponent<Checkpoint> ().activated = false;
			
		}
		//Activate the current checkpoint
		activated = true;
	}
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {
			
			ActivateCheckpoint ();
		}
	}
}
