using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelesteManController : MonoBehaviour {

	public float speed;
	public float jumpForce = 100f;
	public GameObject bulletPrefab;

	private Transform pj;
	private Animator anim;
	public bool shouldJump = true;

	private bool isOnTheFloor;
	private Transform groundCheck;
	public LayerMask whatIsGround;
	public Vector2 groundCheckSize;

	private bool lookRight = false;
	private Vector2 bulletPosition;
	private float bulletSpeed = 1;
	private float jumpProbability = 0.7f;

	// Use this for initialization
	void Start () {

		pj = GameObject.Find ("JP").transform;
		anim = GetComponent<Animator> ();
		groundCheck = transform.FindChild("GroundCheck");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Movement
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Shoot") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Glowing")) {
			Vector2 movement = (Vector2.left * speed * Time.deltaTime);
			transform.Translate (movement);
			anim.SetFloat ("speed", Mathf.Abs (movement.x));
		}

		//Flip
		if (!lookRight && transform.position.x <= 111.5f) {
			Flip ();
			shouldJump = true;
			anim.SetTrigger ("shoot"); 
		}
		if (lookRight && transform.position.x >= 117f) {
			Flip ();
			shouldJump = true;
			anim.SetTrigger ("shoot");
		}

		//GroundCheck
		isOnTheFloor = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0,  whatIsGround);
		
	}

	void Update() {

		//To avoid mega-jumps
		if (GetComponent<Rigidbody2D>().velocity.y > 6) {
			Invoke ("ResetVelocity", 0.12f);
		}
		
		//Interaction
		if (isOnTheFloor) {
			if (shouldJump) {

				float option = Random.Range(0f, 1f);

				//When he's near the player it will choose wether to jump or shoot
				if (transform.position.x <= pj.position.x + 1f && !lookRight && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Shoot")) {
					if (option <= jumpProbability) {
						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
						shouldJump = false;
					} else {
						if (transform.position.x < pj.transform.position.x) {
							Flip();
						}
						anim.SetTrigger ("shoot");
					}

				} else if (transform.position.x >= pj.position.x - 1f && lookRight && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Shoot")) {
					if (option <= jumpProbability) {
						GetComponent<Rigidbody2D>().velocity = Vector3.zero;
						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
						shouldJump = false;
					} else {
						if (transform.position.x > pj.transform.position.x) {
							Flip();
						}
						anim.SetTrigger ("shoot");
					}

				}
			}
			anim.SetBool ("jump", false);
		} else if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Glowing")){
			anim.SetBool ("jump", true);
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
	public void Shoot() {

		bulletPosition = new Vector2 (transform.position.x - 0.5f * transform.localScale.x, transform.position.y);
		GameObject bullet = Instantiate (bulletPrefab, bulletPosition, transform.rotation);

		//Adjust the movement of the bullet
		bullet.GetComponent<MetShootBehavior>().speed *= transform.localScale.x * bulletSpeed;
	}

	public void Enrage() {

		speed *= 1.5f;
		bulletSpeed = 1.2f;
		jumpProbability = 0.5f;
	}

	public void EnrageThing() {
		GetComponentInParent<Animator> ().SetBool ("enrage", false);
	}

	private void ResetVelocity() {
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
	}
		
}
