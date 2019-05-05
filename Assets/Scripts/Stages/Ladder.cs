using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	private GameObject jp;

	// Use this for initialization
	void Start () {

		jp = GameObject.Find ("JP");
		
	}

	void OnTriggerEnter2D() {

		jp.GetComponent<JpControl> ().canClimb = true;

	}

	void OnTriggerExit2D() {

		jp.GetComponent<JpControl> ().canClimb = false;
		jp.GetComponent<JpControl> ().isClimbing = false;
		jp.GetComponent<Rigidbody2D> ().gravityScale = 1f;
		jp.GetComponent<Animator>().SetFloat ("climbingSpeed", 0);
		jp.GetComponent<Animator> ().SetBool("isClimbing", false);
		jp.GetComponent<Animator> ().speed = 1;
		//jp.GetComponent<Animator> ().SetTrigger("reincorporate");

	}
}
