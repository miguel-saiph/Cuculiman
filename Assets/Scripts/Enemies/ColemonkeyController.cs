using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColemonkeyController : MonoBehaviour {

	public float jumpForce;
	public float distance = 3; //Distance to detect the player
	public enum Encounter
	{
		First = 0, Second = 1, Third = 2
	}

	public Encounter encounterNumber;

	private Transform pj;
	private Animator anim;
	private bool jumping = false;
	private bool isOnTheFloor;

	private Transform groundCheck;
	public LayerMask whatIsGround;
	public Vector2 groundCheckSize;

	private int jumpCounter = 0;
	private bool facingRight = false;

	private bool move = false;
	private bool defaultPosition;
	private bool defaultHp;

	// Use this for initialization
	void Start () {

		pj = GameObject.Find ("JP").GetComponent<Transform>();
		anim = GetComponent<Animator> ();
		groundCheck = transform.Find("GroundCheck");

		if (encounterNumber == Encounter.Second) {
			Flip ();
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if (encounterNumber == Encounter.First) {
			if (jumpCounter >= 3) {
				jumpForce += 100;
				jumpCounter = 0;
			}
		}

		if (!jumping) {
			if (encounterNumber == Encounter.First) {
				if (pj.position.x >= transform.position.x - distance && pj.position.x <= transform.position.x) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (100, jumpForce));
					jumping = true;
					jumpCounter++;
					Invoke ("JumpCooldown", 1.5f);
				}
			}

			if (encounterNumber == Encounter.Second) {
				if (pj.position.x >= transform.position.x + distance && pj.position.x >= transform.position.x) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (100, jumpForce));
					jumping = true;
					Invoke ("JumpCooldown", 1.5f);
				}
			}

			if (encounterNumber == Encounter.Third) {
				if (!move) {
					if (pj.position.x >= transform.position.x - 4) {
						move = true;
					}
				} else {
					if (facingRight && pj.position.x <= transform.position.x || !facingRight && pj.position.x >= transform.position.x) {
						Flip ();
					}
						
					Vector2 randomJump = new Vector2(Random.Range (50, 120), Random.Range(100, 300));

					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-randomJump.x * transform.localScale.x, randomJump.y));
					jumping = true;
					Invoke("JumpCooldown", 1.5f);

				}

			}
				
		}

		//GroundCheck
		isOnTheFloor = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0,  whatIsGround);
		if (encounterNumber == Encounter.Third) {
			if (!isOnTheFloor) {
				anim.SetBool ("attack", true);
			} else {
				anim.SetBool ("attack", false);
			}


		} else {
			if (!isOnTheFloor) {
				anim.SetBool ("jump", true);
			} else {
				anim.SetBool ("jump", false);
			}
		}
		
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Lemon") {

			if (encounterNumber != Encounter.Third) {
				GetComponent<AudioSource> ().Play ();
			}
			Destroy (other.gameObject);
		}

		if (other.gameObject.GetComponent<Spike>()) {
			Destroy (gameObject);
		}
	}

	void JumpCooldown() {
		jumping = false;

		if (encounterNumber == Encounter.Third) {
			anim.SetBool ("attack", false);
		}
	}

	void Flip() {

		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		facingRight = !facingRight;
	}

	void OnDestroy() {
		if (encounterNumber == Encounter.Third) {
			GameManager.gm.EnableBossTransition ();
		}
	}
}
