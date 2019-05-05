using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour {

	public float speed = 0.5f;

	private float defaultSpeed;
	private Vector2 defaultPosition;

	// Use this for initialization
	void Awake () {
		
		defaultSpeed = speed;
		defaultPosition = gameObject.transform.position;

	}

	void OnEnable() {
		
		transform.position = defaultPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (transform.position.y >= Camera.main.ViewportToWorldPoint (new Vector3 (0, 1.1f, 0)).y) {
			transform.position = new Vector2(transform.position.x, Camera.main.ViewportToWorldPoint (new Vector3 (0, -0.1f, 0)).y);
		}
		transform.Translate (Vector2.up * speed * Time.deltaTime);
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible") {
			if (speed > 0) {
				speed *= -0.8f;
			}
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible") {
			if (speed < 0) {
				speed = defaultSpeed;
			}
		}
	}

	void OnDestroy() {

		//Send message to the spawner to spawn again
		if (GetComponentInParent<CameraSpawner>()) {
			GetComponentInParent<CameraSpawner> ().hasSpawned = false;
		}
	}

}
