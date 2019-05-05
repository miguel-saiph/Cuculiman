using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

	public GameObject explosionPrefab;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.layer == 8 || other.gameObject.tag == "Player") { //8 = Environment layer
			GetComponent<SpriteRenderer> ().enabled = false;
			Instantiate (explosionPrefab, transform.position, transform.rotation);
			//It will still doing damage for 0.5f after it explodes
			Invoke ("AutoDestroy", 0.5f);
		}
	}

	void AutoDestroy() {
		Destroy (gameObject);
	}
}
