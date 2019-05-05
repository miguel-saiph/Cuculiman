using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour {

	public int healthAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {

			GameManager.gm.HealthUp(healthAmount);
			Destroy (gameObject.transform.parent.gameObject);
		}
	}


}
