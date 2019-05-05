using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {

			if (GetComponent<AudioSource>()) {

				GetComponent<AudioSource> ().Play ();
			}
			GameManager.gm.ChangeLife (1);

			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<CircleCollider2D> ().enabled = false;

			Invoke ("DestroyThis", 4f);
		}

	}

	void DestroyThis() {

		Destroy (gameObject);
	}
}
