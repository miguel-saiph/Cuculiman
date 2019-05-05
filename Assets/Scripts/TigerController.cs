using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour {

	public float speed;
	public float jumpForce;
	public float distance = 3; //Distance to detect the player
	public bool facingRight = false;

	private Transform pj;
	private Animator anim;
	private bool move = false;
	private bool jumping = false;
	private bool isOnTheFloor;

	private Transform groundCheck;
	public LayerMask whatIsGround;
	public Vector2 groundCheckSize;


	// Use this for initialization
	void Start () {

		pj = GameObject.Find ("JP").GetComponent<Transform>();
		anim = GetComponent<Animator> ();
		groundCheck = transform.FindChild("GroundCheck");

		speed *= -1;

		if (facingRight) {
			Flip ();
		}
		
	}

	void FixedUpdate() {

		if (move) {
			Vector2 movement = Vector2.right * speed * Time.deltaTime;
			transform.Translate (movement);
			anim.SetFloat ("speed", Mathf.Abs(movement.x));

			if (!jumping) {
				if (!facingRight && pj.position.x >= transform.position.x - (distance - 2.2f) && pj.position.x <= transform.position.x ||
					facingRight && pj.position.x <= transform.position.x + (distance + 2.2f) && pj.position.x >= transform.position.x) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
					jumping = true;
					Invoke ("JumpCooldown", 2f);
					anim.SetBool ("jump", true);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!facingRight && pj.position.x >= transform.position.x - distance ||
			facingRight && pj.position.x <= transform.position.x + distance)  {
			move = true;
		}
			
		//GroundCheck
		isOnTheFloor = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0,  whatIsGround);

		if (isOnTheFloor) {
			anim.SetBool ("jump", false);
		}

		//If there is ground in front of him, he flips
		if (Physics2D.Raycast(transform.FindChild("Center").position, Vector2.left * transform.localScale.x, 0.5f, whatIsGround).transform != null) {
			Flip();
		}

	}

	void Flip() {

		speed *= -1;
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		facingRight = !facingRight;
	}

	void JumpCooldown() {
		jumping = false;
	}

	void OnDisable() {
		Destroy (gameObject);
	}
}
