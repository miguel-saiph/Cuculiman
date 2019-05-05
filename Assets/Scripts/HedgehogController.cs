using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogController : MonoBehaviour {

	public float speed;
	public bool facingRight = false;
	public GameObject hSpikePrefab;

	public int hp = 5;
	public float timeOfColor = 0.2f; //El tiempo en que va a estar de otro color cuando reciba un disparo
	public GameObject explosionPrefab;
	public bool pivotIsCentered = true; //Hacer un objeto vacío en el centro si esto es falso

	public AudioClip damage;
	public AudioClip noDamage;

	private bool move = true;
	private Animator anim;
	private Transform pj;
	private GameObject hSpike;

	private Vector2 defaultPosition;
	private int defaultHp;
	private SpriteRenderer sr;
	public bool destroy = false;

	public LayerMask whatIsWall;

	void Awake() {

		defaultPosition = transform.position;
		defaultHp = hp;
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		speed *= -1;
		pj = GameObject.Find ("JP").GetComponent<Transform>();


		if (facingRight) {
			Flip ();
		}
	}

	// Use this for initialization
	void OnEnable () {

		transform.position = defaultPosition;
		hp = defaultHp;
		sr.enabled = true;
		gameObject.GetComponent<Collider2D>().enabled = true;
		anim.enabled = true;
		move = true;


		Invoke ("Stop", 1.5f);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (move) {
			Vector2 movement = Vector2.right * speed * Time.deltaTime;
			transform.Translate (movement);
			anim.SetFloat ("speed", Mathf.Abs (movement.x));

			//If there is and invisible wall in front of him, he flips
			if (Physics2D.Raycast(transform.FindChild("Center").position, Vector2.left * transform.localScale.x, 0.25f, whatIsWall).transform != null) {
				Flip();
			}

		} else if (facingRight && pj.position.x <= transform.position.x || !facingRight && pj.position.x >= transform.position.x) {
			Flip ();
		}
		
	}

	void Flip() {

		speed *= -1;
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		facingRight = !facingRight;
	}

	void LetsRoll() {

		move = true;
		Invoke ("Stop", 1.5f);
	}

	void Stop() {
		
		move = false;
		if (anim.enabled == true) {
			anim.SetFloat ("speed", 0);	
		}

		Invoke ("Attack", 1);
	}

	void Attack() {

		if (anim.enabled == true) {
			anim.SetTrigger ("attack");
			//Horizontal spike
			hSpike = Instantiate (hSpikePrefab, new Vector2(transform.position.x + 0.45f * speed, transform.position.y + 0.109f), transform.rotation);
			hSpike.GetComponent<Rigidbody2D> ().AddForce (new Vector2(100 * speed, 0));
			//Change orientation
			if (speed > 0) {
				var s = hSpike.transform.localScale;
				s.x *= -1;
				hSpike.transform.localScale = s;
			}
			//Vertical spike
			hSpike = Instantiate (hSpikePrefab, new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.Euler(0, 0, -90f));
			hSpike.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, 100));
			//Diagonal spike
			hSpike = Instantiate (hSpikePrefab, new Vector2(transform.position.x + 0.396f * speed, transform.position.y + 0.55f), Quaternion.Euler(0, 0, 45f * speed));
			hSpike.GetComponent<Rigidbody2D> ().AddForce (new Vector2(75 * speed, 75));
			if (speed > 0) {
				var s = hSpike.transform.localScale;
				s.x *= -1;
				hSpike.transform.localScale = s;
			}
		}

		Invoke ("LetsRoll", 1.5f);
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Lemon") {

			Destroy (other.gameObject);

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
				if (this.GetComponent<ReSkinAnimation> ()) {
					this.GetComponent<ReSkinAnimation> ().enabled = true;
					Invoke ("DisableReSkin", timeOfColor);
				}
				//Sound

				//Destruction
				hp -= 1;
				if (hp == 0) {
					
					if (destroy) {
						Destroy (gameObject);
					} else {
						CancelInvoke ("Stop");
						CancelInvoke ("Attack");
						CancelInvoke ("LetsRoll");
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
					GetComponent<AudioSource> ().clip = damage;
					GetComponent<AudioSource> ().Play ();
				}
			} else {
				
				GetComponent<AudioSource> ().clip = noDamage;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	void DisableReSkin() {

		if (this.GetComponent<ReSkinAnimation>()) {
			this.GetComponent<ReSkinAnimation> ().enabled = false;
		}

	}

	void OnDestroy() {

		//Send message to the spawner to spawn again
		if (GetComponentInParent<CameraSpawner>()) {
			GetComponentInParent<CameraSpawner> ().hasSpawned = false;
		}
	}

	void OnDisable() {

		CancelInvoke ("Stop");
		CancelInvoke ("Attack");
		CancelInvoke ("LetsRoll");
		sr.enabled = false;
		gameObject.GetComponent<Collider2D> ().enabled = false;
		anim.enabled = false;
	}
}
