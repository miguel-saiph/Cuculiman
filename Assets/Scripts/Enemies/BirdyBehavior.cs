using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdyBehavior : MonoBehaviour {

	public float speed = 1f;
	private float defaultY;
	private GameObject jp;
	private bool isReady = true; //Is ready to attack?
	private bool attack = false;
	private bool moveDown;


	void Start () {
		jp = GameObject.Find ("JP");
		defaultY = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.Translate (Vector2.left * speed * Time.deltaTime);

		//It will go straight until it's near the pj
		//It executes only one time
		if (isReady && transform.position.x <= jp.transform.position.x + 4f) {
			isReady = false;
			attack = true;
			moveDown = true;
		}

		if (attack && moveDown) {
			transform.Translate (Vector2.down * speed * Time.deltaTime);
			if (transform.position.y <= defaultY - 1.5f) {
				moveDown = false;
				//Para que no empiece a subir de inmediato
				attack = false;
				Invoke ("GoUp", 0.7f);
			}
		} else if (attack && !moveDown) {
			transform.Translate (Vector2.up * speed * Time.deltaTime);
			if (transform.position.y >= defaultY) {
				moveDown = true;
				attack = false;
			}
		}
		
	}

	void Update() {

		AutoDestroy ();
	}

	void GoUp() {
		attack = true;
	}

	void AutoDestroy() {
		if (Camera.main.ViewportToWorldPoint (new Vector3 (-0.5f, 0, 0)).x >= transform.position.x) {
			Destroy (gameObject);

			//Send message to the spawner to spawn again
			if (GetComponentInParent<CameraSpawner>()) {
				GetComponentInParent<CameraSpawner> ().hasSpawned = false;
			}
		}
	}
}
