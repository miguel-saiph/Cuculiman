using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour {

	public GameObject bulletPrefab;

	//To modify the orientation
	private int bulletModifier;

	void OnEnable () {

		if (transform.rotation.z != 0) {

			bulletModifier = -1;
		} else {
			bulletModifier = 1;
		}
		
	}

	//Called from the animator
	public void SpitShoot() {

		GameObject bullet = Instantiate (bulletPrefab, new Vector2(transform.position.x, transform.position.y - 0.4f * bulletModifier), transform.rotation);
		bullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, -100 * bulletModifier));
		if (GetComponent<SpriteRenderer>().isVisible) {
			GetComponent<AudioSource> ().Play ();
		}
	}
}
