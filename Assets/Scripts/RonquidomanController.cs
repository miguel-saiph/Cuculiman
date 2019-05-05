using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonquidomanController : MonoBehaviour {
	public float speed;
	public float jumpForce = 100f;
	public GameObject rockPrefab;

	private Transform pj;
	private Animator anim;
	private Rigidbody2D rb;

	private bool isOnTheFloor;
	private Transform groundCheck;
	public LayerMask whatIsGround;
	public Vector2 groundCheckSize;

	private bool lookRight = false;
	private Vector2 rockPosition;

	private bool canJump = true; //When the player is near
	private bool shouldJump = true; //If the player doesn't move for a while
	private bool begin = true;
	public bool enrage = false;
	public bool enrageAnim = false;

	// Use this for initialization
	void Start () {

		pj = GameObject.Find ("JP").transform;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		groundCheck = transform.FindChild("GroundCheck");
	}

	// Update is called once per frame
	void FixedUpdate () {

		//Flip
		if (!lookRight && pj.transform.position.x >= transform.position.x) {
			Flip (); 
		}
		if (lookRight && pj.transform.position.x <= transform.position.x) {
			Flip ();
		}




		//GroundCheck
		isOnTheFloor = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0,  whatIsGround);

	}

	void Update() {


		/*
		if (isOnTheFloor) {


			//To stick to the ground after jump
			var x = GetComponent<Rigidbody2D> ().velocity;
			x.x = 0;
			rb.velocity = x;

			rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
		} else {
			rb.constraints = RigidbodyConstraints2D.None;
		}*/
		

		//Interaction
		if (isOnTheFloor && canJump) {
			if (!lookRight && pj.transform.position.x >= transform.position.x - 1.5f && anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				rb.AddForce (new Vector2 (-jumpForce, jumpForce));
				CancelInvoke ("Jump");
				Invoke("TriggerAttack", 1.5f);
				canJump = false;
				Invoke ("JumpCooldown", 2);
				Invoke ("Jump", 3.5f);


			} else 	if (lookRight && pj.transform.position.x <= transform.position.x + 1.5f && anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				rb.AddForce (new Vector2 (jumpForce, jumpForce));
				CancelInvoke ("Jump");
				Invoke("TriggerAttack", 1.5f);
				canJump = false;
				Invoke ("JumpCooldown", 2);
				Invoke ("Jump", 3.5f);
			}

		}

		if (shouldJump && anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			if (begin) {
				Invoke ("Jump", 1.5f);
				begin = false;
			} else {
				Invoke ("Jump", 3.5f);
			}
			shouldJump = false;
		}

		if (!enrage && enrageAnim && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Sleep")) {
			anim.SetTrigger ("sleep");
			canJump = true;
			shouldJump = true;
			begin = true;
		}

		if (!isOnTheFloor) {
			anim.SetBool ("jump", true);
		} else {
			anim.SetBool ("jump", false);
		}


	}

	void Flip() {

		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		lookRight = !lookRight;
		speed *= -1;
	}

	//Called from an animation event
	public void ThrowRock() {

		float x = Random.Range (pj.transform.position.x + 0.4f, pj.transform.position.x - 0.4f);
		rockPosition = new Vector2 (x, -11.22f);
		GameObject rocky = Instantiate (rockPrefab, rockPosition, transform.rotation);
		rocky.GetComponent<Rigidbody2D> ().gravityScale = 2f;
		if (enrage) {
			x = Random.Range (158.8f, 160f);
			rockPosition = new Vector2 (x, -11.22f);
			rocky = Instantiate (rockPrefab, rockPosition, transform.rotation);
			rocky.GetComponent<Rigidbody2D> ().gravityScale = 2f;

			x = Random.Range (163.6f, 164.8f);
			rockPosition = new Vector2 (x, -11.22f);
			rocky = Instantiate (rockPrefab, rockPosition, transform.rotation);
			rocky.GetComponent<Rigidbody2D> ().gravityScale = 2f;
		}


	}

	public void Jump() {

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			if (!lookRight) {
				rb.AddForce (new Vector2 (-jumpForce, jumpForce));
			} else {
				rb.AddForce (new Vector2 (jumpForce, jumpForce));
			}
			shouldJump = true;
			Invoke("TriggerAttack", 1.5f);
			canJump = false;
			Invoke ("JumpCooldown", 2);
		}


	}

	void JumpCooldown() {
		canJump = true;
	}

	void TriggerAttack() {

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			anim.SetTrigger ("attack");
		}
	}

}
