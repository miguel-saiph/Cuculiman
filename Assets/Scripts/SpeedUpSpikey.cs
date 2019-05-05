using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSpikey : MonoBehaviour {

	public float newSpeed = 3f;

	void OnTriggerStay2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {
			GetComponentInChildren<SpikeyControl> ().speed = 3;
			GetComponentInChildren<Animator> ().speed = 2;
			if (other.transform.position.x <= GetComponentInChildren<Transform> ().position.x &&
			    GetComponentInChildren<SpikeyControl> ().facingRight) {
				GetComponentInChildren<SpikeyControl> ();
			}
		}

	}
}
