using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBugController : MonoBehaviour {

	public int speed;
	public GameObject leaf;

	private SpriteRenderer sr;
	private Collider2D col;
	private GameObject pj;

	private bool facingRight = false;
	private bool move = false;
	private bool isMoving = false;

	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		sr.enabled = false;
		col = GetComponent<Collider2D> ();
		col.enabled = false;
		pj = GameObject.Find ("JP");

		//SpawnLeaf
		if (leaf) {
			Instantiate (leaf, transform.position, transform.rotation);
		}

		//Spawn itself
		Invoke("ShowBug", 1.5f);

		if (facingRight) {
			Flip ();
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (move) {

			if (facingRight && pj.transform.position.x <= transform.position.x ||
				!facingRight && pj.transform.position.x >= transform.position.x) {
				Flip ();
			}

			//Move Towards target
			transform.position = Vector2.MoveTowards (transform.position, pj.transform.position, speed * Time.deltaTime);
		}
		
	}

	void Update() {

		//It moves 2 sec and stands 2 sec
		if (!isMoving) {
			isMoving = true;
			Invoke ("Move", 2f);
		}


	}

	void ShowBug() {
		sr.enabled = true;
		col.enabled = true;
		//move = true;
	}

	void Move() {
		move = true;
		Invoke ("Stop", 2f);

	}

	void Stop() {
		move = false;
		isMoving = false;
	}

	void Flip() {

		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		facingRight = !facingRight;
	}

	void OnDisable() {
		Destroy (gameObject);
	}

	/*
	void OnDestroy() {


		if (GetComponentInParent<OnCameraSpawner> ()) {
			GetComponentInParent<OnCameraSpawner> ().hasSpawned = false;
		}
	}*/
}
