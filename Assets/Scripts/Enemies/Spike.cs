using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other) {

			if (other.tag == "Player") {
				Kill ();
				other.tag = "Invincible";
			}
	}

	void OnTriggerStay2D(Collider2D other) {

			if (other.tag == "Player") {
				Kill ();
				other.tag = "Invincible";
			}
	}


	void Kill() {
		GameManager.gm.PlayerDeath ();
	}
		
}
