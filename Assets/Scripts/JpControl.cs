using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JpControl : MonoBehaviour {

	public float speed = 3.0f;
	public GameObject projectilePrefab;

	private Animator anim;
	private float x;
	private float y;

	private bool lookRight = true;

	public float jumpForce = 100f;
	private bool isOnTheFloor;

	private bool shooting = false;

	private Rigidbody2D rb;

	//private Sprite idle;
	//public Sprite jumping;
	//public Sprite shootingSpriteOnAir;

	private Transform groundCheck;
	public LayerMask whatIsGround;
	public Vector2 groundCheckSize;
	//private float groundedRadius = 0.1f;

	public bool canClimb = false;
	public bool isClimbing = false;

	public int hp = 30;
	public GameObject hurtedThing;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		//idle = sr.sprite;
		groundCheck = transform.Find("GroundCheck");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Para ver dónde está el personaje en relación a la cámara
		//Debug.Log(Camera.main.WorldToViewportPoint(gameObject.transform.position));

		//Movimiento
		float x = Input.GetAxis ("Horizontal");


		transform.Translate (Vector2.right * x * speed * Time.deltaTime);
		//La condición para la animación de caminar
		anim.SetFloat("speed", Mathf.Abs(x));
			
		//Cambio de lado
		if (lookRight && x < 0) {
			Flip ();
		} else if (!lookRight && x > 0) {
			Flip ();
		}

		//Subir escalera
		if (canClimb) {
			y = Input.GetAxis ("Vertical");
			anim.SetFloat ("climbingSpeed", Mathf.Abs (y));
			if (y > 0) {
				rb.gravityScale = 0f;
				transform.Translate (Vector2.up * y * speed * Time.deltaTime);
				if (!isClimbing) {
					isClimbing = true;
					anim.SetBool ("isClimbing", true);
				}
			}
			if (isClimbing && y == 0) {
				anim.speed = 0;
			} else {
				anim.speed = 1;
			}

		}

		//Salto

		isOnTheFloor = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0,  whatIsGround);
		//isOnTheFloor = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
		/*
		isOnTheFloor = Physics2D.Linecast(transform.FindChild("LinearCheck1").position, groundCheck.position, whatIsGround);
		if (!isOnTheFloor) {
			isOnTheFloor = Physics2D.Linecast(transform.FindChild("LinearCheck2").position, groundCheck.position, whatIsGround);
		}*/
		/*for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				isOnTheFloor = true;
		}*/

		//Fuerza a velocity.y a siempre ser cero cuando esté en tierra (como debería ser...)
		/*if (isOnTheFloor) {
			var v = rb.velocity;
			v.y = 0;
			rb.velocity = v;
		}*/

		/*
		if (Input.GetButtonDown ("Jump")) {
			//if (isOnTheFloor) {
				if (rb.velocity.y == 0) {
					rb.AddForce (new Vector2(0f, jumpForce));
				}
			//}
		}*/
			

	}

	void Update() {

		if (Input.GetButtonDown ("Jump")) {
			if (isOnTheFloor) {
				rb.AddForce (new Vector2 (0f, jumpForce));
			}
		}

		//Salto con animación
		if (!isOnTheFloor && !isClimbing && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Hurted")) {
			anim.SetBool ("jump", true);
		} else {
			anim.SetBool ("jump", false);
		}

		//Attack
		if (Input.GetButtonDown ("Fire1")) {

			if (!shooting && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Hurted")) {
				anim.SetBool ("shooting", true);
				shooting = true;
				Shoot ();
				Invoke ("ShootCooldown", 0.25f); //Delay de disparo
			}

		}
	}

	void OnTriggerEnter2D (Collider2D other) {

		//Daño
		if (gameObject.tag == "Player") {
			if (other.gameObject.tag == "Enemy") {
				/*if (!anim.enabled) {
					anim.enabled = true;
				}*/
				anim.SetTrigger ("hurted");
				/*
				if (GetComponent<AudioSource> ()) {
					GetComponent<AudioSource> ().Play ();
				}*/
				//gameObject.transform.Translate (new Vector2 (0.5f * transform.localScale.x * -1, 0)); //Debería ser una fuerza?
				rb.AddForce(new Vector2 (60f * transform.localScale.x * -1, 0));
				this.GetComponent<Invincibility> ().enabled = true;

				if (GameManager.gm) {
					int damage = other.GetComponent<DamageDone> ().damageDone;
					GameManager.gm.SubstractHealth (damage);
					if (GameManager.gm.playerHp > 1) {
						//Instantiate (hurtedThing, transform.position, transform.rotation, transform);
					}
				}
			}
		}
	}

	void OnTriggerStay2D (Collider2D other) {

		//Recieve damage
		if (gameObject.tag == "Player") {
			if (other.gameObject.tag == "Enemy") {
				/*if (!anim.enabled) {
					anim.enabled = true;
				}*/
				//Instantiate (hurtedThing, transform.position, transform.rotation, transform);
				anim.SetTrigger ("hurted");
				/*
				if (GetComponent<AudioSource> ()) {
					GetComponent<AudioSource> ().Play ();
				}*/
				//gameObject.transform.Translate (new Vector2 (0.5f * transform.localScale.x * -1, 0));
				rb.AddForce(new Vector2 (60f * transform.localScale.x * -1, 0));
				this.GetComponent<Invincibility> ().enabled = true;

				if (GameManager.gm) {
					int damage = other.GetComponent<DamageDone> ().damageDone;
					GameManager.gm.SubstractHealth (damage);
				}
			}
		}
	}

	void Flip() {

		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		lookRight = !lookRight;
	}
		
	void Shoot() {

		GameObject projectile;

		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			//Create a new projectile at the position of the pj + a positive or negative x that depends on the direction that is facing
			projectile = Instantiate (projectilePrefab, new Vector3 (transform.position.x + 0.32f * transform.localScale.x, transform.position.y + 0.03f, transform.position.z), transform.rotation);
		} else {
			projectile = Instantiate (projectilePrefab, new Vector3 (transform.position.x + 0.32f * transform.localScale.x, transform.position.y + 0.11f, transform.position.z), transform.rotation);
		}
		if (!lookRight) {
			projectile.GetComponent<ProjectileControl> ().shootRight = false;
		}

		//shooting = false;
		//anim.SetBool ("shooting", false);

	}

	void ShootCooldown() {
		if (shooting) {
			shooting = false;
			anim.SetBool ("shooting", false);
		}
	}

	public void TurnRight() {

		if (!lookRight) {
			Flip ();
		}
	}


}
