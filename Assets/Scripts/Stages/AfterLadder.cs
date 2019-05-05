using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterLadder : MonoBehaviour {

	private GameObject jp;

	// Use this for initialization
	void Start () {

		jp = GameObject.Find ("JP");
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject == jp) {
			if (jp.GetComponent<JpControl> ().isClimbing) {
				jp.transform.Translate (new Vector2 (0, 0.25f)); //Translate the character above the ladder
				gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
				jp.GetComponent<Animator> ().SetTrigger ("reincorporate"); //Animation to finish climbing
				//jp.GetComponent<Animator> ().SetBool ("isClimbing", false);
				//jp.GetComponent<Animator> ().speed = 1;
				jp.GetComponent<Rigidbody2D> ().gravityScale = 1f;
				jp.GetComponent<Animator> ().SetFloat ("climbingSpeed", 0);
			}
		}
			
	}

	/*
	void OnCollisionEnter2D( Collision2D other) {

		Debug.Log ("holi");

		if (other.gameObject == jp) {
			if (!jp.GetComponent<JpControl> ().isClimbing) {
				if (Input.GetKeyDown ("down")) {
					jp.GetComponent<Animator> ().SetTrigger("reincorporate");
					jp.transform.Translate (new Vector2 (0, -0.25f));
					gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
					jp.GetComponent<JpControl> ().isClimbing = true;
					//jp.GetComponent<Animator> ().SetFloat ("speed", 0);
					jp.GetComponent<Animator> ().SetBool ("isClimbing", true);
					jp.GetComponent<Animator> ().speed = 1;
					jp.GetComponent<JpControl> ().canClimb = true;
				}
			}
		}

	}*/

}
