using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeySpeedUp : MonoBehaviour {

	private float defaultSpeed;
	private float modifier = 0;

	void Start() {
		defaultSpeed = GetComponentInChildren<SpikeyControl> ().speed;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible") {
			if (GetComponentInChildren<SpikeyControl> ().facingRight) {
				modifier = 1;
			} else {
				modifier = -1;
			}
			GetComponentInChildren<SpikeyControl> ().speed = 4 * modifier;
		}

	}

	void OnTriggerExit2D(Collider2D other) {

		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Invincible") {

			if (GetComponentInChildren<SpikeyControl> ().facingRight) {
				modifier = 1;
			} else {
				modifier = -1;
			}

			GetComponentInChildren<SpikeyControl> ().speed = defaultSpeed * modifier;
		}


	}
}
