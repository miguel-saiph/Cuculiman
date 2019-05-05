using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeyControl : MonoBehaviour {

	public float speed = 3f;

	public float leftLimit;
	public float rightLimit;

	public bool facingRight = false;

	// Use this for initialization
	void Start () {

		if (facingRight) {
			speed *= -1;
			var s = transform.localScale;
			s.x *= -1;
			transform.localScale = s;
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if (transform.localPosition.x >= rightLimit || transform.localPosition.x <= leftLimit) {
			Flip ();
		}
		transform.Translate (Vector2.left * speed * Time.deltaTime);
	}

	void Flip() {

		speed *= -1;
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		facingRight = !facingRight;
	}
}
