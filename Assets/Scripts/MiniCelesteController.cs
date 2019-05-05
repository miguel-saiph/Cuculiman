using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCelesteController : MonoBehaviour {

	private bool canAttack = true;

	public int hp = 10;
	public float timeOfColor = 0.2f; //El tiempo en que va a estar de otro color cuando reciba un disparo
	public GameObject explosionPrefab;
	public GameObject boomerangPrefab;
	public AudioClip damage;
	public AudioClip noDamage;

	//private Transform pj;
	private Animator anim;
	private GameObject boomerang;
	private Vector2 boomerangPosition;
	private bool firstAttack = true;


	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		//pj = GameObject.Find ("JP").transform;
		boomerangPosition = new Vector2 (transform.position.x + 0.62f * transform.localScale.x, transform.position.y + 0.34f);

	}

	void OnEnable() {
		firstAttack = true;
		hp = 10;
	}

	// Update is called once per frame
	void Update () {

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle1") && 
			Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x >= transform.position.x) {
			anim.SetTrigger ("prepare");
		}

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Defense") && canAttack) {
			if (!firstAttack) {
				Invoke ("Attack", 4);
			} else {
				Invoke ("Attack", 2);
				firstAttack = false;
			}
			canAttack = false;
			anim.SetBool ("canAttack", false);
		}

	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Lemon") {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Defense")) {
				Destroy (other.gameObject);
				GetComponent<AudioSource> ().clip = damage;
				GetComponent<AudioSource> ().Play ();
				if (this.GetComponent<ReSkinAnimation> ()) {
					this.GetComponent<ReSkinAnimation> ().enabled = true;
					Invoke ("DisableReSkin", timeOfColor);
				}
				hp -= 1;
				if (hp == 0) {
					Destroy (gameObject);
					if (boomerang) {
						Destroy (boomerang);
					}

					if (explosionPrefab) {
						Vector3 enemyPosition = transform.GetChild (0).position;
						Instantiate (explosionPrefab, enemyPosition, Quaternion.identity);
					}
				}
			} else {
				Destroy (other.gameObject);
				GetComponent<AudioSource> ().clip = noDamage;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	void OnDestroy() {
		GameManager.gm.EnableBossTransition ();
	}

	void DisableReSkin() {

		if (this.GetComponent<ReSkinAnimation>()) {
			this.GetComponent<ReSkinAnimation> ().enabled = false;
		}

	}

	void Attack() {
		anim.SetTrigger("attack");
		boomerang = Instantiate(boomerangPrefab, boomerangPosition, transform.rotation);
		canAttack = true;
		Invoke ("SetAttack", 2); //Time of life of the boomerang set in the Timed object destructor of the prefab
	}

	void SetAttack() {
		anim.SetBool ("canAttack", true);
	}
}
