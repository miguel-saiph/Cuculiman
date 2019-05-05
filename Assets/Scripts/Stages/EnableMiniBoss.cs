using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMiniBoss : MonoBehaviour {

	public GameObject miniBoss;
	public bool isActive = false;

	void OnEnable() {
		miniBoss.SetActive (false);
		isActive = false;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {
			if (!isActive) {
				miniBoss.SetActive (true);
				isActive = true;
			}
		}

	}
}
