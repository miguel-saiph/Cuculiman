using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberController : MonoBehaviour {

	public float speed;
	public GameObject bombPrefab;

	private Animator anim;
	private Vector2 bombPosition;

	// Use this for initialization
	void Start () {

		speed = speed * transform.localScale.x;
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {

		//Movement
		gameObject.transform.Translate (Vector2.right * speed * Time.deltaTime);
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		//Invisibly collider in the stage triggers the bomb
		if (other.tag != "Player" && other.tag != "Invincibility" && other.tag != "Enemy") {
			anim.SetTrigger ("dropBomb");
			Invoke ("DropBomb", 0.5f);
		}
	}

	void DropBomb() {
		bombPosition = new Vector2 (transform.position.x, transform.position.y - 0.5f);
		Instantiate (bombPrefab, bombPosition, transform.rotation);
	}

	void OnDestroy() {

		//Send message to the spawner to spawn again
		if (GetComponentInParent<CameraSpawner>()) {
			GetComponentInParent<CameraSpawner> ().hasSpawned = false;
		}
	}
}
