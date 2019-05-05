using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour {

	public Vector2 targetPosition;
	public float speed = 10f;
	public GameObject actualZone;
	public GameObject nextZone;
	public static GameObject[] transitionList;
	public bool movePj;
	public bool toStaticLevel; //Has the next level only one screen?
	public bool moveUp;
	public bool moveDown;
	public bool moveRight;
	public bool isInLadder = false;

	public float newXLimitL;
	public float newXLimitR;

	private GameObject pj;
	bool move = false;
	private bool defaultMoveUp;
	private bool defaultMoveDown;
	private bool defaultMoveRight;

	// Use this for initialization
	void Start () {

		transitionList = GameObject.FindGameObjectsWithTag ("Transition");
		pj = GameObject.Find ("JP");
		defaultMoveUp = moveUp;
		defaultMoveDown = moveDown;
		defaultMoveRight = moveRight;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (move) {
			//To freeze pj
			//Invoke ("Freeze", 0.1f);

			if (movePj) {
				pj.GetComponent<JpControl> ().enabled = false;
			}

			if (moveRight) {
				if (Camera.main.transform.position.x <= targetPosition.x) {
					Camera.main.transform.Translate (Vector2.right * speed * Time.deltaTime);
					if (movePj) {
						pj.transform.Translate (Vector2.right * speed * Time.deltaTime * 0.1f);
					}
				} else {
					moveRight = false;
				}
			}

			if (moveUp) {
				if (Camera.main.transform.position.y <= targetPosition.y) {
					Camera.main.transform.Translate (Vector2.up * speed * Time.deltaTime);
				} else {
					moveUp = false;
				}
			}

			if (moveDown) {
				if (Camera.main.transform.position.y >= targetPosition.y) {
					Camera.main.transform.Translate (Vector2.down * speed * Time.deltaTime);
				} else {
					moveDown = false;
				}
			}

			if (!moveUp && !moveDown && !moveRight) {

				move = false;
			}
		} else {
			//if (!isInLadder) {
			//	pj.GetComponent<Rigidbody2D> ().simulated = true;
			//}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincible") {
			
			Camera.main.GetComponent<Camera2D> ().enabled = false;

			if (isInLadder) {
				if (other.gameObject.GetComponent<Rigidbody2D>().gravityScale == 0 || other.gameObject.GetComponent<Rigidbody2D>().gravityScale == 1) {
					move = true;
					//Zone enabling/disabling
					Invoke ("DisablePreviousZone", 2);
					nextZone.gameObject.SetActive (true);
					Invoke ("DestroyThis", 1f);
				}
			} else {
				//pj.GetComponent<Rigidbody2D> ().simulated = false;
				//To move camera and player
				move = true;
				//Zone enabling/disabling
				Invoke ("DisablePreviousZone", 2);
				nextZone.gameObject.SetActive (true);
				Invoke ("DestroyThis", 1f);
			}


		}

	}

	void DisablePreviousZone() {
		actualZone.gameObject.SetActive(false);

		if (!toStaticLevel) {
			if (Camera.main.GetComponent<Camera2D>()) {
				Camera.main.GetComponent<Camera2D> ().isXLocked = true;
				Camera.main.GetComponent<Camera2D> ().enabled = true;
				Camera.main.GetComponent<Camera2D> ().xLimitLeft = newXLimitL;
				Camera.main.GetComponent<Camera2D> ().xLimitRight = newXLimitR;
			}
		}
	}

	void DestroyThis() { //Not really

		//pj.GetComponent<Rigidbody2D> ().simulated = true;

		//Destroy (this.gameObject);
		if (gameObject.GetComponent<EdgeCollider2D> ()) {
			gameObject.GetComponent<EdgeCollider2D> ().enabled = false;
		}

		if (gameObject.GetComponent<BoxCollider2D> ()) {
			gameObject.GetComponent<BoxCollider2D> ().enabled = true; //New world limit
		}
	
		if (movePj) {
			pj.GetComponent<JpControl> ().enabled = true;
			movePj = false;
		}
			
	}

	void Freeze() {
		pj.GetComponent<Rigidbody2D> ().simulated = false;
	}

	public void RestartTransition() {
		moveUp = defaultMoveUp;
		moveDown = defaultMoveDown;
		moveRight = defaultMoveRight;
		GetComponent<EdgeCollider2D> ().enabled = true;
		if (GetComponent<BoxCollider2D>()) {
			GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

}
