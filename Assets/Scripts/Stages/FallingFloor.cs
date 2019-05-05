using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour {

	private float fallSpeed = 0.2f; //It mess up the groundcheck when more than 0.2
	private bool fall = false;

	private Vector2 defaultPosition;

	void Awake() {

		defaultPosition = transform.position;
	}

	void OnEnable() {

		transform.position = defaultPosition;
		fall = false;
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		if (fall) {
			transform.Translate (Vector2.up * -fallSpeed * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D() {

		if (!fall) {
			fall = true;
			Invoke ("StopFalling", 8);
		}

	}

	void StopFalling() {
		fall = false;
	}
}
