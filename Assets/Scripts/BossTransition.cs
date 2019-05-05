using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTransition : MonoBehaviour {

	public Vector2 newCameraPosition;
	public Vector2 newPlayerPosition;
	public float speed = 10f;
	public GameObject actualZone;
	public GameObject nextZone;
	public GameObject door;
	public GameObject boss;
	public GameObject bossLifeBar;
	public GameObject playerLifeBar;

	private GameObject pj;
	private bool moveCamera = false;
	private bool movePj = false;
	private bool activateThings = true;

	// Use this for initialization
	void Start () {

		pj = GameObject.Find ("JP");

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!moveCamera && door.GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName ("Opened")) {
			moveCamera = true;
			movePj = true;
			pj.GetComponent<Rigidbody2D> ().gravityScale = 1;

		}

		if (moveCamera) {
			if (Camera.main.transform.position.x <= newCameraPosition.x) {
				Camera.main.transform.Translate (Vector2.right * speed * Time.deltaTime);
			} else {
				moveCamera = false;
			}

			if (Camera.main.transform.position.y <= newCameraPosition.y) {
				Camera.main.transform.Translate (Vector2.up * speed * Time.deltaTime);
			} else {
				moveCamera = false;
			}
		}

		if (movePj) {
			if (pj.transform.position.x <= newPlayerPosition.x) {
				pj.transform.Translate (Vector2.right * speed * Time.deltaTime * 0.2f);
				pj.GetComponent<Animator> ().SetFloat ("speed", 1);
			} else {
				pj.GetComponent<Animator> ().SetFloat ("speed", 0);
				movePj = false;

				//Dialogue
				//GameManager.gm.GetComponent<InGameDialogue> ().enabled = true;

				if (activateThings) {
					Invoke("ActiveDialogue", 0.4f);
					//BOSS THINGS HERE
					Invoke("ActiveBoss", 0.5f);
					activateThings = false;
				}

			}
		}
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" || other.tag == "Invincibility") {
			if (door) {
				door.GetComponent<Animator> ().SetTrigger ("Open");
				pj.GetComponent<Rigidbody2D> ().gravityScale = 0;
			}

			if (GameObject.Find("Colemonkey_3")) {
				GameObject.Find ("Colemonkey_3").gameObject.SetActive (false);
			}

			pj.GetComponent<JpControl> ().enabled = false;

			if (other.tag == "Player") {
				//playerLifeBar.gameObject.SetActive(false);
				Camera.main.GetComponent<Camera2D> ().enabled = false;
				Invoke ("DisablePreviousZone", 4);
				/*
				if (worldLimit) {
					Invoke ("EnableWorldLimit", 0.5f);
				}*/
				nextZone.gameObject.SetActive (true);
				boss.GetComponent<Animator>().enabled = false;
				Invoke ("DestroyThis", 3f);
			}
		}

	}

	void DisablePreviousZone() {
		actualZone.gameObject.SetActive(false);
	}

	void ActiveBoss() {
		playerLifeBar.SetActive (true);
		bossLifeBar.SetActive(true);
		bossLifeBar.GetComponent<Image>().enabled = true;
		boss.GetComponent<Animator>().enabled = true;
		boss.GetComponentInChildren<BossLifeBarBehavior> ().enabled = true;
	}

	void ActiveDialogue() {

		enabled = false;
		playerLifeBar.SetActive (false);
		GameManager.gm.GetComponent<InGameDialogue> ().enabled = true;
	}

	void DestroyThis() {
		gameObject.GetComponent<EdgeCollider2D> ().enabled = false;
		gameObject.GetComponent<BoxCollider2D> ().enabled = true; //New world limit

		//pj.GetComponent<JpControl> ().enabled = true; //This is in bosslifebehavior script
	}
		
}
