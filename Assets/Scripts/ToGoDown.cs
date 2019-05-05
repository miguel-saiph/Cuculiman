using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To go down trough a ladder when the character is above it

public class ToGoDown : MonoBehaviour {

	private GameObject jp;

	// Use this for initialization
	void Start () {

		jp = GameObject.Find ("JP");

	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject == jp) {

			if (!jp.GetComponent<JpControl> ().isClimbing) {
				if (Input.GetKeyDown ("down")) {
					jp.GetComponent<Animator> ().SetTrigger("reincorporate");
					jp.transform.Translate (new Vector2 (0, -0.25f));
					transform.parent.gameObject.SetActive (false);
					Invoke ("ReactivateLadder", 0.5f);
					jp.GetComponent<JpControl> ().isClimbing = true;
					//jp.GetComponent<Animator> ().SetFloat ("speed", 0);
					jp.GetComponent<Animator> ().SetBool ("isClimbing", true);
					jp.GetComponent<Animator> ().speed = 1;
					jp.GetComponent<JpControl> ().canClimb = true;
				}
			}
		}
			
	}

	void ReactivateLadder() {
		
		transform.parent.gameObject.SetActive (true);
	}
}
