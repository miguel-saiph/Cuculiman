using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour {

	void OnEnable() {
		GetComponentInParent<Rigidbody2D> ().gravityScale = 0;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincibility") {

			GetComponentInParent<Rigidbody2D> ().gravityScale = 1f;
			Invoke ("MakeInvisible", 2f);
		}

	}

	void MakeInvisible() {
		GetComponentInParent<SpriteRenderer> ().enabled = false;
		GetComponentInParent<Rigidbody2D> ().gravityScale = 0;
	}
}
