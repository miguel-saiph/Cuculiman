using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLadderTop : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerStay2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {

			if (Input.GetAxis("Vertical") < 0) {
				Debug.Log ("Holi");
				GetComponentInParent<EdgeCollider2D> ().isTrigger = true;
			}

		}

			
	}
}
