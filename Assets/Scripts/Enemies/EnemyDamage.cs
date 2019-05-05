using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	public int hp = 5;
	public float timeOfColor = 0.2f;
	public GameObject explosionPrefab;
	public bool pivotIsCentered = true; //Make an empty object in the center if this is false
	public bool destroy = true; //It will be destroyed or disabled?
	public bool isParent = true; //Is this the main object?

	private Vector2 defaultPosition;
	private int defaultHp;
	private SpriteRenderer sr;
	private Animator anim;
	private ReSkinAnimation resKin;

	void Awake () {

		defaultHp = hp;

		if (isParent) {
			sr = GetComponent<SpriteRenderer> ();
			anim = GetComponent<Animator> ();
			resKin = GetComponent<ReSkinAnimation>();
			defaultPosition = transform.position;
		} else {
			sr = GetComponentInParent<SpriteRenderer> ();
			anim = GetComponentInParent<Animator> ();
			resKin = GetComponentInParent<ReSkinAnimation>();
			defaultPosition = transform.parent.gameObject.transform.position;
		}

	}

	void OnEnable() {

		if (isParent) {
			transform.position = defaultPosition;
		} else {
			transform.parent.gameObject.transform.position = defaultPosition;
		}
		hp = defaultHp;
		sr.enabled = true;
		gameObject.GetComponent<Collider2D>().enabled = true;
		anim.enabled = true;
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Lemon") {
			Destroy (other.gameObject);
			if (resKin) {
				resKin.enabled = true;
				Invoke ("DisableReSkin", timeOfColor);
			}

			//Destruction
			hp -= 1;
			if (hp == 0) {

				if (destroy) {
					if (!isParent) { //Destroy the parent in case this is a compound enemy (complex animations and stuff)
						Destroy (gameObject.transform.parent.gameObject);
					} else {
						Destroy (gameObject);
					}
				} else {

					//Disable things to enable again after a player's death
					sr.enabled = false;
					gameObject.GetComponent<Collider2D> ().enabled = false;
					anim.enabled = false;

				}

				if (explosionPrefab) {
					Vector3 enemyPosition;
					if (!pivotIsCentered) {
						enemyPosition = transform.GetChild (0).position;
					} else {
						enemyPosition = transform.position;
					}
					Instantiate (explosionPrefab, enemyPosition, Quaternion.identity);
				}
			} else {
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	void DisableReSkin() {

		if (resKin) {
			resKin.enabled = false;
		}

	}

	void OnDestroy() {

		//Send message to the spawner to spawn again
		if (GetComponentInParent<CameraSpawner>()) {
			GetComponentInParent<CameraSpawner> ().hasSpawned = false;
		}
	}
}
