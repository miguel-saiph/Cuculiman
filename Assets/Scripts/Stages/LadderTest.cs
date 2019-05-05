using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTest : MonoBehaviour {

	public float climbingSpeed = 1;
	GameObject pj;

	bool canClimb = false;

	void Awake() {
		pj = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player" || other.tag == "Invincible") {
			canClimb = true;
			//pj.GetComponent<Rigidbody2D> ().gravityScale = 0;
			GetComponentInChildren<EdgeCollider2D>().isTrigger = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player" || other.tag == "Invincible") {
			canClimb = false;
			pj.GetComponent<JpControl> ().isClimbing = false;
			pj.GetComponent<Animator> ().SetBool ("isClimbing", false);
			//pj.GetComponent<Animator> ().SetFloat ("climbingSpeed", 0);
			pj.GetComponent<Rigidbody2D> ().gravityScale = 1;
			pj.GetComponent<Animator> ().speed = 1;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (pj.GetComponent<JpControl> ().isClimbing) {
			pj.GetComponent<Rigidbody2D> ().gravityScale = 0;
			pj.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		} else {
			pj.GetComponent<Rigidbody2D> ().gravityScale = 1;
			pj.GetComponent<Animator> ().speed = 1;
		}
	}

	void Update () {

		if (canClimb) {

			if (Input.GetAxis ("Vertical") > 0) {
				pj.transform.Translate (Vector2.up * climbingSpeed * Time.deltaTime);
				//pj.GetComponent<Animator> ().SetFloat ("climbingSpeed", 1);
				pj.GetComponent<JpControl> ().isClimbing = true;
				pj.GetComponent<Animator> ().SetBool ("isClimbing", true);
				pj.GetComponent<Animator> ().speed = 1;
				//pj.GetComponent<Animator> ().Play ("Climb");
				
			} else if (Input.GetAxis ("Vertical") < 0) {
				pj.transform.Translate (Vector2.down * climbingSpeed * Time.deltaTime);
				//pj.GetComponent<Animator> ().SetFloat ("climbingSpeed", 1);
				pj.GetComponent<JpControl> ().isClimbing = true;
				pj.GetComponent<Animator> ().SetBool ("isClimbing", true);
				pj.GetComponent<Animator> ().speed = 1;
				//pj.GetComponent<Animator> ().Play ("Climb");
			} else if (pj.GetComponent<JpControl> ().isClimbing) {
				pj.GetComponent<Animator> ().speed = 0;
			}

		}
	}
}
