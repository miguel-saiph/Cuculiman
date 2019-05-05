using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveLadder : MonoBehaviour {

	private GameObject jp;

	// Use this for initialization
	void Start () {

		jp = GameObject.Find ("JP");

	}


	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject == jp) {

			if (jp.GetComponent<JpControl> ().isClimbing) {
			
				jp.GetComponent<JpControl> ().canClimb = false;
				//jp.GetComponent<JpControl> ().isClimbing = false;
				jp.GetComponent<Rigidbody2D> ().gravityScale = 1f;
				jp.GetComponent<Animator> ().SetFloat ("climbingSpeed", 0);
				jp.GetComponent<Animator> ().SetBool ("isClimbing", false);
			}
		}


	}
}
