using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kill the player on contact
public class Abysm : MonoBehaviour {

	private bool canKill = true; //To avoid multiple killings in correlative frames

	void OnTriggerEnter2D(Collider2D other) {

		if (canKill) {
			if (other.tag == "Player" || other.tag == "Invincible") {

				GameManager.gm.PlayerDeath ();
				canKill = false;
				Invoke ("EnableKill", 2);
			}
		}


	}

	void EnableKill() {
		canKill = true;
	}
}
