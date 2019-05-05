using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveLadderTest : MonoBehaviour {

	GameObject pj;
	//bool forceIdle = false;

	// Use this for initialization
	void Start () {
		pj = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" || other.tag == "Invincible") {
			pj.GetComponent<Animator> ().Play ("AfterLadder");
			pj.GetComponent<Animator> ().SetBool ("leaveLadder", true);
		}
	}

	void OnTriggerExit2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {
			if (!pj.GetComponent<JpControl> ().isClimbing) {
				gameObject.GetComponent<EdgeCollider2D> ().isTrigger = false;
				pj.GetComponent<Animator> ().SetBool ("isClimbing", false);
				pj.GetComponent<Animator> ().SetBool ("leaveLadder", false);
				//forceIdle = true;
				//Invoke ("DontForceIdle", 0.1f);
					
			} else {
				pj.GetComponent<Animator> ().SetBool ("leaveLadder", false);
				pj.GetComponent<Animator> ().SetBool ("isClimbing", false);
			}
		}
	}

	/*
	// Update is called once per frame
	void Update () {

		if (forceIdle) {
			pj.GetComponent<Animator> ().Play("Idle");
		}
		
	}

	void DontForceIdle() {
		forceIdle = false;
	}*/
}
