using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnEnable : MonoBehaviour {

	private Vector2 defaultPosition;

	// Use this for initialization
	void Awake () {

		GetComponent<SpriteRenderer> ().enabled = true;
		defaultPosition = gameObject.transform.position;

	}

	void OnEnable() {
		GetComponent<SpriteRenderer> ().enabled = true;
		transform.position = defaultPosition;
	}
}
