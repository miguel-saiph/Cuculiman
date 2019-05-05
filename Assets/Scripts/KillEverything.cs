using UnityEngine;
using System.Collections;

public class KillEverything : MonoBehaviour {

	public GameObject explosionPrefab;

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {
			Instantiate (explosionPrefab, other.transform.position, Quaternion.identity);
			other.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			other.gameObject.GetComponent<JpControl> ().enabled = false;
			//Destroy (other.gameObject);
		}

	}

}
